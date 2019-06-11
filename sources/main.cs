namespace Application
{
    class Program
    {       
        static void Main(string[] args)
        {
            CApplicationDetectionObjets application = new CApplicationDetectionObjets();

            //Charger les fichiers XML
            application.ChargerEnvironnementsIdentifiables_depuisFichierXML(@"..\..\..\..\fichiers_XML\EnvironnementsIdentifiables.xml");
            application.ChargerImageOrientee_depuisFichierXML(@"..\..\..\..\fichiers_XML\ImageOrientee.xml");
            application.ChargerNuagePoint3D_depuisFichierXML(@"..\..\..\..\fichiers_XML\NuagePoints3D.xml");
            application.ChargerObjetsIdentifiables_depuisFichierXML(@"..\..\..\..\fichiers_XML\ObjetsIdentifiables.xml");


            //Identifier les objets
            application.IdentifierObjets();

            //Positionner les objet
            application.PositionnerObjetsDansImage();

            //Génerer l'image de sortie
            application.GenererImageSortie(@"..\..\..\..\images\output.jpg");

            //Génerer la scène 3D
            application.GenererScene3d();
            
            //Exporter la scène 3D vers un fichier XML
            application.ExporterScene3DversFichierXML(@"..\..\..\..\fichiers_XML\sortie.xml");

            //affichage            
            application.AfficherTout();
            
            while(true)
            {
                // boucle à enlever en production
            }
        }
    }
}
