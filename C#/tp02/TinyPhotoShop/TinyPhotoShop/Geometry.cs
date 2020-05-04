using System;
using System.Drawing;
using System.Globalization;

namespace TinyPhotoShop
{
    public class Geometry
    {
        public static Image Shift(Bitmap img, int x, int y)
        {
            Bitmap newbitmap = new Bitmap(img);

            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    newbitmap.SetPixel((j+x)%img.Width, (i+y)%img.Height, img.GetPixel(j,i));
                }
            }

            return newbitmap;
        }

        public static Image SymmetryHorizontal(Bitmap img)
        {
            Bitmap newbitmap = new Bitmap(img);
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    newbitmap.SetPixel(j,i,img.GetPixel(j, img.Height-i-1));
                }
            }

            return newbitmap;
        }
        public static Image SymmetryVertical(Bitmap img)
        {
            Bitmap newbitmap = new Bitmap(img);
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    newbitmap.SetPixel(j,i,img.GetPixel(img.Width-j-1,i));
                }
            }

            return newbitmap;
        }

        public static Image SymmetryPoint(Bitmap img, int x, int y)
        {
            Bitmap newbitmap = new Bitmap(img);
            SymmetryHorizontal(newbitmap);
            Bitmap newone = new Bitmap(newbitmap);
            SymmetryVertical(newone);
            return Shift(newone, x, y);
        }

        public static Image RotationLeft(Bitmap img)
        {
            Bitmap newbitmap = new Bitmap(img.Height,img.Width);
            Color init;
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    init = img.GetPixel(j, i);
                    newbitmap.SetPixel(i, img.Width - j - 1, init);
                }
            }

            return newbitmap;
            
        }

        public static Image RotationRight(Bitmap img)
        {
            Bitmap newbitmap = new Bitmap(img.Height,img.Width);
            Color init;
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    init = img.GetPixel(j, i);
                    newbitmap.SetPixel(img.Height-i-1, j, init);
                }
            }

            return newbitmap;
        }
        // hardcode de toutes les possibilités pour Resize
        //getneighbours renvoit la couleur(RGB) moyenne de chaque pixels
        //voisin du pixel passé en argument.
        public static Color Getneighbours(Bitmap img, int x, int y)
        {
            int red = 0;
            int green = 0;
            int blue = 0;
            
            if (x==0)
            {
                if (y==0)
                {
                    red = (img.GetPixel(x + 1, y).R + img.GetPixel(x + 1, y + 1).R + img.GetPixel(x, y + 1).R) / 3;
                    blue =(img.GetPixel(x + 1, y).B + img.GetPixel(x + 1, y + 1).B + img.GetPixel(x, y + 1).B) / 3;
                    green =(img.GetPixel(x + 1, y).G + img.GetPixel(x + 1, y + 1).G + img.GetPixel(x, y + 1).G) / 3;
                }
                else
                {
                    if (y==img.Height-1)
                    {
                        red = (img.GetPixel(x,y-1).R + img.GetPixel(x + 1, y - 1).R + img.GetPixel(x+1, y).R) / 3;
                        green =(img.GetPixel(x,y-1).G + img.GetPixel(x + 1, y - 1).G + img.GetPixel(x+1, y).G) / 3;
                        blue =(img.GetPixel(x,y-1).B + img.GetPixel(x + 1, y - 1).B + img.GetPixel(x+1, y).B) / 3;
                    }
                    else
                    {
                        red = (img.GetPixel(x, y - 1).R + img.GetPixel(x + 1, y - 1).R + img.GetPixel(x + 1, y).R +
                               img.GetPixel(x + 1, y + 1).R + img.GetPixel(x, y + 1).R) / 5;
                        green =(img.GetPixel(x, y - 1).G + img.GetPixel(x + 1, y - 1).G + img.GetPixel(x + 1, y).G +
                                img.GetPixel(x + 1, y + 1).G + img.GetPixel(x, y + 1).G) / 5;
                        blue =(img.GetPixel(x, y - 1).B + img.GetPixel(x + 1, y - 1).B + img.GetPixel(x + 1, y).B +
                               img.GetPixel(x + 1, y + 1).B + img.GetPixel(x, y + 1).B) / 5;
                    }
                }
            }
            else
            {
                if (x==img.Width-1)
                {
                    if (y==0)
                    {
                        red = (img.GetPixel(x - 1, y).R + img.GetPixel(x - 1, y + 1).R + img.GetPixel(x, y + 1).R) / 3;
                        green =(img.GetPixel(x - 1, y).G + img.GetPixel(x - 1, y + 1).G + img.GetPixel(x, y + 1).G) / 3;
                        blue =(img.GetPixel(x - 1, y).B + img.GetPixel(x - 1, y + 1).B+ img.GetPixel(x, y + 1).B) / 3;
                    }
                    else
                    {
                        if (y==img.Height-1)
                        {
                            red = (img.GetPixel(x - 1, y).R + img.GetPixel(x - 1, y - 1).R + img.GetPixel(x, y - 1).R) / 3;
                            green =(img.GetPixel(x - 1, y).G + img.GetPixel(x - 1, y - 1).G + img.GetPixel(x, y - 1).G) / 3;
                            blue =(img.GetPixel(x - 1, y).B + img.GetPixel(x - 1, y - 1).B + img.GetPixel(x, y - 1).B) / 3;
                        }
                        else
                        {
                            red =(img.GetPixel(x,y-1).R + img.GetPixel(x-1,y-1).R + img.GetPixel(x -1,y).R +
                             img.GetPixel(x -1,y+1).R + img.GetPixel(x,y+1).R) / 5;
                            green = (img.GetPixel(x,y-1).G + img.GetPixel(x-1,y-1).G + img.GetPixel(x -1,y).G +
                                     img.GetPixel(x -1,y+1).G + img.GetPixel(x,y+1).G) / 5;
                            blue =(img.GetPixel(x,y-1).B + img.GetPixel(x-1,y-1).B + img.GetPixel(x -1,y).B +
                                   img.GetPixel(x -1,y+1).B + img.GetPixel(x,y+1).B) / 5;
                        }
                    }
                }
                else
                {
                    if ((x>0 && x<img.Width-1)&&(y==0))
                    {
                       red= (img.GetPixel(x-1,y).R + img.GetPixel(x -1, y + 1).R + img.GetPixel(x, y+1).R +
                         img.GetPixel(x+1,y+1).R + img.GetPixel(x+1,y).R) / 5;
                        green = (img.GetPixel(x-1,y).G + img.GetPixel(x -1, y + 1).G + img.GetPixel(x, y+1).G +
                                 img.GetPixel(x+1,y+1).G + img.GetPixel(x+1,y).G) / 5;
                        blue= (img.GetPixel(x-1,y).B + img.GetPixel(x -1, y + 1).B + img.GetPixel(x, y+1).B +
                               img.GetPixel(x+1,y+1).B + img.GetPixel(x+1,y).B) / 5;
                    }
                    else
                    {
                        if ((x>0 && x<img.Width-1)&&(y==img.Height-1))
                        {
                           red= (img.GetPixel(x-1,y).R + img.GetPixel(x - 1, y - 1).R + img.GetPixel(x, y-1).R +
                             img.GetPixel(x + 1, y-1).R + img.GetPixel(x+1,y).R) / 5;
                            green =(img.GetPixel(x-1,y).G + img.GetPixel(x - 1, y - 1).G + img.GetPixel(x, y-1).G +
                                    img.GetPixel(x + 1, y-1).G + img.GetPixel(x+1,y).G) / 5;
                            blue =(img.GetPixel(x-1,y).B + img.GetPixel(x - 1, y - 1).B + img.GetPixel(x, y-1).B +
                                   img.GetPixel(x + 1, y-1).B + img.GetPixel(x+1,y).B) / 5;
                        }
                        else
                        {
                            red = (img.GetPixel(x -1,y-1).R + img.GetPixel(x, y - 1).R + img.GetPixel(x+1, y - 1).R +
                                   img.GetPixel(x + 1, y).R + img.GetPixel(x + 1, y+1).R + img.GetPixel(x, y+1).R +
                                   img.GetPixel(x - 1, y+1).R + img.GetPixel(x - 1, y).R) / 8;
                            green=(img.GetPixel(x -1,y-1).G + img.GetPixel(x, y - 1).G + img.GetPixel(x+1, y - 1).G +
                                   img.GetPixel(x + 1, y).G + img.GetPixel(x + 1, y+1).G + img.GetPixel(x, y+1).G +
                                   img.GetPixel(x - 1, y+1).G + img.GetPixel(x - 1, y).G) / 8;
                            blue=(img.GetPixel(x -1,y-1).B + img.GetPixel(x, y - 1).B + img.GetPixel(x+1, y - 1).B +
                                  img.GetPixel(x + 1, y).B + img.GetPixel(x + 1, y+1).B + img.GetPixel(x, y+1).B +
                                  img.GetPixel(x - 1, y+1).B + img.GetPixel(x - 1, y).B) / 8;
                        }
                    }
                }
            }

            return Color.FromArgb(red, green, blue);
        }
        
        public static Image Resize(Bitmap img, int x, int y)
        {
            Bitmap resize = new Bitmap(x,y);

            int coefficientx = img.Width/x;
            int coefficienty = img.Height/y;

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (coefficientx*j%1== 0 && coefficienty*i%1 == 0)
                    {
                        resize.SetPixel(j, i ,img.GetPixel(j*coefficientx,i*coefficienty));
                    }
                    else
                    {
                        resize.SetPixel(j,i, Getneighbours(img, (j*coefficientx)/1,(i*coefficienty)/1));
                    }
                }
            }

            return resize;
        }
    }
}