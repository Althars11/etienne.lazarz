using System;
using System.Drawing;

namespace TinyPhotoShop
{
    public class Convolution
    {
        private static int Clamp(float c)
        {
            int valeurretour;
            if (c<=0)
            {
                valeurretour = 0;
            }
            else
            {
                if (c>255)
                {
                    valeurretour = 255;
                }
                else
                {
                    valeurretour = (int) c;
                }
            }

            return valeurretour;
        }

        private static bool IsValid(int x, int y, Size size)
        {
            Bitmap test = new Bitmap(x,y);
            
            if (size!=test.Size)
            {
                return false;
            }

            return true;
        }
        //undone (no much more time to do it)
        public static Image Convolute(Bitmap img, float[,] mask)
        {
            Bitmap newone =new Bitmap(img.Width,img.Height);

            
            return newone;
        }
    }
}