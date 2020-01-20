using B_Lectura_E2K.Entidades.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace B_Lectura_E2K.Entidades
{
    public class Load_Pattern
    {
        public string _Name { get; set; }
        public float Sf { get; set; }
        public Enum_LoadPattern Type { get; set; }

        public Load_Pattern(string name,Enum_LoadPattern type,float scalefactor=1)
        {
            _Name = name;
            Type = type;
            Sf = scalefactor;
        }

        public override string ToString()
        {
            return $"{_Name} SF : {Sf}";
        }

    }
}
