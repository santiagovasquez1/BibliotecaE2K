using System.Collections.Generic;
using System.Linq;
using B_Lectura_E2K.Entidades;
using B_Lectura_E2K.Entidades.Enumeraciones;
using BibliotecaE2K.Core;

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
            var materials = new List<Material>();
            int inicio = 0; int fin = 0;
            string resist_material = "";
            string Material_E = "";
            Enum_Material tipomaterial = Enum_Material.Concrete;

            inicio = ModeloFile.FindIndex(x => x.Contains("$ MATERIAL PROPERTIES")) + 1;
            fin = ModeloFile.FindIndex(x => x.Contains("$ REBAR DEFINITIONS")) - 2;
            
            var ModeloRange = ModeloFile.GetRange(inicio, fin - inicio).Select(x => x.Split()[4]).Distinct().ToList();

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

            IGetStory = new GetStoriesEtabs();
            return IGetStory.ExtraerStories(Stories, ModeloFile, ref Storyi, inicio, fin, ref Elevation);
        }
        public override List<Wall_Section> GetWallSections()
        {
            throw new System.NotImplementedException();
        }


    }
}