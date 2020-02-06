using System.Collections.Generic;
using B_Lectura_E2K.Entidades;
using B_Lectura_E2K.Entidades.Enumeraciones;

namespace BibliotecaE2K
{
    public abstract class Etabs2018 : AModeloEngine
    {
        public override List<IConcreteSection> GetFrameSections()
        {
            return null;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override List<Material> GetMaterials()
        {
            return null;
        }

        public override List<Story> GetStories()
        {
            var Stories = new List<Story>();
            Story Storyi = null;
            int inicio = 0; int fin = 0;
            float Elevation = 0f;

            inicio = ModeloFile.FindIndex(x => x.Contains("$ STORIES - IN SEQUENCE FROM TOP")) + 1;
            fin = ModeloFile.FindIndex(x => x.Contains("$ GRIDS")) - 2;

            // if (Modelo.Version == Version_Etabs.ETABS2018)

            // else
            //     fin = ModeloFile.FindIndex(x => x.Contains("$ DIAPHRAGM NAMES")) - 2;

            return ExtraerStories(Stories, ref Storyi, inicio, fin, ref Elevation);
        }

        private List<Story> ExtraerStories(List<Story> Stories, ref Story Storyi, int inicio, int fin, ref float Elevation)
        {
            for (int i = fin; i >= inicio; i--)
            {
                var Temp = ModeloFile[i].Split();
                string Story_name = Temp[3].Replace("\"", "");
                string Story_Height = Temp[6];
                Elevation += float.Parse(Story_Height);

                Storyi = new Story(Story_name, float.Parse(Story_Height));
                Storyi.StoryElevation = Elevation;
                Stories.Add(Storyi);
            }

            return Stories;
        }

        public override List<Wall_Section> GetWallSections()
        {
            throw new System.NotImplementedException();
        }


    }
}