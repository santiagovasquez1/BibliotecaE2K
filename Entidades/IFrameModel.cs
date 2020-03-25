using B_Lectura_E2K.Entidades.Enumeraciones;

namespace B_Lectura_E2K.Entidades
{
    public interface IFrameModel
    {
        string Name { get; set; }
        MPoint p1 { get; set; }
        MPoint p2 { get; set; }
        Enum_Frame_Type Frame_Type { get; set; }
        Story Story { get; set; }
        ISection Section { get; set; }
    }
}