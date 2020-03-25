using B_Lectura_E2K.Entidades;
using B_Lectura_E2K.Entidades.Enumeraciones;
using System.Collections.Generic;

namespace B_Lectura_E2K.Core
{
    public class FrameFactoryEtabs2018 : IFrameFactory
    {
        public List<IFrameModel> CreateFrames(string line, List<MPoint> Points, List<string> LineAssigment, List<Story> Stories, List<ISection> Sections)
        {
            var Temp = line.Split();
            var Name = Temp[4].Replace("\"", "");
            var Type = Temp[6].Replace("\"", "");
            var point1 = Points.Find(x => x.Name == Temp[8].Replace("\"", ""));
            var point2 = Points.Find(x => x.Name == Temp[10].Replace("\"", ""));

            var Range = LineAssigment.FindAll(x => x.Contains($" {Name} "));
            IFrameModel frame = null;
            List<IFrameModel> Frames = new List<IFrameModel>();

            foreach (string LineAssign in LineAssigment)
            {
                var prop = LineAssign.Split();
                Story story = Stories.Find(x => x.StoryName == prop[6].Replace("\"", ""));
                ISection section = Sections.Find(x => x._Name == prop[9].Replace("\"", ""));

                if (section._Material.tipo_Material == Enum_Material.Concrete)
                    frame = new ConcreteFrameModel(Name, point1, point2, story, section);
                else if (section._Material.tipo_Material == Enum_Material.Steel)
                    frame = new SteelFrameModel(Name, point1, point2, story, section);

                if (Type == "COLUMN")
                    frame.Frame_Type = Enum_Frame_Type.COLUMN;
                else if (Type == "BEAM")
                    frame.Frame_Type = Enum_Frame_Type.BEAM;

                Frames.Add(frame);
            }

            return Frames;
        }
    }
}