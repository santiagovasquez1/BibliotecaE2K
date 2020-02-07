using System.Collections.Generic;
using System.Linq;
using B_Lectura_E2K.Entidades;
using B_Lectura_E2K.Entidades.Enumeraciones;
using BibliotecaE2K.Core;

namespace BibliotecaE2K
{
    public class Etabs2018 : AModeloEngine
    {
        public Etabs2018(string PathFile)
        {
            GetFile(PathFile);
            Modelo = new Modelo_Etabs();
            Modelo.Stories = GetStories();
            Modelo.Materials = GetMaterials();
            Modelo.ConcreteSections = GetFrameSections();
            Modelo.WallSections = GetWallSections();
        }
        public override List<IConcreteSection> GetFrameSections()
        {

            string[] dummy = { };
            string FrameName = "";
            string Temp_material = "";
            Material Material_dummy = null;
            int inicio = 0; int fin = 0;
            int indiceM = 11;
            int indiceB = 14;
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

            int inicio = 0; int fin = 0;

            inicio = ModeloFile.FindIndex(x => x.Contains("$ MATERIAL PROPERTIES")) + 1;
            fin = ModeloFile.FindIndex(x => x.Contains("$ REBAR DEFINITIONS")) - 2;

            var TempMaterials = ModeloFile.GetRange(inicio, fin - inicio).Select(x => x.Split()[4]).Distinct().ToList();
            ExtraerMateriales = new GetMaterialsEtabs2018();

            return ExtraerMateriales.ExtraerMateriales(inicio, fin, ModeloFile, TempMaterials);
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
            int inicio = ModeloFile.FindIndex(x => x.Contains("$ WALL PROPERTIES")) + 1;
            int fin = ModeloFile.FindIndex(x => x.Contains("$ LINK PROPERTIES")) - 2;

            ExtraerWallSections = new GetWallSectionsEtabs2018();
            return ExtraerWallSections.Get_Walls(ModeloFile, inicio, fin, Modelo);
        }

    }
}