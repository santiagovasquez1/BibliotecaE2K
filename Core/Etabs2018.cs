using B_Lectura_E2K.Core;
using B_Lectura_E2K.Entidades;
using BibliotecaE2K.Core;
using System.Collections.Generic;
using System.Linq;

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
            Modelo.Sections = GetFrameSections();
            Modelo.WallSections = GetWallSections();
            Modelo.Points = GetPoints();
            CreateFrames();
        }

        public override void CreateFrames()
        {
            int inicio = ModeloFile.FindIndex(x => x.Contains("$ LINE CONNECTIVITIES")) + 1;
            int fin = ModeloFile.FindIndex(x => x.Contains("$ AREA CONNECTIVITIES")) - 2;

            var TempLines = ModeloFile.GetRange(inicio, fin - inicio).ToList();

            int inicio2 = ModeloFile.FindIndex(x => x.Contains("$ LINE ASSIGNS")) + 1;
            int fin2 = ModeloFile.FindIndex(x => x.Contains("$ AREA ASSIGNS")) - 2;
            var TempAssigns = ModeloFile.GetRange(inicio2, fin2 - inicio2).ToList();

            FrameFactory = new FrameFactoryEtabs2018();

            foreach (string templine in TempLines)
            {
                var Framei = FrameFactory.CreateFrames(templine, Modelo.Points, TempAssigns, Modelo.Stories, Modelo.Sections);
                Modelo.Frames.AddRange(Framei);
            }
        }

        public override List<ISection> GetFrameSections()
        {
            string[] dummy = { };
            string FrameName = "";
            string Temp_material = "";
            Material Material_dummy = null;
            int inicio = 0; int fin = 0;
            int indiceM = 11;
            int indiceB = 14;
            ISection framei = null;
            List<ISection> concreteSections = new List<ISection>();

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

        public override List<MPoint> GetPoints()
        {
            int inicio = ModeloFile.FindIndex(x => x.Contains("$ POINT COORDINATES")) + 1;
            int Fin = ModeloFile.FindIndex(x => x.Contains("$ LINE CONNECTIVITIES")) - 2;
            var RangePoints = ModeloFile.GetRange(inicio, Fin - inicio);

            ExtaerPuntos = new GetPointsEtabs();
            return ExtaerPuntos.ExtraerPuntos(RangePoints);
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