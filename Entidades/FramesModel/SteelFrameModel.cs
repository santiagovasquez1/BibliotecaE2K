using System;
using System.Collections.Generic;
using System.Text;

namespace B_Lectura_E2K.Entidades
{
    public class SteelFrameModel:IFrameModel
    {
        public string Name { get; set; }
        public IConcreteSection section { get; set; }
        public Point p1 { get; set; }
        public Point p2 { get; set; }

        public SteelFrameModel(string name,Point i1,Point i2)
        {
            Name = name;
            p1 = i1;
            p2 = i2;
        }
    }
}
