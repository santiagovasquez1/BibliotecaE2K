using System;
using System.Collections.Generic;
using System.Text;

namespace B_Lectura_E2K.Entidades
{
    public class AreaModel
    {
        public string Name { get; set; }
        public Wall_Section section { get; set; }
        public List<Point> Points { get; set; }

    }
}
