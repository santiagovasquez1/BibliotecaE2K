namespace B_Lectura_E2K.Entidades
{
    public class SteelFrameModel : IFrameModel
    {
        public string Name { get; set; }
        public IConcreteSection section { get; set; }
        public MPoint p1 { get; set; }
        public MPoint p2 { get; set; }

        public SteelFrameModel(string name, MPoint i1, MPoint i2)
        {
            Name = name;
            p1 = i1;
            p2 = i2;
        }
    }
}
