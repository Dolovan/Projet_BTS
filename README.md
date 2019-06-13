# Application de détection et de positionnement d'objet

# Prérequis

 

 - Visual Studio 2017 
		
		https://visualstudio.microsoft.com/fr/vs/older-downloads/

 - .NET Core 2.1 
 
		https://dotnet.microsoft.com/download/dotnet-core/2.1

# CApplicationDetectionObjets

## Attributs

 - Cimage image
 - CListeEnvironnementsIdentifiables listeEnvironnementsIdentifiables
 - CEnvironnement environnement
 - CListeObjetsIdentifiables listeObjetsIdentifiables
 -  CNuagePoints3D nuagePoint3D
 - CDetectionObjet detection
 - CScene3D scene3D
 - NDArray[] resultArr
 - List < SPositionObjetDansImage > listeSPositionObjetDansImage
 - string imageSortie

## Méthodes

**void ChargerEnvironnementsIdentifiables_depuisFichierXML(string fichierEnvironnemtsIdentifiables)**

Exemple d'utilisation:

	application.ChargerEnvironnementsIdentifiables_depuisFichierXML(@"..\EnvironnementsIdentifiables.xml");
---
**void ChargerEnvironnementsIdentifiables_depuisXmlDocument(XmlDocument XMLEnvironnementsIdentifiables)**

Exemple d'utilisation:
	
	XmlDocument XMLEnvironnementsIdentifiables = new XmlDocument();
	ChargerEnvironnementsIdentifiables_depuisXmlDocument(XMLEnvironnementsIdentifiables);
---
**void ChargerImageOrientee_depuisFichierXML(string fichierImageOrientee)**

Exemple d'utilisation:
	
	application.ChargerImageOrientee_depuisFichierXML(@"..\ImageOrientee.xml");
---
**void ChargerImageOrientee_depuisXmlDocument(XmlDocument XMLImageOrientee)**

Exemple d'utilisation:
	
	XmlDocument XMLImageOrientee = new XmlDocument();
	application.ChargerImageOrientee_depuisXmlDocumentL(XMLImageOrientee);
---   

**void ChargerNuagePoint3D_depuisFichierXML(string fichierNuagePoint3D)**

Exemple d'utilisation:
	
	application.ChargerNuagePoint3D_depuisFichierXML(@"..\NuagePoints3D.xml");
---   
**void ChargerNuagePoints3D_depuisXmlDocument(XmlDocument XMLNuagePoints3D)**

Exemple d'utilisation:
	
	XmlDocument XMLNuagePoint3D = new XmlDocument();
	application.ChargerNuagePoint3D_depuisXmlDocument(XMLNuagePoint3D);
---   
**void ChargerObjetsIdentifiables_depuisFichierXML(string fichierObjetsIdentifiables)**
	
Exemple d'utilisation:
	
	application.ChargerObjetsIdentifiables_depuisFichierXML(@"..\ObjetsIdentifiables.xml");
---   
**void ChargerObjetsIdentifiables_depuisXmlDocument(XmlDocument XMLObjetsIdentifiables)**
	
Exemple d'utilisation:
	
	XmlDocument XMLObjetsIdentifiables = new XmlDocument();
	application.ChargerObjetsIdentifiables_depuisXmlDocument(XMLObjetsIdentifiables);
---   
**void parametrerDetection(
 float Min_score = 0.5f, 
string pbFile = @"..\..\..\..\reseaux_neurones\frozen_inference_graph.pb",
string labelFile = @"..\..\..\..\reseaux_neurones\mscoco_label_map.pbtxt"
)**

**Min_score** est le score minimum où l'on considère l'objet comme étant détecté.

**pbfile** est le chemin vers le modèle entrainé (extension .pb).

**labelfile** est le chemin vers le fichier contenant les labelles du modèle (extensions .pbtxt).

Exemple d'utilisation:
	
	application.parametrerDetection(0.1f);
---   
**void IdentifierObjets()**

Cette méthode permet de démarrer la détection d'objet dans l'image.
 
Exemple d'utilisation:
	
	application.IdentifierObjets();
---   
**void PositionnerObjetsDansImage()**

Cette méthode permet à l'application de récupérer l'emplacement des objets dans l'image.
Cette méthode s'utilise **après** la méthode "void IdentifierObjet()".
	
Exemple d'utilisation:

	application.PositionnerObjetsDansImage();
---   
**void GenererImageSortie(string filename = "output.jpg")**

**filename** est le chemin de l'image que l'on souhaite générer. Par défaut elle se nomme "output.jpg".

Cette méthode s'utilise **après** la méthode "void IdentifierObjet()".
Sur l'image générer, les objets détectés sont représenté par un rectangle.

**Image à mettre**   

Exemple d'utilisation:
	
	application.GenererImageSortie(@"..\..\..\..\images\output.jpg");
---   
**void GenererScene3d()**

Cette méthode génère la scène 3D.
Cette méthode s'utilise **après** la méthode "void PositionnerObjetsDansImage()".

Exemple d'utilisation:
	
	application.GenererScene3d();
---   
 **XmlDocument ExporterScene3DversXMLDocument()**
 
Cette méthode renvoie la scène 3D dans un objet de type XmlDocument.

Exemple d'utilisation:
	
	XmlDocument XMLScene3D = application.ExporterScene3DversXMLDocument();
---   
**void ExporterScene3DversFichierXML(string fichierScene3D)**

Cette méthode enregistre la scène 3D dans un fichier au format XML.

Exemple d'utilisation:
	
	application.ExporterScene3DversFichierXML(@"..\..\..\..\fichiers_XML\sortie.xml");
---   
**void AfficherRenduDetectionImage()**

Cette méthode affiche l'image qui à été générer.
Cette méthode s'utilise **après** la méthode "void GenererImageSortie(string filename = "output.jpg")".

Exemple d'utilisation:
	
	application.AfficherRenduDetectionImage();
---   
**void AfficherNuagePoints3D()**

Cette méthode affiche les informations concernant le nuage de points 3D.

Exemple d'utilisation:

	application.AfficherNuagePoints3D();
---   
**application.AfficherScene3D()**

Cette méthode affiche les informations concernant la scène 3D.

Exemple d'utilisation:

	application.AfficherScene3D();
---   
**void AfficherRenduDetectionConsole()**

Cette méthode affiche dans la console, les informations concernant la détection des objets.

Exemple d'utilisation:

	application.AfficherRenduDetectionConsole();
---   

**void AfficherObjetsIdentifiables()**

Cette méthode affiche les informations concernant les objets identifiables.

Exemple d'utilisation:

	application.AfficherObjetsIdentifiables();
---   
**void AfficherInfoImage()**

Cette méthode affiche les informations concernant l'image.

Exemple d'utilisation:

	application.AfficherInfoImage();
---   
**void AfficherEnvironnemtsIdentifiables()**

Cette méthode affiche les informations concernant les environnement identifiables.

Exemple d'utilisation:

	application.AfficherEnvironnemtsIdentifiables();
---   
**void AfficherTout()**

Cette méthode appelle l'ensemble des autres méthodes "Afficher".

Exemple d'utilisation:
	
	application.AfficherTout();
---   

2 425‬ lignes de code
