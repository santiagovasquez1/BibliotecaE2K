using System;
using System.Collections.Generic;

namespace B_Lectura_E2K.Entidades
{
    public class MPoint
    {
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public List<Story> Stories { get; set; }

        public MPoint(string name, double x1, double y1, double z1)
        {
            Name = name;
            X = x1;
            Y = y1;
            Z = z1;
        }

        public override string ToString()
        {
            return $"{Name} X:{Math.Round(X, 2)}, Y:{Math.Round(Y, 2)}";
        }
    }
}