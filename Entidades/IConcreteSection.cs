using B_Lectura_E2K.Entidades.Enumeraciones;

namespace B_Lectura_E2K.Entidades
{
    public interface IConcreteSection
    {
        Enum_Seccion _TipoSeccion { get; set; }
        string _Name { get; set; }
        double _Area { get; set; }
        Material _Material { get; set; }
        float B { get; set; }
        float H { get; set; }

        void CalcularArea();
    }
}