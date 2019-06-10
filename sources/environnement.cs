using System;
using System.Xml;
using System.Collections.Generic;

namespace Application
{
    class CEnvironnement
    {
        string libelle;
        List<int> ListIdObj = new List<int>();

        public void SetLibelle(string libelle)
        {
            this.libelle = libelle;
        }

        public int AjouterIdObjListIdObj(int valeur)
        {
            this.ListIdObj.Add(valeur);
            return this.ListIdObj.IndexOf(valeur);
        }

        public List<int> GetListIdObj()
        {
           return this.ListIdObj;
        }

        public bool ObjetDansEnvironnement_idObj(int idobj)
        {
            for (int i = 0; i < this.ListIdObj.Count; i++)
            {
                if (this.ListIdObj[i] == idobj)
                    return true;
            }

            return false;
        }

        public string GetLibelle()
        {
            return this.libelle;
        }

        public void Afficher()
        {
            Console.WriteLine("Libelle: " + GetLibelle());

            for (int i = 0; i < this.ListIdObj.Count; i++)
            {
                Console.WriteLine("Id objet: " + this.ListIdObj[i]);
            }
        }
    }

    class CListeEnvironnementsIdentifiables
    {
        protected List<CEnvironnement> ListEnvironnements = new List<CEnvironnement>();

        public int AjouterEnvironnementListEnvironnements(CEnvironnement environnement)
        {
            this.ListEnvironnements.Add(environnement);
            return this.ListEnvironnements.IndexOf(environnement);
        }

        public CEnvironnement GetEnvironnementListEnvironnements(string libelle)
        {
            for(int i = 0; i < this.ListEnvironnements.Count; i++)
            {
                if (this.ListEnvironnements[i].GetLibelle() == libelle)
                    return this.ListEnvironnements[i];
            }
            return null;
        }
        public CEnvironnement GetEnvironnementsListEnvironnements(int index)
        {
            return this.ListEnvironnements[index];
        }

        public void ChargerEnvironnementsIdentifiables_depuisXmlDocument(XmlDocument xmlDoc_EnvironnementsIdentifiables)
        {
            XmlNodeList listeEnvironnements = xmlDoc_EnvironnementsIdentifiables.GetElementsByTagName("environnementIdentification");

            if (listeEnvironnements.Count > 0)  // Si il y a au moins 1 environnement
            {
                for (int i = 0; i < listeEnvironnements.Count; i++)
                {
                    XmlElement xmlEnvironnentIdentification = (XmlElement)xmlDoc_EnvironnementsIdentifiables.GetElementsByTagName("environnementIdentification").Item(i);
                    this.AjouterEnvironnementListEnvironnements(new CEnvironnement());

                    this.ListEnvironnements[i].SetLibelle(Convert.ToString(xmlEnvironnentIdentification.GetAttribute("libelle")));

                    XmlNodeList listeObjetsIdentifiables = xmlEnvironnentIdentification.GetElementsByTagName("objetIdentifiable");

                    for (int j = 0; j < listeObjetsIdentifiables.Count; j++)
                    {
                        XmlElement xmlObjetIdentifiable = (XmlElement)xmlEnvironnentIdentification.GetElementsByTagName("objetIdentifiable").Item(j);
                        this.ListEnvironnements[i].AjouterIdObjListIdObj(Convert.ToInt32(xmlObjetIdentifiable.GetAttribute("idObj")));
                    }
                }
            }
            else
            {
                Console.WriteLine(@"Il n'y a aucun environement identifiable dans ce fichier.");
            }
        }
        

        public void ChargerEnvironnementsIdentifiables_depuisXML(string fichier_EnvironnementsIdentifiables)
        {
            XmlDocument xmlDoc_EnvironnementsIdentifiables = new XmlDocument();
            try
            {
                xmlDoc_EnvironnementsIdentifiables.Load(fichier_EnvironnementsIdentifiables);

                try
                {
                    this.ChargerEnvironnementsIdentifiables_depuisXmlDocument(xmlDoc_EnvironnementsIdentifiables);
                }
                catch
                {
                    Console.WriteLine("Erreur lors du chargement des l'environnements !");
                }
            }
            catch
            {
                Console.WriteLine("Impossible d'ouvrir le fichier suivant: " + fichier_EnvironnementsIdentifiables);
            }
        }

        public void afficher()
        {
            try
            {
                Console.WriteLine(@"Details des " + this.ListEnvironnements.Count + " environnements identifiables: ");
                Console.WriteLine(" ");
                for (int i = 0; i < this.ListEnvironnements.Count; i++)
                {
                    Console.WriteLine(@"+------------------+");
                    Console.WriteLine(@"Environnement n° " + (i + 1) + " :");
                    this.ListEnvironnements[i].Afficher();
                }
                Console.WriteLine(@"+------------------+");
            }
            catch
            {
                Console.WriteLine(@"Aucun affichage posible.");
            }
        }
    }
}