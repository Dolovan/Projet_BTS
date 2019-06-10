
namespace Application
{
    class Program
    {       
        static void Main(string[] args)
        {
            CApplicationDetectionObjets application = new CApplicationDetectionObjets();
            
            application.ChargerEnvironnementsIdentifiables_depuisXML(@"..\..\..\..\fichiers_XML\EnvironnementsIdentifiables.xml");
           //application.ChargerEnvironnementsIdentifiables_depuisXmlDocument(...);

            application.ChargerImageOrientee_depuisXML(@"..\..\..\..\fichiers_XML\ImageOrientee.xml");
            //application.ChargerImageOrientee_depuisXmlDocumentL(...);

            application.ChargerNuagePoint3D_depuisXML(@"..\..\..\..\fichiers_XML\NuagePoints3D.xml");
            ///application.ChargerNuagePoints3D_depuisXmlDocumentL(...);

            application.ChargerObjetsIdentifiables_depuisXML(@"..\..\..\..\fichiers_XML\ObjetsIdentifiables.xml");
            //application.ChargerEnvironnementsIdentifiables_depuisXmlDocument(...);

           //application.parametrerDetection(0.1f);
            /// Les paramètres sont: (float Min_score,string pbfile, string labelfile)
            /// Min_score est le score minimu où l'on considère l'objet comme étant détecté
            /// pbfile est le chemin vers le modèle (extension .pb)
            /// labelfile est le chemin vers le fichier contenant les labelles du modèle (extensions .pbtxt)

            application.IdentifierObjets();

            application.PositionnerObjetsDansImage();

            application.GenererImageSortie(@"..\..\..\..\images\output.jpg");
            //Le paramètre est optionnel, c'est le chemin du fichier de sortie.

           //application.GenererScene3d();
            
            //application.ExporterScene3DversXMLDocument();
           //application.ExporterScene3DversFichierXML(@"..\..\..\..\fichiers_XML\sortie.xml");


            application.AfficherRenduDetectionImage();
            //application.AfficherNuagePoints3D();
            //application.AfficherScene3D();
            //application.AfficherRenduDetectionConsole();
            //application.AfficherObjetsIdentifiables();
            //application.AfficherInfoImage();
            //application.AfficherEnvironnemtsIdentifiables();
           //application.AfficherTout();
        }
    }
}
