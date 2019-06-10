using NumSharp;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Application
{
    public struct SSecteurAngulaire
    {
        //Variable représentant les coordonées d'un pixel
        public int elevation_Max, elevation_Min, azimut_Max, azimut_Min;

        public SSecteurAngulaire(int elevation_Max, int elevation_Min, int azimut_Max, int azimut_Min)
        {
            this.elevation_Max = elevation_Max;
            this.elevation_Min = elevation_Min;
            this.azimut_Max = azimut_Max;
            this.azimut_Min = azimut_Min;
        }
    }

    public struct SPositionObjetDansImage
    {
        //Variable représentant les coordonées d'un pixel
        public SPositionPixel position;
        public string libelle;

        public SPositionObjetDansImage(SPositionPixel position, string libelle)
        {
            this.position = position;
            this.libelle = libelle;
        }
    }

    public class CApplicationDetectionObjets
    {
        Cimage image;
        CListeEnvironnementsIdentifiables listeEnvironnementsIdentifiables;
        CEnvironnement environnement;
        CListeObjetsIdentifiables listeObjetsIdentifiables;
        CNuagePoints3D nuagePoint3D;
        CDetectionObjet detection;
        CScene3D scene3D;
        NDArray[] resultArr;
        List<SPositionObjetDansImage> listeSPositionObjetDansImage;
        string imageSortie;

        public CApplicationDetectionObjets()
        {
            this.listeSPositionObjetDansImage = new List<SPositionObjetDansImage>();
            this.environnement = new CEnvironnement();
            this.image = new Cimage();
            this.listeEnvironnementsIdentifiables = new CListeEnvironnementsIdentifiables();
            this.listeObjetsIdentifiables = new CListeObjetsIdentifiables();
            this.nuagePoint3D = new CNuagePoints3D();
            this.detection = new CDetectionObjet();
            this.scene3D = new CScene3D();

            this.parametrerDetection(0.5f, @"..\..\..\..\reseaux_neurones\frozen_inference_graph.pb", @"..\..\..\..\reseaux_neurones\mscoco_label_map.pbtxt");
        }

        public void ChargerObjetsIdentifiables_depuisXML(string filename)
        {
            this.listeObjetsIdentifiables.ChargerObjetsIdentifiables_depuisXML(filename);
        }

        public void ChargerObjetsIdentifiables_depuisXmlDocument(XmlDocument XMLobjetsIdentifiables)
        {
            this.listeObjetsIdentifiables.ChargerObjetsIdentifiables_depuisXmlDocument(XMLobjetsIdentifiables);
        }

        public void ChargerNuagePoint3D_depuisXML(string filename)
        {
            this.nuagePoint3D.ChargerNuagePoint3D_depuisXML(filename);
        }

        public void ChargerNuagePoints3D_depuisXmlDocumentL(XmlDocument XMLNuagePoints3D)
        {
            this.nuagePoint3D.ChargerNuagePoints3D_depuisXmlDocument(XMLNuagePoints3D);
        }

        public void ChargerEnvironnementsIdentifiables_depuisXML(string filename)
        {
            this.listeEnvironnementsIdentifiables.ChargerEnvironnementsIdentifiables_depuisXML(filename);
        }

        public void ChargerEnvironnementsIdentifiables_depuisXmlDocument(XmlDocument XMLEnvironnementsIdentifiables)
        {
            this.listeEnvironnementsIdentifiables.ChargerEnvironnementsIdentifiables_depuisXmlDocument(XMLEnvironnementsIdentifiables);
        }

        public void ChargerImageOrientee_depuisXML(string filename)
        {
            this.image.ChargerImageOrientee_depuisXML(filename);
            this.DefinirEnvironnement(this.image.GetLibelle());
        }

        public void ChargerImageOrientee_depuisXmlDocumentL(XmlDocument XMLImageOrientee)
        {
            this.image.ChargerImageOrientee_depuisXmlDocument(XMLImageOrientee);
        }

        protected void ChargerImage(string filename)
        {
            this.image.RepertoireVersImage(filename);
        }

        protected void DefinirEnvironnement(string libelle)
        {
            this.environnement = this.listeEnvironnementsIdentifiables.GetEnvironnementListEnvironnements(libelle);
        }

        public void parametrerDetection(float Min_score = 0.5f, string pbFile = @"..\..\..\..\reseaux_neurones\frozen_inference_graph.pb", string labelFile = @"..\..\..\..\reseaux_neurones\mscoco_label_map.pbtxt")
        {
            detection.SetpbFile(pbFile);
            detection.SetlabelFile(labelFile);
            detection.SetMin_Score(Min_score);
        }

        public void IdentifierObjets()
        {
            detection.changerImage_depuisImage(this.image.GetImage());
            this.resultArr = detection.detecter();
        }

        public void GenererImageSortie(string filename = "output.jpg")
        {
            this.imageSortie = filename;
            this.detection.GenererImageSortie(this.resultArr,filename);
        }
        
        public void PositionnerObjetsDansImage()
        {
            if(this.resultArr != null)
            {
                detection.changerImage_depuisImage(this.image.GetImage());

                this.listeSPositionObjetDansImage =  detection.positionnerObjetsDansImage(this.resultArr);
            }
            else
            {
                Console.WriteLine("+--------------------------------------------+");
                Console.WriteLine("| " + DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " | Méthode: \"PositionnerObjets()\"  |");
                Console.WriteLine("| La variable \"resultArr\" vaut: null.        |");
                Console.WriteLine("+---------------------------------------------------------------------------------------------------+");
                Console.WriteLine("| Conseil: Vérifié que la méthode \"detection.IdentifierObjets()\" soit appelée avant cette méthode.  |");
                Console.WriteLine("+---------------------------------------------------------------------------------------------------+");
                Console.WriteLine(" ");
                this.listeSPositionObjetDansImage = null;
            }
        }
        
        public void GenererScene3d()
        {
            if (this.listeSPositionObjetDansImage != null)
            {
                if (this.environnement != null)
                {
                    for (int i = 0; i < this.listeSPositionObjetDansImage.Count; i++)
                    {
                        int idObj = this.listeObjetsIdentifiables.RecupererIdobjetAvecLibelle(this.listeSPositionObjetDansImage[i].libelle);

                        if (this.environnement.ObjetDansEnvironnement_idObj(idObj))
                        {
                            CObjetIdentifie nouvelobjetIdentifie = new CObjetIdentifie();

                            //Recuperer elevation et azimut centre pour les ajouter a elevationAuCentre...
                            //Élevation de l'objet par rapport au centre de l'image

                            float elevationCentre = image.CalculerAngleElevationCentre(image.GetSecteurAngulaire_elevation_Max() - image.GetSecteurAngulaire_elevation_Min());
                            float elevationObjet = image.CalculerAngleElevationObjet(this.listeSPositionObjetDansImage[i], image.GetSecteurAngulaire_elevation_Max() - image.GetSecteurAngulaire_elevation_Min());
                            float elevationCentreVersObjet = elevationObjet - elevationCentre;
                            float elevationReel = image.GetSecteurAngulaire_elevation_Min() + elevationObjet;

                            //Azimut de l'objet par rapport au centre de l'image

                            float azimutCentre = image.CalculerAngleAzimutCentre(image.GetSecteurAngulaire_azimut_Max() - image.GetSecteurAngulaire_azimut_Min());
                            float azimutObjet = image.CalculerAngleAzimutObjet(this.listeSPositionObjetDansImage[i], image.GetSecteurAngulaire_azimut_Max() - image.GetSecteurAngulaire_azimut_Min());
                            float azimutCentreVersObjet = azimutObjet - azimutCentre;
                            float azimutReel = image.GetSecteurAngulaire_azimut_Min() + azimutObjet;

                            double distance = this.nuagePoint3D.GetDistance((int)Math.Ceiling(elevationReel), (int)Math.Ceiling(azimutReel));

                            if (distance == -1)
                            {
                                Console.WriteLine("Distance non disponnible dans le nuage de point 3D");
                            }

                            double entree = 45.47;
                            double sortie = Math.Cos((entree * Math.PI) / 180) * 3.42;


                            if (azimutCentreVersObjet < 0)
                            {
                                nouvelobjetIdentifie.SetPosition3D_X(this.image.GetPosition3D_X() - (Math.Sin((Math.Abs(azimutCentreVersObjet) * Math.PI) / 180) * distance));
                            }
                            if (azimutCentreVersObjet >= 0)
                            {
                                nouvelobjetIdentifie.SetPosition3D_X(this.image.GetPosition3D_X() + (Math.Sin((Math.Abs(azimutCentreVersObjet) * Math.PI) / 180) * distance));
                            }

                            nouvelobjetIdentifie.SetPosition3D_Y(this.image.GetPosition3D_Y() + (Math.Cos((Math.Abs(azimutCentreVersObjet) * Math.PI) / 180) * distance));

                            if (elevationCentreVersObjet < 0)
                            {
                                nouvelobjetIdentifie.SetPosition3D_Z(this.image.GetPosition3D_Z() - (Math.Sin((Math.Abs(elevationCentreVersObjet) * Math.PI) / 180) * distance));
                            }
                            if (elevationCentreVersObjet >= 0)
                            {
                                nouvelobjetIdentifie.SetPosition3D_Z(this.image.GetPosition3D_Z() + (Math.Sin((Math.Abs(elevationCentreVersObjet) * Math.PI) / 180) * distance));
                            }

                            nouvelobjetIdentifie.setLibelle(this.listeSPositionObjetDansImage[i].libelle);

                            CObjetIdentifiable objetIdentifiableTemp = this.listeObjetsIdentifiables.RecupererObjetIdentifiable_Libelle(this.listeSPositionObjetDansImage[i].libelle);

                            if (objetIdentifiableTemp != null)
                            {
                                nouvelobjetIdentifie.ChargerObjetIdentifiable(objetIdentifiableTemp);
                                this.scene3D.AjouterObjetIdentifie(nouvelobjetIdentifie);
                            }
                            else
                            {
                                Console.WriteLine("L'objet \"" + this.listeSPositionObjetDansImage[i].libelle + "\" n'est répertorié dans la liste des objets identifiables.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("L'objet \"" + this.listeSPositionObjetDansImage[i].libelle + "\" ne fait pas partie de l'environnement\"" + this.environnement.GetLibelle() + "\".");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("L'environnement\"" + this.image.GetLibelle() + "\" n'est pas déninit dans comme étant un environnement identifiable.");
                }
            }
            else
            {
                Console.WriteLine("+----------------------------------------------------------------------------------------------+");
                Console.WriteLine("| " + DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " | Méthode: \"GenererScene3d(List<SPositionObjetDansImage> sPositionObjetDansImage)\"  |");
                Console.WriteLine("| La variable \"sPositionObjetDansImage\" vaut: null.                                            |");
                Console.WriteLine("+---------------------------------------------------------------------------------------------------------------+");
                Console.WriteLine("| Conseil: Vérifié que la méthode \"application.PositionnerObjetsDansImage()\" soit appelée avant cette méthode.  |");
                Console.WriteLine("+---------------------------------------------------------------------------------------------------------------+");
                Console.WriteLine(" ");
            }
        }

        public XmlDocument ExporterScene3DversXMLDocument()
        {
            return this.scene3D.exporterXMLDocument();
        }

        public void ExporterScene3DversFichierXML(string filename)
        {
            this.scene3D.exporterFichierXML(filename);
        }

        public void AfficherRenduDetectionImage()
        {
            detection.afficherRenduDetectionImage(this.imageSortie);
        }

        public void AfficherRenduDetectionConsole()
        {
            detection.afficherRenduDetectionConsole(this.resultArr);
        }

        public void AfficherEnvironnemtsIdentifiables()
        {
            this.listeEnvironnementsIdentifiables.afficher();
        }

        public void AfficherNuagePoints3D()
        {
            this.nuagePoint3D.afficher();
        }

        public void AfficherInfoImage()
        {
            this.image.afficher();
        }

        public void AfficherObjetsIdentifiables()
        {
            this.listeObjetsIdentifiables.Afficher();
        }

        public void AfficherScene3D()
        {
            this.scene3D.AfficherListeObjetsIdentifies();
        }

        public void AfficherTout()
        {
            this.nuagePoint3D.afficher();
            this.listeEnvironnementsIdentifiables.afficher();
            this.listeObjetsIdentifiables.Afficher();
            this.image.afficher();
            this.detection.afficherRenduDetectionConsole(this.resultArr);
            this.detection.afficherRenduDetectionImage(this.imageSortie);
        }
    }
}

   