using System.Collections.Generic;
using System.Linq;
using B_Lectura_E2K.Entidades;

namespace BibliotecaE2K.Core
{
    public class Etabs95 : AModeloEngine
    {
        public override List<IConcreteSection> GetFrameSections()
        {
            string[] dummy = { };
            string FrameName = "";
            string Temp_material = "";
            Material Material_dummy = null;
            int inicio = 0; int fin = 0;
            int indiceM = 10;
            int indiceB = 13;
            IConcreteSection framei = null;
            List<IConcreteSection> concreteSections = new List<IConcreteSection>();

            inicio = ModeloFile.FindIndex(x => x.Contains("$ FRAME SECTIONS")) + 1;
            fin = ModeloFile.FindIndex(x => x.Contains("$ CONCRETE SECTIONS")) - 2;

            ExtraerFrameSections = new GetFrameSectionsEtabs();

            return ExtraerFrameSections.GetConcreteFrameSection(dummy, FrameName, Temp_material,
                                                         Material_dummy, inicio, fin, framei,
                                                         indiceM, indiceB, Modelo, ModeloFile);
        }

        public override List<Material> GetMaterials()
        {
            int inicio = ModeloFile.FindIndex(x => x.Contains("$ MATERIAL PROPERTIES")) + 1;
            int fin = ModeloFile.FindIndex(x => x.Contains("$ FRAME SECTIONS")) - 2;

            var TempMaterials = ModeloFile.GetRange(inicio, fin - inicio).Select(x => x.Split()[4]).Distinct().ToList();

            ExtraerMateriales = new GetMaterialsEtabs95();
            return ExtraerMateriales.ExtraerMateriales(inicio, fin, ModeloFile, TempMaterials);
        }

        public override List<Story> GetStories()
        {
            var Stories = new List<Story>();
            Story Storyi = null;
            int inicio = 0; int fin = 0;
            float Elevation = 0f;

            inicio = ModeloFile.FindIndex(x => x.Contains("$ STORIES - IN SEQUENCE FROM TOP")) + 1;
            fin = ModeloFile.FindIndex(x => x.Contains("$ DIAPHRAGM NAMES")) - 2;

            IGetStory = new GetStoriesEtabs();
            return IGetStory.ExtraerStories(Stories, ModeloFile, ref Storyi, inicio, fin, ref Elevation);
        }

        public override List<Wall_Section> GetWallSections()
        {
            int inicio = ModeloFile.FindIndex(x => x.Contains("$ WALL/SLAB/DECK PROPERTIES")) + 1;
            int fin = ModeloFile.FindIndex(x => x.Contains("$ LINK PROPERTIES")) - 2;

            ExtraerWallSections = new GetWallSectionsEtabs95();
            return ExtraerWallSections.Get_Walls(ModeloFile, inicio, fin, Modelo);

        }
    }
}