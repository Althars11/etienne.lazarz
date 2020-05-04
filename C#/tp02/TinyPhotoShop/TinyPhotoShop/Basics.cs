using System;
using System.Drawing;

namespace TinyPhotoShop
{
    public class Basics
    {
        //Exercice 2
        //Palier 0:Manipulation dde pixels
        
        public static Color Grey(Color c)
        {
            int rgb = (int) (c.R + c.G + c.B) / 3;
            return Color.FromArgb(rgb, rgb, rgb);
        }

        public static Color Binarize(Color c)
        {
            int rgb = (int) (c.R + c.G + c.B) / 3;
            if (rgb>(255/2))
            {
                return Color.White;
            }
            else
            {
                return Color.Black;
            }
        }

        public static Color BinarizeColor(Color c)
        {
            int[] color = {c.R,c.G,c.B};
            for (int i = 0; i < color.Length; i++)
            {
                if (color[i]>(255/2))
                {
                    color[i] = 255;
                }
                else
                {
                    color[i] = 0;
                }
            }
            return Color.FromArgb(color[0],color[1],color[2]);
        }

        public static Color Negative(Color c)
        {
            return Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B);
        }

        public static Color Tinter(Color c, Color c1)
        {
            return Color.FromArgb((c.R + c1.R)/2, (c.G + c1.G)/2, (c.B + c1.B)/2);
        }
    }
}