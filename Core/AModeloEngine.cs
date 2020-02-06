using System.Collections.Generic;
using System.IO;
using B_Lectura_E2K.Entidades;
using BibliotecaE2K.Core;

namespace BibliotecaE2K
{
    public abstract class AModeloEngine
    {
        public Modelo_Etabs Modelo { get; set; }
        private List<string> modelofile;
        public List<string> ModeloFile
        {
            get { return modelofile; }
            set { modelofile = value; }
        }
        public IGetStories GetStory { get; set; }
        public AModeloEngine()
        {
            Modelo = new Modelo_Etabs();
            Modelo.Stories = GetStories();
            Modelo.Materials = GetMaterials();
            Modelo.ConcreteSections = GetFrameSections();
            Modelo.WallSections = GetWallSections();
        }
        protected void GetFile(string PathFile)
        {

            string sline;
            List<string> Temp = new List<string>();

            var Reader = new StreamReader(PathFile);
            do
            {
                sline = Reader.ReadLine();
                Temp.Add(sline);
            } while (!(sline == null));

            Reader.Close();

            ModeloFile = Temp;
        }
        public abstract List<Story> GetStories();
        public abstract List<Material> GetMaterials();
        public abstract List<IConcreteSection> GetFrameSections();
        public abstract List<Wall_Section> GetWallSections();
    }
}