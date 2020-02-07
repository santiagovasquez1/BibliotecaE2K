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
        protected List<string> ModeloFile
        {
            get { return modelofile; }
            set { modelofile = value; }
        }
        protected IGetStories IGetStory { get; set; }
        protected IGetMaterial ExtraerMateriales { get; set; }
        protected IGetFrameSections ExtraerFrameSections { get; set; }
        protected IGetWallSections ExtraerWallSections { get; set; }
        protected AModeloEngine()
        {

        }
        public void GetFile(string PathFile)
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