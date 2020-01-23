using System.Collections.Generic;

namespace B_Lectura_E2K.Entidades
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public List<Story> Stories { get; set; }
    }
}