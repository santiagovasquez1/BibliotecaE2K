using B_Lectura_E2K.Entidades.Enumeraciones;
using System.Collections.Generic;

namespace B_Lectura_E2K.Entidades
{
    public class Load_Combination
    {
        public string Name { get; set; }
        public Enum_Combination_Type Type { get; set; }
        public List<Load_Pattern> LoadCase { get; set; }

        public override string ToString()
        {
            return $"{Name} Type : {Type}";
        }
    }
}