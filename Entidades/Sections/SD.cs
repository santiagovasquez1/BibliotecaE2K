using B_Lectura_E2K.Entidades.Enumeraciones;
namespace B_Lectura_E2K.Entidades.ConcreteSection
{
    public class SD : ISection
    {
        public Enum_Seccion _TipoSeccion { get; set; }
        public string _Name { get; set; }
        public double _Area { get; set; }
        public Material _Material { get; set; }
        public float B { get; set; }
        public float H { get; set; }
        public float TW { get; set; }
        public float TF { get; set; }

        public SD(string nombre, float b, float h, float tw, float tf, Material material, Enum_Seccion shape)
        {
            _Name = nombre;
            B = b;
            H = h;
            TW = tw;
            TF = tf;
            _Material = material;
            _TipoSeccion = shape;
            CalcularArea();
        }

        public void CalcularArea()
        {
            _Area = (H * TW) + ((B - TW) * TF);
        }

        public override string ToString()
        {
            string Nombre_seccion = "";
            if (_TipoSeccion == Enum_Seccion.Tee)
            {
                Nombre_seccion = $"T{B}X{H}X{TW}X{TF}{_Material.Material_name}";
            }
            else
            {
                Nombre_seccion = $"L{B}X{H}X{TW}X{TF}{_Material.Material_name}";
            }

            return Nombre_seccion;
        }

    }
}