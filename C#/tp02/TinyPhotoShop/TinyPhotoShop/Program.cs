using System;
using System.Drawing;
using System.Linq.Expressions;

namespace TinyPhotoShop
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Bitmap img = new Bitmap( "C:\\Users\\etien\\Desktop\\G0053403.jpg");
            Geometry.RotationRight(img); 
        }
    }
}