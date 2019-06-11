using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace Application
{
    internal class CScene3D
    {
        ClisteObjetsIdentifies listeObjetsIdentifies;

        public CScene3D()
        {
            this.listeObjetsIdentifies = new ClisteObjetsIdentifies();
        }

        public int AjouterObjetIdentifie(CObjetIdentifie objetIdentifie)
        {
            return this.listeObjetsIdentifies.AjouterObjetIdentifie(objetIdentifie);
        }

        public CObjetIdentifie RecupererObjetIdentifie(int index)
        {
            return this.listeObjetsIdentifies.RecupererObjetIdentifie(index);
        }

        public void ViderListeObjetsIdentifiables()
        {
            this.listeObjetsIdentifies.ViderListeObjetsIdentifies();
        }

        public List<CObjetIdentifie> RecupererListeObjetsIdentifies()
        {
            return this.listeObjetsIdentifies.RecupererListeObjetsIdentifies();
        }

        public void AfficherListeObjetsIdentifies()
        {
            this.listeObjetsIdentifies.afficher();
        }

        public XmlDocument exporterXMLDocument()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;

            doc.InsertBefore(xmlDeclaration, root);

            XmlElement xElement_objetsIdentifies = doc.CreateElement(string.Empty, "objetsIdentifies", string.Empty);
            doc.AppendChild(xElement_objetsIdentifies);

            List<CObjetIdentifie> lObjetIdentifies = this.listeObjetsIdentifies.RecupererListeObjetsIdentifies();

            for (int i = 0; i < lObjetIdentifies.Count; i++)
            {
                XmlElement xElement_objetIdentifie = doc.CreateElement(string.Empty, "objetIdentifie", string.Empty);

                xElement_objetsIdentifies.AppendChild(xElement_objetIdentifie);
                xElement_objetIdentifie.SetAttribute("idObj", Convert.ToString(lObjetIdentifies[i].GetIdObj()));

                //Element position du fichier XML
                XmlElement xElement_position = doc.CreateElement(string.Empty, "position", string.Empty);
                xElement_objetIdentifie.AppendChild(xElement_position);
                xElement_position.SetAttribute("x", Math.Round(lObjetIdentifies[i].GetPosition3D_X(), 2).ToString(CultureInfo.InvariantCulture));
                xElement_position.SetAttribute("y", Math.Round(lObjetIdentifies[i].GetPosition3D_Y(), 2).ToString(CultureInfo.InvariantCulture));
                xElement_position.SetAttribute("z", Math.Round(lObjetIdentifies[i].GetPosition3D_Z(), 2).ToString(CultureInfo.InvariantCulture));

                //Element rotation du fichier XML
                XmlElement xElement_rotation = doc.CreateElement(string.Empty, "rotation", string.Empty);
                xElement_objetIdentifie.AppendChild(xElement_rotation);
                xElement_rotation.SetAttribute("x", Math.Round(lObjetIdentifies[i].GetProprietesDefaut_rotation_X(), 2).ToString(CultureInfo.InvariantCulture));
                xElement_rotation.SetAttribute("y", Math.Round(lObjetIdentifies[i].GetProprietesDefaut_rotation_Y(), 2).ToString(CultureInfo.InvariantCulture));
                xElement_rotation.SetAttribute("z", Math.Round(lObjetIdentifies[i].GetProprietesDefaut_rotation_Z(), 2).ToString(CultureInfo.InvariantCulture));

                //Element taille du fichier XML
                XmlElement xElement_taille = doc.CreateElement(string.Empty, "taille", string.Empty);
                xElement_objetIdentifie.AppendChild(xElement_taille);
                xElement_taille.SetAttribute("x", Math.Round(lObjetIdentifies[i].GetProprietesDefaut_taille_X(), 2).ToString(CultureInfo.InvariantCulture));
                xElement_taille.SetAttribute("y", Math.Round(lObjetIdentifies[i].GetProprietesDefaut_taille_Y(), 2).ToString(CultureInfo.InvariantCulture));
                xElement_taille.SetAttribute("z", Math.Round(lObjetIdentifies[i].GetProprietesDefaut_taille_Z(), 2).ToString(CultureInfo.InvariantCulture));
            }
            return doc;
        }

        public void exporterFichierXML(string filename)
        {
            this.exporterXMLDocument().Save(filename);
        }
    }
}
