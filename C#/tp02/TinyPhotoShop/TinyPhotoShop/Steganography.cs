using System;
using System.Drawing;
using System.IO;

namespace TinyPhotoShop
{
    public class Steganography
    {
        
                //Tool Box
        
        private const byte MostSignificantBitsMask = 240; 
        public static byte getMostsignificantPart(byte b)
        {
            return (byte) (b & MostSignificantBitsMask);
        }

        static Color GetMostSignificantPart(Color c)
        {
            byte newR = getMostsignificantPart(c.R);
            byte newG = getMostsignificantPart(c.G);
            byte newB = getMostsignificantPart(c.B);

            return Color.FromArgb(newR, newG, newB);
        }
        
        private const byte LeastSignificantPartMask = 15; // 0000 1111 en binaire

        static byte getLeastSignificantPart(byte b)
        {
            return (byte) (b & LeastSignificantPartMask);
        }
        static Color GetLeastSignificantPart(Color c)
        { 
            byte newR = (byte)(c.R & LeastSignificantPartMask);
            byte newG = (byte)(c.G & LeastSignificantPartMask);
            byte newB = (byte)(c.B & LeastSignificantPartMask);

            return Color.FromArgb(newR, newG, newB);
        }

        static Color ColorOr(Color c1 , Color c2)
        {
            byte resR = (byte) (c1.R | c2.R);
            byte resG = (byte) (c1.G | c2.G);
            byte resB = (byte) (c1.B | c2.B);

            return Color.FromArgb(resR, resG, resB);
        }

        static Color ShiftMostSignificantBits(Color c)
        {
            return Color.FromArgb(c.R >> 4, c.B >> 4, c.R >> 4);
        }
        
        static Color ShiftLeastSignificantBits(Color c)
        {
            return Color.FromArgb(c.R << 4, c.B << 4, c.R << 4);
        }
                
                //Fin ToolBox
        
                
                //3.1.1
        public static Image EncryptImage(Bitmap img, Bitmap enc)
        {
            if (( enc.Width <= img.Width) && (enc.Height <= img.Height) )
            {
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        img.SetPixel(j, i ,ColorOr(GetMostSignificantPart(img.GetPixel(j,i)),ShiftMostSignificantBits(GetMostSignificantPart(enc.GetPixel(j,i)))));
                    }
                }   
            }
            return img;
        }

        public static Image DecryptImage(Bitmap img)
        {
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    img.SetPixel(j, i,ColorOr(GetMostSignificantPart(img.GetPixel(j, i)),ShiftLeastSignificantBits(GetLeastSignificantPart(img.GetPixel(j, i)))));
                }
            }

            return img;
        }
        
            //3.1.2

        public static Image Encrypttext(Bitmap img, string text)
        {
            byte charbyte;
            int squaretextlen = text.Length * 2;
            int bufflen = img.Height * img.Width;
            int[] buffer = new int[bufflen];
            
            for (int i = 0; i < bufflen; i+=2)
            {
                if (i<text.Length)
                {
                    charbyte = Convert.ToByte(text[i]);
                    buffer[i] = getMostsignificantPart(charbyte)>>4;
                    buffer[i + 1] = getLeastSignificantPart(charbyte);
                }
                else
                {
                    buffer[i] = 0;
                    buffer[i + 1] = 0;
                }
            }
            
            int stockbuffer = 0;

            if (img.Width*img.Height <= squaretextlen)
            {
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        byte newR = (byte)buffer[stockbuffer];
                        stockbuffer++;
                        byte newG = (byte) buffer[stockbuffer];
                        stockbuffer++;
                        byte newB = (byte) buffer[stockbuffer];
                        stockbuffer++;
                        img.SetPixel(j, i, Color.FromArgb(newR,newG,newB));
                        img.SetPixel(j, i , ColorOr(GetMostSignificantPart(img.GetPixel(j,i)),GetLeastSignificantPart(Color.FromArgb(newR,newG,newB))));
                    }
                }
            }
            return img;
        }

        public static string DecryptText(Bitmap img)
        {
            string text = "";
            int[] buffer = new int[img.Height*img.Width*3];
            Color c;
            int stockbuffer = 0;
            
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    c = img.GetPixel(j, i);
                    buffer[stockbuffer] = (byte)(c.R & LeastSignificantPartMask);
                    stockbuffer++;
                    buffer[stockbuffer] = (byte)(c.G & LeastSignificantPartMask);
                    stockbuffer++;
                    buffer[stockbuffer] = (byte)(c.B & LeastSignificantPartMask);
                    stockbuffer++;
                }
            }

            int compteur = 0;
            while (compteur<buffer.Length && buffer[compteur]!=0)
            {
                text += (char) (buffer[compteur] + buffer[compteur + 1]);
                
                compteur++;
            }
            return text;
        }
    }
}