using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Tensorflow;
using Newtonsoft.Json;
using NumSharp;
using System.IO;
using System.Drawing.Drawing2D;
using static Tensorflow.Python;

namespace Application
{
    public class PbtxtItem
    {
        public string name { get; set; }
        public int id { get; set; }
        public string display_name { get; set; }
    }

    public class PbtxtItems
    {
        public List<PbtxtItem> items { get; set; }
    }

    //Class permettant l'extraction des labels qui se situe dans le fichier .pbtxt
    public class PbtxtParser
    {
        public static PbtxtItems ParsePbtxtFile(string filePath)
        {
            string line;
            string newText = "{\"items\":[";

            System.IO.StreamReader reader;

            try
            {
                reader = new System.IO.StreamReader(filePath);
            }
            catch
            {
                reader = null;
            }

            if(reader != null)
            {
                using (reader)
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        string newline = string.Empty;

                        if (line.Contains("{"))
                        {
                            newline = line.Replace("item", "").Trim();
                            newText += newline;
                        }
                        else if (line.Contains("}"))
                        {
                            newText = newText.Remove(newText.Length - 1);
                            newText += line;
                            newText += ",";
                        }
                        else
                        {
                            newline = line.Replace(":", "\":").Trim();
                            newline = "\"" + newline;
                            newline += ",";

                            newText += newline;
                        }
                    }

                    newText = newText.Remove(newText.Length - 1);
                    newText += "]}";
                    reader.Close();
                    PbtxtItems items = JsonConvert.DeserializeObject<PbtxtItems>(newText);
                    return items;

                }
            }
            else
            {
                Console.WriteLine("+-------------------------------------------------------------------------------+");
                Console.WriteLine("| " + DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " | Méthode: \"ParsePbtxtFile(string filePath)\"                     |");
                Console.WriteLine("| Impossible d'ouvrir le fichier \"" + filePath + "\".  |");
                Console.WriteLine("+-------------------------------------------------------------------------------+");
                Console.WriteLine("| Conseil: Vérifié que le fichier placé en paramètre existe.  |");
                 Console.WriteLine("+-------------------------------------------------------------+");

                return null;
            }
        }
    }

    public class CDetectionObjet
    {
        public float MIN_SCORE;
        string imageFilename;
        string pbFile;
        string labelFile ;
        string imageSortie = "output.jpg";
        NDArray imgArr;
        Bitmap bitmapOutput;

        public CDetectionObjet()
        {
            this.SetMin_Score(0);
            this.SetpbFile("");
            this.SetlabelFile("");
            File.Delete(this.GetImageSortie());
        }

        public void SetMin_Score(float min_score)
        {
            this.MIN_SCORE = min_score;
        }
        
        public void SetImageFilename(string imageFilename)
        {
            this.imageFilename = imageFilename;
        }

        public void SetImageSortie(string imageSortie)
        {
            this.imageSortie= imageSortie;
        }

        public void SetpbFile(string pbFile)
        {
            this.pbFile = pbFile;
        }

        public void SetlabelFile(string labelFile)
        {
            this.labelFile = labelFile;
        }

        public float GetMin_Score()
        {
            return this.MIN_SCORE;
        }

        public string GetImageFilename()
        {
            return this.imageFilename;
        }

        public string GetImageSortie()
        {
            return this.imageSortie;
        }

        public string GetpbFile()
        {
            return this.pbFile;
        }

        public string GetlabelFile()
        {
            return this.labelFile;
        }

        public void changerImage_depuisRep(string imageFilename)
        {
            this.imageFilename = imageFilename;
            this.bitmapOutput = new Bitmap(imageFilename);
            this.imgArr = ReadTensorFromImageFile(imageFilename);
        }

        public void changerImage_depuisImage(Image image)
        {
            if(image != null)
            {
                image.Save("imageTemp.jpg");
                this.bitmapOutput = new Bitmap(image);
                this.imgArr = ReadTensorFromImageFile("imageTemp.jpg");
                File.Delete("imageTemp.jpg");
            }
            else
            {
                Console.WriteLine("+--------------------------------------------------------------+");
                Console.WriteLine("| " + DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " | Méthode: \"changerImage_depuisImage(Image image)\"  |");
                Console.WriteLine("| La variable \"image\" vaut: null.                              |");
                Console.WriteLine("+---------------------------------------------------------------------------------------+");
                Console.WriteLine("| Conseil: Vérifié le paramètre de la méthode \"changerImage_depuisImage(Image image)\".  |");
                Console.WriteLine("+---------------------------------------------------------------------------------------+");
                Console.WriteLine(" ");
            }
        }

        private NDArray ReadTensorFromImageFile(string file_name)
        {
            return with(tf.Graph().as_default(), graph =>
            {
                var file_reader = tf.read_file(file_name, "file_reader");
                var decodeJpeg = tf.image.decode_jpeg(file_reader, channels: 3, name: "DecodeJpeg");
                var casted = tf.cast(decodeJpeg, TF_DataType.TF_UINT8);
                var dims_expander = tf.expand_dims(casted, 0);
                return with(tf.Session(graph), sess => sess.run(dims_expander));
            });
        }
        
        public NDArray[] detecter()
        {
            NDArray[] resultArr = new NDArray[0];

            try
            {
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " | Début de la détection... ");

                var graph = new Graph().as_default();
                graph.Import(pbFile);

                Tensor tensorNum = graph.OperationByName("num_detections");
                Tensor tensorBoxes = graph.OperationByName("detection_boxes");
                Tensor tensorScores = graph.OperationByName("detection_scores");
                Tensor tensorClasses = graph.OperationByName("detection_classes");
                Tensor imgTensor = graph.OperationByName("image_tensor");
                Tensor[] outTensorArr = new Tensor[] { tensorNum, tensorBoxes, tensorScores, tensorClasses };

                try
                {
                    with(tf.Session(graph), sess =>
                    {
                        NDArray results = sess.run(outTensorArr, new FeedItem(imgTensor, this.imgArr));
                        resultArr = results.Data<NDArray>();

                        try
                        {
                            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " | Fin de la détection... ");
                            return resultArr;
                        }
                        catch
                        {
                            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " | Erreur lors de la création de l'image de sortie !");
                            return resultArr;
                        }
                    });
                
                }
                catch
                {
                    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " | Erreur lors du chargement de l'image !");
                    return resultArr;
                }
            }
            catch
            {
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " | Erreur lors de l'importation du modèle !");
                return resultArr;
            }
            return resultArr;
        }

        

        public List<SPositionObjetDansImage> positionnerObjetsDansImage(NDArray[] resultArr)
        {
            if(resultArr == null || resultArr.Length == 0 )
            {
                return null;
            }

            PbtxtItems pbTxtItems = PbtxtParser.ParsePbtxtFile(labelFile);
            List<SPositionObjetDansImage> lPositionObjetDansImages = new List<SPositionObjetDansImage>();
            SPositionObjetDansImage sPositionObjetDansImage = new SPositionObjetDansImage();
            float[] scores = resultArr[2].Data<float>();

            for (int i = 0; i < scores.Length; i++)
            {
                float score = scores[i];

                if (score > MIN_SCORE)
                {
                    float[] boxes = resultArr[1].Data<float>();
                    float top = boxes[i * 4] * this.bitmapOutput.Height;
                    float left = boxes[i * 4 + 1] * this.bitmapOutput.Width;
                    float bottom = boxes[i * 4 + 2] * this.bitmapOutput.Height;
                    float right = boxes[i * 4 + 3] * this.bitmapOutput.Width;

                    Rectangle rect = new Rectangle()
                    {
                        X = (int)left,
                        Y = (int)top,
                        Width = (int)(right - left),
                        Height = (int)(bottom - top)
                    };

                    //Définition du centre de l'objet détecté
                    rect.X = rect.X + (rect.Width / 2);
                    rect.Y = rect.Y + (rect.Height / 2);
                    rect.Width = 1;
                    rect.Height = 1;

                    float[] ids = resultArr[3].Data<float>();

                    string name = pbTxtItems.items.Where(w => w.id == (int)ids[i]).Select(s => s.display_name).FirstOrDefault();

                    //Stockage des valeurs: nom, positionX et positionY de l'objet (les positions sont en pixel)
                    sPositionObjetDansImage.libelle = name;
                    sPositionObjetDansImage.position.x = rect.X;
                    sPositionObjetDansImage.position.y = rect.Y;

                    lPositionObjetDansImages.Add(sPositionObjetDansImage);
                }
            }

            return lPositionObjetDansImages;
        }

        public void GenererImageSortie(NDArray[] resultArr, string filename = @"output.jpg")
        {
            if(resultArr != null)
            {
                this.SetImageSortie(filename);
                PbtxtItems pbTxtItems = PbtxtParser.ParsePbtxtFile(labelFile);
                float[] scores = resultArr[2].Data<float>();
                this.imageSortie = filename;

                for (int i = 0; i < scores.Length; i++)
                {
                    float score = scores[i];

                    if (score > MIN_SCORE)
                    {
                        float[] boxes = resultArr[1].Data<float>();
                        float top = boxes[i * 4] * this.bitmapOutput.Height;
                        float left = boxes[i * 4 + 1] * this.bitmapOutput.Width;
                        float bottom = boxes[i * 4 + 2] * this.bitmapOutput.Height;
                        float right = boxes[i * 4 + 3] * this.bitmapOutput.Width;

                        Rectangle rect = new Rectangle()
                        {
                            X = (int)left,
                            Y = (int)top,
                            Width = (int)(right - left),
                            Height = (int)(bottom - top)
                        };

                        float[] ids = resultArr[3].Data<float>();
                        string name = pbTxtItems.items.Where(w => w.id == (int)ids[i]).Select(s => s.display_name).FirstOrDefault();
                        DessinerObjetIdentifier(this.bitmapOutput, rect, score, name);
                    }
                }

                this.bitmapOutput.Save(this.imageSortie);
                DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\" + Path.GetDirectoryName(this.imageSortie));
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " | " + $"Emplacement de l'image de sortie: {dir.FullName + @"\" + Path.GetFileName(this.imageSortie)}");
            }
            else
            {
                Console.WriteLine("+-------------------------------------------------------------------------------------------------+");
                Console.WriteLine("| " + DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " | Méthode: \"GenererImageSortie(NDArray[] resultArr, string filename = @\"output.jpg\")\"  |");
                Console.WriteLine("| Impossible de générer une image de sortie car la variable \"resultArr\" vaut: null.               |");
                Console.WriteLine("+-------------------------------------------------------------------------------------------------+");
                Console.WriteLine(" ");
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " | " + $"Emplacement de l'image de sortie: {this.imageSortie}");
            }
        }

        public void afficherRenduDetectionConsole(NDArray[] resultArr)
        {
            if(resultArr != null)
            {
                PbtxtItems pbTxtItems = PbtxtParser.ParsePbtxtFile(labelFile);

                float[] scores = resultArr[2].Data<float>();

                Console.WriteLine("");
                Console.WriteLine("+-----------Detection----------+");
                Console.WriteLine("");

                for (int i = 0; i < scores.Length; i++)
                {
                    float score = scores[i];
                    if (score > MIN_SCORE)
                    {
                        float[] boxes = resultArr[1].Data<float>();
                        float top = boxes[i * 4] * this.bitmapOutput.Height;
                        float left = boxes[i * 4 + 1] * this.bitmapOutput.Width;
                        float bottom = boxes[i * 4 + 2] * this.bitmapOutput.Height;
                        float right = boxes[i * 4 + 3] * this.bitmapOutput.Width;

                        Rectangle rect = new Rectangle()
                        {
                            X = (int)left,
                            Y = (int)top,
                            Width = (int)(right - left),
                            Height = (int)(bottom - top)
                        };

                        float[] ids = resultArr[3].Data<float>();

                        string name = pbTxtItems.items.Where(w => w.id == (int)ids[i]).Select(s => s.display_name).FirstOrDefault();
                        Console.WriteLine("Objet: " + name + " " + score + " dans la zone: " + rect);
                    }
                }
            }
            else
            {
                Console.WriteLine("+---------------------------------------------------------------------------+");
                Console.WriteLine("| " + DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " | Méthode: \"afficherRenduDetectionConsole(NDArray[] resultArr)\"");
                Console.WriteLine("| Aucun affichage console possible car la variable \"resultArr\" vaut: null.");
                Console.WriteLine("+---------------------------------------------------------------------------+");
                Console.WriteLine(" ");
            }
        }

        public void afficherRenduDetectionImage(string filename)
        {
            DirectoryInfo dir;

            if (filename.StartsWith(@"..\"))
            {
                dir = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\" + Path.GetDirectoryName(filename));
            }
            else
            {
                dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            }
                
            bool fichierExistant = false;

            foreach (FileInfo f in dir.GetFiles())
            {
                if (f.Name == Path.GetFileName(filename))
                    fichierExistant = true;
            }

            if(fichierExistant)
            {
                Process.Start(@"cmd.exe ", @" /c " + dir.FullName + @"\" + Path.GetFileName(filename));
            }
            else
            {
                Console.WriteLine("+-----------------------------------------------------------------------------------------------------+");
                Console.WriteLine("| " + DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " | Méthode: \"afficherRenduDetectionImage()\"");
                Console.WriteLine("| Aucun affichage d'image possible, car le fichier \"" + filename + "\" est introuvable.");
                Console.WriteLine("+-----------------------------------------------------------------------------------------------------+");
                Console.WriteLine(" ");
            }
        }

        private void DessinerObjetIdentifier(Bitmap bmp, Rectangle rect, float score, string name)
        {
            using (Graphics graphic = Graphics.FromImage(bmp))
            {
                graphic.SmoothingMode = SmoothingMode.AntiAlias;

                using (Pen pen = new Pen(Color.Red, 2))
                {
                    graphic.DrawRectangle(pen, rect);
                    rect.X = rect.X + (rect.Width / 2);
                    rect.Y = rect.Y + (rect.Height / 2);
                    rect.Width = 1;
                    rect.Height = 1;
                    graphic.DrawRectangle(pen, rect);

                    Point p = new Point(rect.Right + 5, rect.Top + 5);
                    string text = string.Format("{0}:{1}%", name, (int)(score * 100));
                    graphic.DrawString(text, new Font("Verdana", 8), Brushes.Red, p);
                }
            }
        }
    }
}
