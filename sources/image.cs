using System;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Globalization;
using System.Xml.Linq;

namespace Application
{

    //Structure contenant les coordonnée d'un pixel 
    public struct SPositionPixel
    {
        //Variable représentant les coordonées d'un pixel
        public long x, y;

        public SPositionPixel(long x, long y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class CCamera
    {
        int profondeurCouleur;
        int Resolution_Y;
        int Resolution_X;
        int focale;

        public void SetProfondeurCouleur(int profondeurCouleur)
        {
            this.profondeurCouleur = profondeurCouleur;
        }

        public void SetResolution_Y(int Resolution_Y)
        {
            this.Resolution_Y = Resolution_Y;
        }

        public void SetResolution_X(int Resolution_X)
        {
            this.Resolution_X = Resolution_X;
        }

        public void SetFocale(int focale)
        {
            this.focale = focale;
        }

        public int GetProfondeurCouleur()
        {
            return this.profondeurCouleur;
        }

        public int GetResolution_X()
        {
            return this.Resolution_X;
        }

        public int GetResolution_Y()
        {
            return this.Resolution_Y;
        }

        public int GetFocale()
        {
            return this.focale;
        }
    }

    //Classe traitant le traitement de l'image
    public class Cimage  
    {
        Image image;
        CCamera camera;
        CPosition3D position3DRepere;
        CRotation3D rotation3DRepere;
        string format;
        

        string libelle;
        string date;
        long largeur, hauteur;
        
        SPositionPixel centre;
        SSecteurAngulaire secteurAngulaire;

        public Cimage() 
        {
            this.camera = new CCamera();
            this.position3DRepere = new CPosition3D();
            this.rotation3DRepere = new CRotation3D();
        }

        public void setFormat(string format)
        {
            this.format = format;
        }

        public string getFormat()
        {
            return this.format;
        }

        public void SetLibelle(string libelle)
        {
            this.libelle = libelle;
        }

        public string GetLibelle()
        {
            return this.libelle;
        }

        public void SetDate(string date)
        {
            this.date = date;
        }

        public string GetDate()
        {
            return this.date;
        }

        public void SetCentreImage()
        {
            this.centre.x = (getLargeurImage() / (long)2);
            this.centre.y = (getHauteurImage() / (long)2);
        }

        public SPositionPixel GetCentreImage()
        {
            return this.centre;
        }
        
        public void SetCamera_ProfondeurCouleur(int profondeurCouleur)
        {
            this.camera.SetProfondeurCouleur(profondeurCouleur);
        }

        public void SetCamera_Resolution_X(int resolution_x)
        {
            this.camera.SetResolution_X(resolution_x);
        }

        public void SetCamera_Resolution_Y(int resolution_y)
        {
            this.camera.SetResolution_Y(resolution_y);
        }

        public void SetCamera_Focale(int focale)
        {
            this.camera.SetFocale(focale);
        }

        public int GetCamera_ProfondeurCouleur()
        {
            return this.camera.GetProfondeurCouleur();
        }

        public int GetCamera_Resolution_X()
        {
            return this.camera.GetResolution_X();
        }

        public int GetCamera_Resolution_Y()
        {
            return this.camera.GetResolution_Y();
        }

        public int GetCamera_Focale()
        {
            return this.camera.GetFocale();
        }

        public void SetPosition3D_X(double x)
        {
            position3DRepere.SetPosition3D_X(x);
        }

        public void SetPosition3D_Y(double y)
        {
            position3DRepere.SetPosition3D_Y(y);
        }

        public void SetPosition3D_Z(double z)
        {
            position3DRepere.SetPosition3D_X(z);
        }

        public double GetPosition3D_X()
        {
            return this.position3DRepere.GetPosition3D_X();
        }

        public double GetPosition3D_Y()
        {
            return this.position3DRepere.GetPosition3D_Y();
        }

        public double GetPosition3D_Z()
        {
            return this.position3DRepere.GetPosition3D_Z();
        }

        public void SetRotation_X(double x)
        {
            this.rotation3DRepere.SetRotation_X(x);
        }

        public void SetRotation_Y(double y)
        {
            this.rotation3DRepere.SetRotation_Y(y);
        }

        public void SetRotation_Z(double z)
        {
            this.rotation3DRepere.SetRotation_Z(z);
        }

        public double GetRotation_X()
        {
            return this.rotation3DRepere.GetRotation_X();
        }

        public double GetRotation_Y()
        {
            return this.rotation3DRepere.GetRotation_Y();
        }

        public double GetRotation_Z()
        {
            return this.rotation3DRepere.GetRotation_Z();
        }

        public void SetSecteurAngulaire(int elevation_Max, int elevation_Min, int azimut_Max, int azimut_Min)
        {
            this.secteurAngulaire.elevation_Max = elevation_Max;
            this.secteurAngulaire.elevation_Min = elevation_Min;
            this.secteurAngulaire.azimut_Max = azimut_Max;
            this.secteurAngulaire.azimut_Min = azimut_Min;
        }
        
        public int GetSecteurAngulaire_elevation_Max()
        {
            return this.secteurAngulaire.elevation_Max;
        }

        public int GetSecteurAngulaire_elevation_Min()
        {
            return this.secteurAngulaire.elevation_Min;
        }

        public int GetSecteurAngulaire_azimut_Max()
        {
            return this.secteurAngulaire.azimut_Max;
        }

        public int GetSecteurAngulaire_azimut_Min()
        {
            return this.secteurAngulaire.azimut_Min;
        }

        public Image GetImage()
        {
            return this.image;
        }

        protected void setLargeurImage()
        {
            this.largeur = this.image.Width;
        }

        public long getLargeurImage()
        {
            return this.largeur;
        }

        protected void setHauteurImage()
        {
            this.hauteur = this.image.Height;
        }

        public long getHauteurImage()
        {
            return this.hauteur;
        }

        public float CalculerAngleAzimutObjet(SPositionObjetDansImage objet, int angleTotaleAzimut)
        {
            float azimutObjet = ((float)objet.position.x * angleTotaleAzimut) / (float)this.getLargeurImage();

            return azimutObjet;
        }

        public float CalculerAngleAzimutCentre(int angleTotaleAzimut)
        {
            float azimutCentre = (float)(this.centre.x * angleTotaleAzimut) / (float)this.getLargeurImage();

            return azimutCentre;
        }

        public float CalculerAngleElevationObjet(SPositionObjetDansImage objet, int angleTotaleElevation)
        {
            float elevationObjet = (float)((this.getHauteurImage()-objet.position.y) * angleTotaleElevation)/(float)this.getHauteurImage();

            return elevationObjet;
        }

        public float CalculerAngleElevationCentre(int angleTotaleElevation)
        {
            float elevationCentre = (float)((this.getHauteurImage() - this.centre.y) * angleTotaleElevation) / (float)this.getHauteurImage();

            return elevationCentre;
        }

        public string RepertoirVersBase64(string repertoire)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(repertoire);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            string path = @"./base64.txt";
            File.WriteAllText(path, base64ImageRepresentation);

            return base64ImageRepresentation;
        }

        public bool Base64VersRepertoire(string base64, string filename)
        {
            try
            {
                var bytes = Convert.FromBase64String(base64);
                using (var imageFile = new FileStream(filename, FileMode.Create))
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        //Méthode permettant de charger l'image en mémoire à partir d'un fichier
        public int RepertoireVersImage(string repertoire)
        {
            //Test de l'ouverture de l'image
            try
            {
                this.image = System.Drawing.Image.FromFile(repertoire);
                setHauteurImage();
                setLargeurImage();
                SetCentreImage();

                return 0;
            }
            //Si le test échoue
            catch
            {
                Console.WriteLine("Impossible de charger l'image !");
                return -1;
            }
        }

        //Méthode permettant de charger l'image en mémoire à partir d'une image coder en Base64
        public int Base64VersImage(string base64)
        {
            //Test de la conversion de l'image
            try
            {
                byte[] bytes = Convert.FromBase64String(base64);
                this.image = Image.FromStream(new MemoryStream(bytes));
                setHauteurImage();
                setLargeurImage();
                SetCentreImage();
                return 0;
            }
            //Si le test échoue
            catch
            {
                Console.WriteLine("Impossible de convertir l'image !");
                return -1;
            }
        }

        public void exporter_vers_xml(string repertoire)
        {
            XElement img = new XElement("image", this.RepertoirVersBase64(repertoire));
            Console.WriteLine(img);
        }

        //Extraction des informations du XML imaage orientée
        public void ChargerImageOrientee_depuisXmlDocument(XmlDocument xmlDoc_ImageOrientee)
        {
            XmlNodeList listeImages = xmlDoc_ImageOrientee.GetElementsByTagName("image");
            if (listeImages.Count > 0)  // Si il y a au moins 1 image
            {
                XmlElement xmlImage = (XmlElement)xmlDoc_ImageOrientee.GetElementsByTagName("image").Item(0);

                this.SetLibelle(Convert.ToString(xmlImage.GetAttribute("libelle")));
                this.SetDate(Convert.ToString(xmlImage.GetAttribute("date")));
                XmlElement xmlCamera = (XmlElement)xmlImage.GetElementsByTagName("camera").Item(0);

                this.SetCamera_ProfondeurCouleur(Convert.ToInt32(xmlCamera.GetAttribute("profondeurCouleur")));
                this.SetCamera_Resolution_X(Convert.ToInt32(xmlCamera.GetAttribute("xResolution")));
                this.SetCamera_Resolution_Y(Convert.ToInt32(xmlCamera.GetAttribute("yResolution")));
                this.SetCamera_Focale(Convert.ToInt32(xmlCamera.GetAttribute("focale")));

                XmlElement xmlSecteurAngulaire = (XmlElement)xmlImage.GetElementsByTagName("secteurAngulaire").Item(0);

                this.secteurAngulaire.elevation_Max = Convert.ToInt32(xmlSecteurAngulaire.GetAttribute("eleMax"));
                this.secteurAngulaire.elevation_Min = Convert.ToInt32(xmlSecteurAngulaire.GetAttribute("eleMin"));
                this.secteurAngulaire.azimut_Max = Convert.ToInt32(xmlSecteurAngulaire.GetAttribute("aziMax"));
                this.secteurAngulaire.azimut_Min = Convert.ToInt32(xmlSecteurAngulaire.GetAttribute("aziMin"));

                XmlElement xmlPositionRepere = (XmlElement)xmlImage.GetElementsByTagName("positionRepere").Item(0);
                
                this.position3DRepere.SetPosition3D_X(double.Parse(xmlPositionRepere.GetAttribute("x"), CultureInfo.InvariantCulture));
                this.position3DRepere.SetPosition3D_Z(double.Parse(xmlPositionRepere.GetAttribute("z"), CultureInfo.InvariantCulture));
                this.position3DRepere.SetPosition3D_Y(double.Parse(xmlPositionRepere.GetAttribute("y"), CultureInfo.InvariantCulture));

                XmlElement xmlRotationRepere = (XmlElement)xmlImage.GetElementsByTagName("rotationRepere").Item(0);

                this.rotation3DRepere.SetRotation_X(double.Parse(xmlRotationRepere.GetAttribute("azimut"), CultureInfo.InvariantCulture));
                this.rotation3DRepere.SetRotation_Y(double.Parse(xmlRotationRepere.GetAttribute("elevation"), CultureInfo.InvariantCulture));
                this.rotation3DRepere.SetRotation_Z(double.Parse(xmlRotationRepere.GetAttribute("zenith"), CultureInfo.InvariantCulture));

                XmlNodeList xmlImageData = xmlImage.GetElementsByTagName("data");
                XmlElement xmlDataAttributs = (XmlElement)xmlImage.GetElementsByTagName("data").Item(0);

                //récupérer les données de l'image en base64 pour les convertir bitmap
                this.Base64VersImage(xmlImageData.Item(0).InnerXml);

                //récupérer le format sous lequel est enregisté l'image.
                this.setFormat(xmlDataAttributs.GetAttribute("format"));
            }
            else
            {
                Console.WriteLine(@"Il n'y a aucun environement identifiable dans ce fichier.");
            }
        }

        public void ChargerImageOrientee_depuisXML(string fichier_ImageOrientee)
        {
            XmlDocument xmlDoc_imageOrientee = new XmlDocument();
            try
            {
                xmlDoc_imageOrientee.Load(fichier_ImageOrientee);

                try
                {
                    this.ChargerImageOrientee_depuisXmlDocument(xmlDoc_imageOrientee);
                }
                catch
                {
                    Console.WriteLine("Erreur lors du chargement de l'image !");
                }
            }
            catch
            {
                Console.WriteLine("Impossible d'ouvrir le fichier suivant: " + fichier_ImageOrientee);
            }
        }

        public void afficher()
        {
            try
            {
                Console.WriteLine(" ");
                Console.WriteLine(@"Details de l'image: ");
                Console.WriteLine(" ");
                Console.WriteLine("Libelle: " + this.GetLibelle() + "  | Date: " + this.GetDate());
                Console.WriteLine(" ");
                Console.WriteLine(@"Camera: ");
                Console.WriteLine("Focale: " + this.camera.GetFocale() + "  | Profondeur Couleur: " + this.camera.GetProfondeurCouleur() + "  |  Resolution X: " + this.camera.GetResolution_X() + "  |  Resolution Y: " + this.camera.GetResolution_Y());
                Console.WriteLine(" ");
                Console.WriteLine(@"Secteur Angulaire: ");
                Console.WriteLine("elevation max: " + this.secteurAngulaire.elevation_Max + "  |  elevation min: " + this.secteurAngulaire.elevation_Min + "  | azimut Max: " + this.secteurAngulaire.azimut_Max + "  | azimut Min: " + this.secteurAngulaire.azimut_Min);
                Console.WriteLine(" ");
                Console.WriteLine(@"Position repere: ");
                Console.WriteLine("X: " + this.position3DRepere.GetPosition3D_X() + "  |  Y: " + this.position3DRepere.GetPosition3D_Y() + "  | Z: " + this.position3DRepere.GetPosition3D_Z());
                Console.WriteLine(" ");
                Console.WriteLine(@"Rotation repere: ");
                Console.WriteLine("X: " + this.rotation3DRepere.GetRotation_X() + "  |  Y: " + this.rotation3DRepere.GetRotation_Y() + "  | Z: " + this.rotation3DRepere.GetRotation_Z());
                Console.WriteLine(" ");
                Console.WriteLine(@"Dimension: ");
                Console.WriteLine("Largeur: " + this.getLargeurImage() + "  |  Hauteur: " + this.getHauteurImage());
                Console.WriteLine(" ");
                Console.WriteLine(@"Format d'enregistrement: " + this.getFormat());


            }
            catch
            {
                Console.WriteLine(@"Aucun affichage posible.");
            }
        }

    }
}