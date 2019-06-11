
namespace Application
{
    class Program
    {       
        static void Main(string[] args)
        {
            CApplicationDetectionObjets application = new CApplicationDetectionObjets();
            
            application.ChargerEnvironnementsIdentifiables_depuisFichierXML(@"..\..\..\..\fichiers_XML\EnvironnementsIdentifiables.xml");

            application.ChargerImageOrientee_depuisFichierXML(@"..\..\..\..\fichiers_XML\ImageOrientee.xml");

            application.ChargerNuagePoint3D_depuisFichierXML(@"..\..\..\..\fichiers_XML\NuagePoints3D.xml");

            application.ChargerObjetsIdentifiables_depuisFichierXML(@"..\..\..\..\fichiers_XML\ObjetsIdentifiables.xml");

            application.IdentifierObjets();

            application.PositionnerObjetsDansImage();

            application.GenererImageSortie(@"..\..\..\..\images\output.jpg");

            application.GenererScene3d();
            
            application.ExporterScene3DversFichierXML(@"..\..\..\..\fichiers_XML\sortie.xml");

            application.AfficherRenduDetectionImage();
            
            application.AfficherTout();
            
            while(true)
            {
                // boucle à enlever en production
            }
        }
    }
}
