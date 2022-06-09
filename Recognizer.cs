using Emgu.CV;
using Emgu.CV.OCR;
using IronOcr;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Recognizer
{
    public interface IRecognizer
    {
        string RecognizeArr(List<System.Drawing.Bitmap> bitmaps);
        string Recognize(System.Drawing.Bitmap bitmap);
    }
   
    public class RecognizerF : IRecognizer
    {
        public string rus_modelPath = "\\Model_ru\\rus.traineddata";
        public string RecognizeArr(List<System.Drawing.Bitmap> bitmaps)
        {
            string data = string.Empty;
            try
            {
                Tesseract tesseract = new Tesseract(rus_modelPath, "rus", OcrEngineMode.TesseractLstmCombined); //Используется кроме библиотеки скаченная модель языка
                for (int i = 0; i < bitmaps.Count; i++)
                {
                    Pix pix = new Pix(bitmaps[i].ToMat());
                    tesseract.SetImage(pix);

                    tesseract.Recognize();

                    data += tesseract.GetUTF8Text();

                    tesseract.Dispose();
                }
                return data;
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                return data;
            }
        }

        public string Recognize(System.Drawing.Bitmap bitmap)
        {
            string data = string.Empty;
            try
            {
                Pix pix = new Pix(bitmap.ToMat());
                Tesseract tesseract = new Tesseract(rus_modelPath, "rus", OcrEngineMode.TesseractLstmCombined); //Используется кроме библиотеки скаченная модель языка
                tesseract.SetImage(pix);

                tesseract.Recognize();

                data = tesseract.GetUTF8Text();

                tesseract.Dispose();
                return data;
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                return data;
            }
        }
    }

    public class RecognizerT : IRecognizer
    {
        public string RecognizeArr(List<System.Drawing.Bitmap> bitmaps)
        {
            string data = string.Empty;
            try
            {
                IronTesseract IronOcr = new IronTesseract(); //Только библиотека nuGet
                for (int i = 0; i < bitmaps.Count; i++)
                {
                    IronOcr.Language = OcrLanguage.RussianBest;

                    var Result = IronOcr.Read(bitmaps[i]);
                    data += Result.Text;
                }
                return data;
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                return data;
            }
        }

        public string Recognize(System.Drawing.Bitmap bitmap)
        {
            string data = string.Empty;
            try
            {
                IronTesseract IronOcr = new IronTesseract(); //Только библиотека nuGet
                IronOcr.Language = OcrLanguage.RussianBest;

                var Result = IronOcr.Read(bitmap);
                data = Result.Text;
                return data;
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                return data;
            }
        }
    } 
}
