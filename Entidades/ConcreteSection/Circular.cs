using System;
using B_Lectura_E2K.Entidades.Enumeraciones;

namespace B_Lectura_E2K.Entidades.ConcreteSection
{
    public class Circular : IConcreteSection
    {
        public Enum_Seccion _TipoSeccion { get; set; }
        public string _Name { get; set; }
        public double _Area { get; set; }
        public Material _Material { get; set; }
        public float B { get; set; }
        public float H { get; set; }
        public float R { get; set; }

        public Circular(string nombre, float b, Material material, Enum_Seccion shape)
        {
            _Name = nombre;
            R = b;
            _Material = material;
            _TipoSeccion = shape;
            CalcularArea();
        }

        public void CalcularArea()
        {
            _Area = Math.PI * Math.Pow(R, 2);
        }

        public override string ToString()
        {
            string Nombre_seccion;
            Nombre_seccion = $"C{R}{_Material.Material_name}";
            return Nombre_seccion;
        }
    }
}