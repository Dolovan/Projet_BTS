using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace Application
{
    class CGeometrie
    {

    }

    class CPosition3D
    {
        double x, y, z;

        public void SetPosition3D_X(double x)
        {
            this.x = x;
        }
        public void SetPosition3D_Y(double y)
        {
            this.y = y;
        }
        public void SetPosition3D_Z(double z)
        {
            this.z = z;
        }

        public double GetPosition3D_X()
        {
            return this.x;
        }

        public double GetPosition3D_Y()
        {
            return this.y;
        }
        public double GetPosition3D_Z()
        {
            return this.z;
        }
    }
    
    class CRendu
    {
        string materiaux;

        public void SetMateriaux(string materiaux)
        {
            this.materiaux = materiaux;
        }

        public string GetMateriaux()
        {
            return this.materiaux;
        }
    }

    class CCategorieObjet
    {
        int idCategorieObjet;
        string libelleCategorieObjet = "|---NON-DEFINIT---|";

        public void SetIdCategorieObjet(int idCategorieObjet)
        {
            this.idCategorieObjet = idCategorieObjet;
        }

        public void SetLibelleCategorieObjet(string libelleCategorieObjet)
        {
            this.libelleCategorieObjet = libelleCategorieObjet;
        }

        public int GetIdCategorieObjet()
        {
            return this.idCategorieObjet;
        }

        public string GetLibelleCategorieObjet()
        {
            return this.libelleCategorieObjet;
        }
    }

    class CRotation3D
    {
        double rotation_x;
        double rotation_y;
        double rotation_z;

        public void SetRotation_X(double x)
        {
            this.rotation_x = x;
        }

        public void SetRotation_Y(double y)
        {
            this.rotation_y = y;
        }

        public void SetRotation_Z(double z)
        {
            this.rotation_z = z;
        }

        public double GetRotation_X()
        {
            return this.rotation_x;
        }

        public double GetRotation_Y()
        {
            return this.rotation_y;
        }

        public double GetRotation_Z()
        {
            return this.rotation_z;
        }
    }

    class CProprietesDefaut
    {
        double taille_x;
        double taille_y;
        double taille_z;

        CRotation3D rotation3D;

        double comportPhysique;

        public CProprietesDefaut()
        {
            this.rotation3D = new CRotation3D();
        }
        
        public void SetTaille_X(double taille)
        {
            this.taille_x = taille;
        }

        public void SetTaille_Y(double taille)
        {
            this.taille_y = taille;
        }

        public void SetTaille_Z(double taille)
        {
            this.taille_z = taille;
        }

        public void SetComportPhysique(double comportPhysique)
        {
            this.comportPhysique = comportPhysique;
        }

        public void SetRotation_X(double x)
        {
            this.rotation3D.SetRotation_X(x);
        }

        public void SetRotation_Y(double y)
        {
            this.rotation3D.SetRotation_Y(y);
        }

        public void SetRotation_Z(double z)
        {
            this.rotation3D.SetRotation_Z(z);
        }

        public double GetRotation_X()
        {
            return this.rotation3D.GetRotation_X();
        }

        public double GetRotation_Y()
        {
            return this.rotation3D.GetRotation_Y();
        }

        public double GetRotation_Z()
        {
            return this.rotation3D.GetRotation_Z();
        }

        public double GetTaille_X()
        {
            return this.taille_x;
        }

        public double GetTaille_Y()
        {
            return this.taille_y;
        }

        public double GetTaille_Z()
        {
            return this.taille_z;
        }

        public double GetComportPhysique()
        {
            return this.comportPhysique;
        }

    }

    class CObjetIdentifiable
    {
        string libelle;
        int idObj;

        List<CCategorieObjet> listCategoriesObjets;
        CRendu rendu;
        CGeometrie geometrie;
        CProprietesDefaut proprietes;

        public CObjetIdentifiable()
            {
                this.proprietes = new CProprietesDefaut();
                this.listCategoriesObjets = new List<CCategorieObjet>();
                this.rendu = new CRendu();
                this.geometrie = new CGeometrie();
            }

        public void setLibelle(string libelle)
        {
            this.libelle = libelle;
        }

        public void SetIdObj(int idObj)
        {
            this.idObj = idObj;
        }

        public int GetIdObj()
        {
            return this.idObj;
        }

        public List<CCategorieObjet> GetListeCategoriesObjets()
        {
            return this.listCategoriesObjets;
        }

        public void SetListeCategoriesObjets(List<CCategorieObjet> ListeCategorieObjets)
        {
            this.listCategoriesObjets = ListeCategorieObjets;
        }

        public void SetRendu(CRendu rendu)
        {
            this.rendu=rendu;
        }

        public void SetRendu_materiaux(string materiaux)
        {
            this.rendu.SetMateriaux(materiaux);
        }

        public void SetGeometrie(CGeometrie geometrie)
        {
            this.geometrie = geometrie;
        }

        public void setProprietesDefaut(CProprietesDefaut proprietesDefaut)
        {
            this.proprietes=proprietesDefaut;
        }

        public CProprietesDefaut getProprietesDefaut()
        {
            return this.proprietes ;
        }

        public void setProprietesDefaut_taille_X(double taille_x)
        {
            this.proprietes.SetTaille_X(taille_x);
        }

        public void setProprietesDefaut_taille_Y(double taille_y)
        {
            this.proprietes.SetTaille_Y(taille_y);
        }

        public void setProprietesDefaut_taille_Z(double taille_z)
        {
            this.proprietes.SetTaille_Z(taille_z);
        }

        public void setProprietesDefaut_rotation_X(double rotation_x)
        {
            this.proprietes.SetRotation_X(rotation_x);
        }

        public void setProprietesDefaut_rotation_Y(double rotation_y)
        {
            this.proprietes.SetRotation_Y(rotation_y);
        }

        public void setProprietesDefaut_rotation_Z(double rotation_z)
        {
            this.proprietes.SetRotation_Z(rotation_z);
        }

        public void setProprietesDefaut_comportPhysique(double comportPhysique)
        {
            this.proprietes.SetComportPhysique(comportPhysique);
        }

        public int AjouterCategorieListIdCategories(int idCategorie)
        {
            CCategorieObjet categorieObjet =new CCategorieObjet();

            this.listCategoriesObjets.Add(categorieObjet);
            int index = this.listCategoriesObjets.IndexOf(categorieObjet);
            this.listCategoriesObjets[index].SetIdCategorieObjet(idCategorie);

            return index;
        }
        public int AjouterCategorieListIdCategories(CCategorieObjet categorieObjet)
        {
            this.listCategoriesObjets.Add(categorieObjet);
            return this.listCategoriesObjets.IndexOf(categorieObjet);
        }

        public bool SetCategorieListIdCategories(int position , int idCategorie)
        {
            if (position <= (this.listCategoriesObjets.Count - 1) && position >= 0)
            {
                this.listCategoriesObjets[position].SetIdCategorieObjet(idCategorie);
                return true;
            }
            return false;
        }

        public int GetIdCategorieListIdObj(int position)
        {
            if (position <= (this.listCategoriesObjets.Count - 1) && position >= 0)
                return this.listCategoriesObjets[position].GetIdCategorieObjet();

            return -1;
        }

        public string GetLibelleCategorieListIdObj(int position)
        {
            if (position <= (this.listCategoriesObjets.Count - 1) && position >= 0)
                return this.listCategoriesObjets[position].GetLibelleCategorieObjet();

            return null;
        }

        public string GetRendu_Materiaux()
        {
            return this.rendu.GetMateriaux();
        }

        public CRendu GetRendu()
        {
            return this.rendu;
        }

        public CGeometrie GetGeometrie()
        {
            return this.geometrie;
        }

        public double GetProprietesDefaut_taille_X()
        {
            return this.proprietes.GetTaille_X();
        }

        public double GetProprietesDefaut_taille_Y()
        {
            return this.proprietes.GetTaille_Y();
        }

        public double GetProprietesDefaut_taille_Z()
        {
            return this.proprietes.GetTaille_Z();
        }

        public double GetProprietesDefaut_rotation_X()
        {
            return this.proprietes.GetRotation_X();
        }

        public double GetProprietesDefaut_rotation_Y()
        {
            return this.proprietes.GetRotation_Y();
        }

        public double GetProprietesDefaut_rotation_Z()
        {
            return this.proprietes.GetRotation_Z();
        }

        public string getLibelle()
        {
            return this.libelle;
        }

        public double GetProprietesDefaut_comportPhysique()
        {
            return this.proprietes.GetComportPhysique();
        }

        public void Afficher()
        {
            Console.WriteLine("Libelle objet: " + this.getLibelle() + "  |  Id Objet: " + this.GetIdObj());

            Console.WriteLine("");
            Console.WriteLine("Categories: ");
            for (int i = 0; i < this.listCategoriesObjets.Count; i++)
            {
                Console.WriteLine("categorie n°" + (i+1) + ": " + this.listCategoriesObjets[i].GetIdCategorieObjet() + "  |  libelle: " + this.listCategoriesObjets[i].GetLibelleCategorieObjet() );
            }

            Console.WriteLine("");
            Console.WriteLine("Taille Standard: ");
            Console.WriteLine("x: " + this.GetProprietesDefaut_taille_X() + ", y: " + this.GetProprietesDefaut_taille_Y() + ", z: " + this.GetProprietesDefaut_taille_Z());

            Console.WriteLine("");
            Console.WriteLine("Rotation Standard: ");
            Console.WriteLine("x: " + this.GetProprietesDefaut_rotation_X() + ", y: " + this.GetProprietesDefaut_rotation_Y() + ", z: " + this.GetProprietesDefaut_rotation_Z());
            Console.WriteLine("");
        }
    }

    class CListeObjetsIdentifiables
    {
        List<CObjetIdentifiable> listObjetsIdentifiables = new List<CObjetIdentifiable>();

        public int AjouterObjetIdentifiableListIdCategories_avecParametres(string libelle, int idObj, double taille_x, double taille_y, double taille_z, double rotation_x, double rotation_y, double rotation_z)
        {
            CObjetIdentifiable objetIdentifiable = new CObjetIdentifiable();

            this.listObjetsIdentifiables.Add(objetIdentifiable);
            int index = this.listObjetsIdentifiables.IndexOf(objetIdentifiable);
            this.listObjetsIdentifiables[index].setLibelle(libelle);
            this.listObjetsIdentifiables[index].SetIdObj(idObj);
            this.listObjetsIdentifiables[index].setProprietesDefaut_taille_X(taille_x);
            this.listObjetsIdentifiables[index].setProprietesDefaut_taille_Y(taille_y);
            this.listObjetsIdentifiables[index].setProprietesDefaut_taille_Z(taille_z);
            this.listObjetsIdentifiables[index].setProprietesDefaut_rotation_X(rotation_x);
            this.listObjetsIdentifiables[index].setProprietesDefaut_rotation_Y(rotation_y);
            this.listObjetsIdentifiables[index].setProprietesDefaut_rotation_Z(rotation_z);

            return index;
        }

        public int AjouterObjetIdentifiableListIdCategories_sansParametre(CObjetIdentifiable objetIdentifiable)
        {
            this.listObjetsIdentifiables.Add(objetIdentifiable);
            int index = this.listObjetsIdentifiables.IndexOf(objetIdentifiable);

            return index;
        }

        public int AjouterCategorie_ObjetIdentifiableListIdCategories(CObjetIdentifiable objetIdentifiable, int idCategorieObjet)
        {
            return objetIdentifiable.AjouterCategorieListIdCategories(idCategorieObjet);
        }
        
        public int RecupererIdobjetAvecLibelle(string libelle)
        {
            for(int i = 0; i < this.listObjetsIdentifiables.Count; i++)
            {
                if(this.listObjetsIdentifiables[i].getLibelle() == libelle)
                    return this.listObjetsIdentifiables[i].GetIdObj();
            }
            return -1;
        }

        public CObjetIdentifiable RecupererObjetIdentifiable_IdObjet(int idObjet)
        {
            for(int i = 0; i < this.listObjetsIdentifiables.Count; i++)
            {
                if (this.listObjetsIdentifiables[i].GetIdObj() == idObjet)
                    return this.listObjetsIdentifiables[i];
            }
            return null;
        }

        public CObjetIdentifiable RecupererObjetIdentifiable_Libelle(string libelle)
        {
            for (int i = 0; i < this.listObjetsIdentifiables.Count; i++)
            {
                if (this.listObjetsIdentifiables[i].getLibelle() == libelle)
                    return this.listObjetsIdentifiables[i];
            }
            return null;
        }

        public void ChargerObjetsIdentifiables_depuisXmlDocument(XmlDocument xmlDoc_ObjetsIdentifiables)
        {
            XmlNodeList listeObjetsIdentifiables = xmlDoc_ObjetsIdentifiables.GetElementsByTagName("objetIdentifiable");

            if (listeObjetsIdentifiables.Count > 0)  // Si il y a au moins 1 objet identifiable
            {

                for (int i = 0; i < listeObjetsIdentifiables.Count; i++)
                {
                    this.AjouterObjetIdentifiableListIdCategories_sansParametre(new CObjetIdentifiable());

                    XmlElement xmlObjetIdentifiable = (XmlElement)xmlDoc_ObjetsIdentifiables.GetElementsByTagName("objetIdentifiable").Item(i);

                    this.listObjetsIdentifiables[i].setLibelle(Convert.ToString(xmlObjetIdentifiable.GetAttribute("libelle")));

                    this.listObjetsIdentifiables[i].AjouterCategorieListIdCategories(Convert.ToInt32(xmlObjetIdentifiable.GetAttribute("cat1")));

                    this.listObjetsIdentifiables[i].AjouterCategorieListIdCategories(Convert.ToInt32(xmlObjetIdentifiable.GetAttribute("cat2")));

                    this.listObjetsIdentifiables[i].SetIdObj(Convert.ToInt32(xmlObjetIdentifiable.GetAttribute("idObj")));

                    XmlElement xmlObjetIdentifiable_tailleStandard = (XmlElement)xmlObjetIdentifiable.GetElementsByTagName("tailleStandard").Item(0);
                    XmlElement xmlObjetIdentifiable_rotationStandard = (XmlElement)xmlObjetIdentifiable.GetElementsByTagName("rotation").Item(0);

                    this.listObjetsIdentifiables[i].setProprietesDefaut_taille_X(double.Parse(xmlObjetIdentifiable_tailleStandard.GetAttribute("x"), CultureInfo.InvariantCulture));
                    this.listObjetsIdentifiables[i].setProprietesDefaut_taille_Y(double.Parse(xmlObjetIdentifiable_tailleStandard.GetAttribute("y"), CultureInfo.InvariantCulture));
                    this.listObjetsIdentifiables[i].setProprietesDefaut_taille_Z(double.Parse(xmlObjetIdentifiable_tailleStandard.GetAttribute("z"), CultureInfo.InvariantCulture));
                    
                    this.listObjetsIdentifiables[i].setProprietesDefaut_rotation_X(double.Parse(xmlObjetIdentifiable_rotationStandard.GetAttribute("x"), CultureInfo.InvariantCulture));
                    this.listObjetsIdentifiables[i].setProprietesDefaut_rotation_Y(double.Parse(xmlObjetIdentifiable_rotationStandard.GetAttribute("y"), CultureInfo.InvariantCulture));
                    this.listObjetsIdentifiables[i].setProprietesDefaut_rotation_Z(double.Parse(xmlObjetIdentifiable_rotationStandard.GetAttribute("z"), CultureInfo.InvariantCulture));
                }
            }
            else
            {
                Console.WriteLine(@"Il n'y a aucun environement identifiable dans ce fichier.");
            }
        }

        public void ChargerObjetsIdentifiables_depuisXML(string fichier_ObjetsIdentifiables)
        {
            XmlDocument xmlDoc_ObjetsIdentifiables = new XmlDocument();
            try
            {
                xmlDoc_ObjetsIdentifiables.Load(fichier_ObjetsIdentifiables);

                try
                {
                    this.ChargerObjetsIdentifiables_depuisXmlDocument(xmlDoc_ObjetsIdentifiables);
                }
                catch
                {
                    Console.WriteLine("Erreur lors du chargement des objets identifiables !");
                }
            }
            catch
            {
                Console.WriteLine("Impossible d'ouvrir le fichier suivant: " + fichier_ObjetsIdentifiables);
            }
        }

        public void Afficher()
        {
            try
            {
                Console.WriteLine(@"Details des " + this.listObjetsIdentifiables.Count + " objets identifiables: ");
                Console.WriteLine(" ");

                for (int i = 0; i < this.listObjetsIdentifiables.Count; i++)
                {
                    Console.WriteLine(@"+---------------------------------------------------------+");
                    this.listObjetsIdentifiables[i].Afficher();
                }
                    Console.WriteLine(@"+---------------------------------------------------------+");
            }
            catch
            {
                Console.WriteLine(@"Aucun affichage posible.");
            }
        }

    }

    class CObjetIdentifie : CObjetIdentifiable
    {
        CPosition3D position3D;
        
        public CObjetIdentifie()
        {
            this.position3D = new CPosition3D();
        }

        public void ChargerObjetIdentifiable(CObjetIdentifiable objetIdentifiable)
        {
            this.setLibelle(objetIdentifiable.getLibelle());
            this.setProprietesDefaut(objetIdentifiable.getProprietesDefaut());
            this.SetRendu(objetIdentifiable.GetRendu());
            this.SetGeometrie(objetIdentifiable.GetGeometrie());
            this.SetIdObj(objetIdentifiable.GetIdObj());
            this.SetListeCategoriesObjets(objetIdentifiable.GetListeCategoriesObjets());
        }

        public void SetPosition3D(CPosition3D position3D)
        {
            this.position3D = position3D;
        }

        public void SetPosition3D_X(double x)
        {
            this.position3D.SetPosition3D_X(x);
        }

        public void SetPosition3D_Y(double y)
        {
            this.position3D.SetPosition3D_Y(y);
        }

        public void SetPosition3D_Z(double z)
        {
            this.position3D.SetPosition3D_Z(z);
        }
        
        public CPosition3D GetPosition3D()
        {
            return this.position3D;
        }

        public double GetPosition3D_X()
        {
            return this.position3D.GetPosition3D_X();        
        }

        public double GetPosition3D_Y()
        {
            return this.position3D.GetPosition3D_Y();
        }

        public double GetPosition3D_Z()
        {
            return this.position3D.GetPosition3D_Z();
        }

        public void AfficherPosition3D()
        {
            Console.WriteLine("Position 3D: ");
            Console.WriteLine("Position X: " + this.GetPosition3D_X());
            Console.WriteLine("Position Y: " + this.GetPosition3D_Y());
            Console.WriteLine("Position Z: " + this.GetPosition3D_Z());
            Console.WriteLine(" ");
        }
    }

    class ClisteObjetsIdentifies
    {
        List<CObjetIdentifie> lObjetsIdentifies;

        public ClisteObjetsIdentifies()
        {
            this.lObjetsIdentifies = new List<CObjetIdentifie>();
        }

        public int AjouterObjetIdentifie(CObjetIdentifie objetIdentifie)
        {
            this.lObjetsIdentifies.Add(objetIdentifie);
            return this.lObjetsIdentifies.IndexOf(objetIdentifie);
        }

        public CObjetIdentifie RecupererObjetIdentifie(int index)
        {
            return this.lObjetsIdentifies[index];
        }

        public void ViderListeObjetsIdentifies()
        {
            this.lObjetsIdentifies.Clear();
        }

        public int getNombreObjetsIdentifies()
        {
            return this.lObjetsIdentifies.Count;
        }

        public List<CObjetIdentifie> RecupererListeObjetsIdentifies()
        {
            return this.lObjetsIdentifies;
        }
        public void afficher()
        {
            for(int i = 0; i < this.lObjetsIdentifies.Count; i++)
            {
                Console.WriteLine(" ");
                Console.WriteLine("Objet n°" + (i + 1) + ":");
                this.lObjetsIdentifies[i].Afficher();
                this.lObjetsIdentifies[i].AfficherPosition3D();
            }
        }
    }
}
