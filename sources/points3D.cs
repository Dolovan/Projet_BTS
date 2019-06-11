using System;
using System.Collections.Generic;
using System.Xml;

namespace Application
{
    class CPoint3d
    {
        protected double distance, elevation, azimut;

        public void SetPoint3d_distance(double Point3D_distance)
        {
            this.distance = Point3D_distance;
        }

        public void SetPoint3d_elevation(double Point3D_elevation)
        {
            this.elevation = Point3D_elevation;
        }

        public void SetPoint3d_azimut(double Point3D_azimut)
        {
            this.azimut = Point3D_azimut;
        }

        public double GetPoint3D_distance()
        {
            return this.distance;
        }

        public double GetPoint3D_elevation()
        {
            return this.elevation;
        }

        public double GetPoint3D_azimut()
        {
            return this.azimut;
        }

        public void Afficher()
        {
            Console.WriteLine("Distance = " + distance + ", Elevation = " + elevation + ", Azimut = " + azimut);
        }
    }

    class CNuagePoints3D
    {
        List<CPoint3d> lPoints3D;
        SSecteurAngulaire secteurAngulaire;

        public CNuagePoints3D()
        {
            this.secteurAngulaire = new SSecteurAngulaire();
            this.lPoints3D = new List<CPoint3d>();
        }

        public CPoint3d RecupererPoints3D(int index)
        {
            return this.lPoints3D[index];
        }

        public List<CPoint3d> RecupererListePoints3D()
        {
            return this.lPoints3D;
        }

        public int AjouterPoint3D(CPoint3d point3D)
        {
            this.lPoints3D.Add(point3D);
            return this.lPoints3D.IndexOf(point3D);
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

        public void SetSecteurAngulaire(int elevation_Max, int elevation_Min, int azimut_Max, int azimut_Min)
        {
            this.secteurAngulaire.elevation_Max = elevation_Max;
            this.secteurAngulaire.elevation_Min = elevation_Min;
            this.secteurAngulaire.azimut_Max = azimut_Max;
            this.secteurAngulaire.azimut_Min = azimut_Min;
        }

        public void ChargerNuagePoints3D_depuisXmlDocument(XmlDocument xmlDoc_NuagePoints3D)
        {
            XmlNodeList listePoints3D = xmlDoc_NuagePoints3D.GetElementsByTagName("point3d");
            XmlElement xmlElementNuagePoints3D = (XmlElement)xmlDoc_NuagePoints3D.GetElementsByTagName("nuagePoints").Item(0);   

            if (xmlElementNuagePoints3D != null)
            {
                if(xmlElementNuagePoints3D.GetAttribute("nombrePoints") != null)
                {
                        int nbPointsSelonFichier = Convert.ToInt32(xmlElementNuagePoints3D.GetAttribute("nombrePoints"));

                    if (listePoints3D.Count > 0)  // Si il y a au moins 1 point 
                    {
                        if (listePoints3D.Count == nbPointsSelonFichier)
                        {
                            for (int i = 0; i < listePoints3D.Count; i++)
                            {
                                XmlElement xmlElementPoint3D = (XmlElement)xmlDoc_NuagePoints3D.GetElementsByTagName("point3d").Item(i);

                                if (Convert.ToInt32(xmlElementPoint3D.GetAttribute("azimut")) < this.secteurAngulaire.azimut_Min)
                                    this.secteurAngulaire.azimut_Min = Convert.ToInt32(xmlElementPoint3D.GetAttribute("azimut"));
                                if (Convert.ToInt32(xmlElementPoint3D.GetAttribute("azimut")) > this.secteurAngulaire.azimut_Max)
                                    this.secteurAngulaire.azimut_Max = Convert.ToInt32(xmlElementPoint3D.GetAttribute("azimut"));

                                if (Convert.ToInt32(xmlElementPoint3D.GetAttribute("elevation")) < this.secteurAngulaire.elevation_Min)
                                    this.secteurAngulaire.elevation_Min = Convert.ToInt32(xmlElementPoint3D.GetAttribute("elevation"));
                                if (Convert.ToInt32(xmlElementPoint3D.GetAttribute("elevation")) > this.secteurAngulaire.elevation_Max)
                                    this.secteurAngulaire.elevation_Max = Convert.ToInt32(xmlElementPoint3D.GetAttribute("elevation"));

                                this.AjouterPoint3D(new CPoint3d());
                                this.lPoints3D[i].SetPoint3d_azimut(Convert.ToDouble(xmlElementPoint3D.GetAttribute("azimut")));
                                this.lPoints3D[i].SetPoint3d_elevation(Convert.ToDouble(xmlElementPoint3D.GetAttribute("elevation")));
                                this.lPoints3D[i].SetPoint3d_distance(Convert.ToDouble(xmlElementPoint3D.GetAttribute("distance")));
                            }
                        }
                        else
                        {
                            Console.WriteLine(@"Le nombre de point3D annonce dans le fichier ne correspond pas au nombre de point reel.");
                        }
                    }
                    else
                    {
                        Console.WriteLine(@"Il n'y a aucun point3D dans le fichier.");
                    }
                }
                
            }
            else
            {
                Console.WriteLine("Il n'y a aucun nuage de point3D dans ce fichier.");
            }
        }

        public void ChargerNuagePoint3D_depuisXML(string fichier_NuagePoints3D)
        {
            XmlDocument xmlDoc_NuagePoints3D = new XmlDocument();
            try
            {
                xmlDoc_NuagePoints3D.Load(fichier_NuagePoints3D);
                try
                {
                    this.ChargerNuagePoints3D_depuisXmlDocument(xmlDoc_NuagePoints3D);
                }
                catch
                {
                    Console.WriteLine("Erreur lors du chargement du nuage de points 3D depuis XmlDocument");
                }
            }
            catch
            {
                Console.WriteLine("Impossible d'ouvrir le fichier suivant: " + fichier_NuagePoints3D);
            }
        }

        public double GetDistance(int elevation, int azimut)
        {
            for(int i =0; i < this.lPoints3D.Count;i++)
            {
                if (this.lPoints3D[i].GetPoint3D_azimut() == azimut && this.lPoints3D[i].GetPoint3D_elevation() == elevation)
                    return this.lPoints3D[i].GetPoint3D_distance();
            }
            return -1;
        }

        public XmlDocument exporterXML()
        {
            XmlDocument doc = new XmlDocument();
            return doc;
        }

        public void afficher()
        {
            try
            {
                Console.WriteLine(@"Nombre de point3D dans le tableau: " + this.lPoints3D.Count);
                 for (int i = 0; i < this.lPoints3D.Count; i++)
                 {
                    Console.WriteLine(@"Point3D n° " + (i+1) + " :");
                    this.lPoints3D[i].Afficher();
                 }
                Console.WriteLine("");
            }
            catch
            {
                Console.WriteLine(@"Aucun affichage posible car aucun tableau n'a ete instancie.");
            }
            
        }
    }
}
