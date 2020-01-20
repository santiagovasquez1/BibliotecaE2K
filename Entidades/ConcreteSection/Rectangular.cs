using B_Lectura_E2K.Entidades.Enumeraciones;

namespace B_Lectura_E2K.Entidades.ConcreteSection
{
    public class Rectangular : IConcreteSection
    {
        public Enum_Seccion _TipoSeccion { get; set; }
        public string _Name { get; set; }
        public double _Area { get; set; }
        public Material _Material { get; set; }
        public float B { get; set; }
        public float H { get; set; }

        public Rectangular(string nombre, float b, float h, Material material, Enum_Seccion shape)
        {
            _Name = nombre;
            B = b;
            H = h;
            _Material = material;
            _TipoSeccion = shape;
            CalcularArea();
        }

        public void CalcularArea()
        {
            _Area = B * H;
        }

        public override string ToString()
        {
            string Nombre_seccion;
            Nombre_seccion = $"C{B}X{H}{_Material.Material_name}";
            return Nombre_seccion;
        }
    }
}