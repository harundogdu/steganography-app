using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SiberGüvenlikOdev
{
    public static class Business
    {
        public static Bitmap Encode(this Bitmap image, List<Char> secretMessage, int key)
        {
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    if (j % key == 0)
                    {
                        Color temp = new Color();
                        if (secretMessage.Count > 0)
                        {
                            int encValue = (int)secretMessage[secretMessage.Count - 1];
                            secretMessage.RemoveAt(secretMessage.Count - 1);
                            temp = Color.FromArgb(encValue, 128, 0);
                        }
                        else
                        {
                            break;
                        }
                        image.SetPixel(j, i, temp);
                    }
                }
                if (secretMessage.Count == 0)
                {
                    break;
                }
            }
            return image;
        }
        public static String Decode(this Bitmap image, int key)
        {
            string decodedString = "";
            bool isDone = false;

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Color temp = image.GetPixel(j, i);
                    if (j % key == 0)
                    {
                        if (temp.G == 128) decodedString += (char)temp.R;
                        else
                        {
                            isDone = true;
                            break;
                        }
                    }
                }
                if (isDone) break;
            }
            decodedString = DataLayer.Reverse(decodedString);
            return decodedString;
        }
    }
}
