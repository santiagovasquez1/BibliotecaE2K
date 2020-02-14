namespace B_Lectura_E2K.Entidades
{
    public interface IFrameModel
    {
        string Name { get; set; }
        MPoint p1 { get; set; }
        MPoint p2 { get; set; }
    }
}