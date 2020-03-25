using System.Collections.Generic;

namespace B_Lectura_E2K.Entidades
{
    public class Modelo_Etabs
    {
        public Version_Etabs Version { get; set; }
        public List<Story> Stories { get; set; }
        public List<Material> Materials { get; set; }
        public List<ISection> Sections { get; set; }
        public List<Wall_Section> WallSections { get; set; }
        public List<MPoint> Points { get; set; }
        public List<AreaModel> Shells { get; set; }
        public List<IFrameModel> Frames { get; set; }
    }
}