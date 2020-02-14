using System.Collections.Generic;

namespace B_Lectura_E2K.Entidades
{
    public class AreaModel
    {
        public string Name { get; set; }
        public Wall_Section section { get; set; }
        public List<MPoint> Points { get; set; }

    }
}
