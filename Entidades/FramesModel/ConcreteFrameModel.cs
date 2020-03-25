using B_Lectura_E2K.Entidades.Enumeraciones;

namespace B_Lectura_E2K.Entidades
{
    public class ConcreteFrameModel : IFrameModel
    {
        public string Name { get; set; }
        public ISection Section { get; set; }
        public MPoint p1 { get; set; }
        public MPoint p2 { get; set; }
        public Enum_Frame_Type Frame_Type { get; set; }
        public Story Story { get; set; }
        public ConcreteFrameModel(string name, MPoint i1, MPoint i2,Story story,ISection section)
        {
            Name = name;
            p1 = i1;
            p2 = i2;
            Story = story;
            Section = section;
        }
    }
}
