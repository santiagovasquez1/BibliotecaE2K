using System.Collections.Generic;

namespace B_Lectura_E2K.Entidades
{
    public class Point
    {
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public List<Story> Stories { get; set; }

         public Point(string name,double x1,double y1)
        {
            Name = name;
            X = x1;
            Y = y1;
        }
    }
}