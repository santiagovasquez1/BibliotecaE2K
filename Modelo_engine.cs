using B_Lectura_E2K.Entidades;
using B_Lectura_E2K.Entidades.ConcreteSection;
using B_Lectura_E2K.Entidades.Enumeraciones;
using System.Collections.Generic;
using System.Linq;

namespace B_Lectura_E2K
{
    public sealed class Modelo_engine
    {
        public Modelo_Etabs modelo { get; set; }
        public List<string> E2KFile { get; private set; }

        public Modelo_engine()
        {
        }

        public void Inicializar(List<string> E2KString)
        {
            E2KFile = E2KString;
            modelo = new Modelo_Etabs();
            modelo.Version = GetVersion();
            modelo.Stories = GetStories();
            modelo.Materials = GetMaterials();
            modelo.ConcreteSections = GetFrameSections();
            modelo.WallSections = GetWallSections();
        }

        private Version_Etabs GetVersion()
        {
            int _start = 0;

            _start = E2KFile.FindIndex(x => x.Contains("$ PROGRAM INFORMATION")) + 1;

            if (E2KFile[_start].Contains("9.5.0"))
            {
                return Version_Etabs.ETABS9;
            }
            else
            {
                return Version_Etabs.ETABS2018;
            }
        }

        #region Listas del modelo

        private List<Story> GetStories()
        {
            var Stories = new List<Story>();
            Story Storyi = null;
            int inicio = 0; int fin = 0;
            float Elevation = 0f;

            inicio = E2KFile.FindIndex(x => x.Contains("$ STORIES - IN SEQUENCE FROM TOP")) + 1;

            if (modelo.Version == Version_Etabs.ETABS2018)
                fin = E2KFile.FindIndex(x => x.Contains("$ GRIDS")) - 2;
            else
                fin = E2KFile.FindIndex(x => x.Contains("$ DIAPHRAGM NAMES")) - 2;

            for (int i = fin; i >= inicio; i--)
            {
                var Temp = E2KFile[i].Split();
                string Story_name = Texto_sub(Temp, 3, 34);
                string Story_Height = Temp[6];
                Elevation += float.Parse(Story_Height);

                Storyi = new Story(Story_name, float.Parse(Story_Height));
                Storyi.StoryElevation = Elevation;
                Stories.Add(Storyi);
            }

            return Stories;
        }

        private List<Material> GetMaterials()
        {
            var materials = new List<Material>();
            int inicio = 0; int fin = 0;
            string resist_material = "";
            string Material_E = "";
            Enum_Material tipomaterial = Enum_Material.Concrete;

            inicio = E2KFile.FindIndex(x => x.Contains("$ MATERIAL PROPERTIES")) + 1;

            if (modelo.Version == Version_Etabs.ETABS2018)
                fin = E2KFile.FindIndex(x => x.Contains("$ REBAR DEFINITIONS")) - 2;
            else
                fin = E2KFile.FindIndex(x => x.Contains("$ FRAME SECTIONS")) - 2;

            if (modelo.Version == Version_Etabs.ETABS9)
            {
                GetMaterial95(materials, inicio, fin, ref resist_material, ref Material_E, ref tipomaterial);
            }
            else
            {
                string Material_name = "";
                int Pos = 0;
                var prueba = E2KFile.GetRange(inicio, fin - inicio).Select(x => x.Split()[4]).Distinct().ToList();

                foreach (string texto in prueba)
                {
                    GetMaterial2018(inicio, fin, ref resist_material, ref Material_E, ref tipomaterial, ref Material_name, out Pos, texto);

                    var materiali = new Material(Material_name, float.Parse(Material_E), float.Parse(resist_material))
                    {
                        tipo_Material = tipomaterial
                    };

                    materials.Add(materiali);
                }
            }

            return materials;
        }

        private List<IConcreteSection> GetFrameSections()
        {
            string[] dummy = { };
            string FrameName = "";
            string Temp_material = "";
            Material Material_dummy = null;
            int inicio = 0; int fin = 0;
            IConcreteSection framei = null;
            List<IConcreteSection> concreteSections = new List<IConcreteSection>();
            
            inicio = E2KFile.FindIndex(x => x.Contains("$ FRAME SECTIONS")) + 1;

            if (modelo.Version == Version_Etabs.ETABS2018)
            {
                GetConcreteSection(dummy, FrameName, Temp_material, Material_dummy, inicio, fin, framei, concreteSections, 11, 14);
            }
            else
            {
                GetConcreteSection(dummy, FrameName, Temp_material, Material_dummy, inicio, fin, framei, concreteSections, 10, 13);
            }

            return concreteSections;
        }

        private List<Wall_Section> GetWallSections()
        {
            int inicio = 0; int fin = 0;
            string[] dummy = { };
            string Temp_material = "";
            string WallSectionName = "";
            float bw = 0;
            Material Material_dummy = null;
            Wall_Section walli = null;
            List<Wall_Section> Temp_wall = new List<Wall_Section>();

            if (modelo.Version == Version_Etabs.ETABS2018)
                GetWalls2018(inicio, fin, dummy, Temp_material, WallSectionName, bw, Material_dummy, walli, Temp_wall);
            else
                GetWalls95(inicio, fin, dummy, Temp_material, WallSectionName, bw, Material_dummy, walli, Temp_wall);

            return Temp_wall;
        }

        private void GetWalls95(int inicio, int fin, string[] dummy, string Temp_material, string WallSectionName, float bw, Material Material_dummy, Wall_Section walli, List<Wall_Section> Temp_wall)
        {
            inicio = E2KFile.FindIndex(x => x.Contains("$ WALL/SLAB/DECK PROPERTIES")) + 1;
            fin = E2KFile.FindIndex(x => x.Contains("$ LINK PROPERTIES")) - 2;
            var Temp = E2KFile.GetRange(inicio, fin - inicio).ToList();

            foreach (string Linea in Temp)
            {
                dummy = Linea.Split();

                if (dummy[10].Replace("\"", "").ToUpper().Contains("WALL"))
                {
                    WallSectionName = dummy[4].Replace("\"", "");
                    Temp_material = dummy[7].Replace("\"", "");
                    bw = float.Parse(dummy[19]);

                    if (modelo.Materials.Exists(x => x.Material_name == Temp_material))
                    {
                        var prueba = from Material materiali in modelo.Materials
                                     where materiali.Material_name == Temp_material
                                     select materiali;

                        Material_dummy = prueba.FirstOrDefault();
                    }

                    walli = new Wall_Section(WallSectionName, Material_dummy, bw);
                    Temp_wall.Add(walli);
                }
            }
        }

        private void GetWalls2018(int inicio, int fin, string[] dummy, string Temp_material, string WallSectionName, float bw, Material Material_dummy, Wall_Section walli, List<Wall_Section> Temp_wall)
        {
            inicio = E2KFile.FindIndex(x => x.Contains("$ WALL PROPERTIES")) + 1;
            fin = E2KFile.FindIndex(x => x.Contains("$ LINK PROPERTIES")) - 2;
            var Temp = E2KFile.GetRange(inicio, fin - inicio).ToList();

            foreach (string Linea in Temp)
            {
                dummy = Linea.Split();
                WallSectionName = dummy[4].Replace("\"", "");
                Temp_material = dummy[11].Replace("\"", "");
                bw = float.Parse(dummy[17]);

                if (modelo.Materials.Exists(x => x.Material_name == Temp_material))
                {
                    var prueba = from Material materiali in modelo.Materials
                                 where materiali.Material_name == Temp_material
                                 select materiali;

                    Material_dummy = prueba.FirstOrDefault();
                }

                walli = new Wall_Section(WallSectionName, Material_dummy, bw);
                Temp_wall.Add(walli);
            }
        }

        #endregion Listas del modelo

        private void GetConcreteSection(string[] dummy, string FrameName, string Temp_material, Material Material_dummy, int inicio, int fin, IConcreteSection framei, List<IConcreteSection> concreteSections, int indiceM, int indiceB)
        {
            fin = E2KFile.FindIndex(x => x.Contains("$ CONCRETE SECTIONS")) - 2;
            var Temp = E2KFile.GetRange(inicio, fin - inicio).FindAll(x => x.Contains(" MATERIAL "));

            foreach (string Linea in Temp)
            {
                dummy = Linea.Split();
                Temp_material = dummy[7].Replace("\"", "");

                if (modelo.Materials.Exists(x => x.Material_name == Temp_material))
                {
                    var prueba = from Material materiali in modelo.Materials
                                 where materiali.Material_name == Temp_material
                                 select materiali;

                    Material_dummy = prueba.FirstOrDefault();

                    if (Material_dummy.tipo_Material == Enum_Material.Concrete)

                    {
                        FrameName = dummy[4].Replace("\"", "");
                        var FrameSection = dummy[indiceM].ToLower().Replace("\"", "");
                        float h, b;

                        switch (FrameSection)
                        {
                            case "rectangular":
                                h = float.Parse(dummy[indiceB]);
                                b = float.Parse(dummy[16]);
                                framei = new Rectangular(FrameName, b, h, Material_dummy, Enum_Seccion.Rectangular);
                                break;

                            case "circular":
                                h = float.Parse(dummy[indiceB]);
                                framei = new Circular(FrameName, h, Material_dummy, Enum_Seccion.Circular);
                                break;

                            //No hacer nada por el momento hasta saber como lo modelan
                            case "sd":
                                break;
                        }
                        concreteSections.Add(framei);
                    }
                }
            }
        }

        private void GetMaterial95(List<Material> materials, int inicio, int fin, ref string resist_material, ref string Material_E, ref Enum_Material tipomaterial)
        {
            for (int i = inicio; i < fin; i += 2)
            {
                var Temp1 = E2KFile[i].Split();
                var Temp2 = E2KFile[i + 1].Split();
                var Materialname = Texto_sub(Temp1, 4, 34);
                Material_E = Temp1[16];

                if (Temp2[7].Contains("STEEL") | Temp2[7].Contains("CONCRETE"))
                {
                    if (Temp2[4].Contains("STEEL"))
                    {
                        tipomaterial = Enum_Material.Steel;
                        resist_material = Temp2[10];
                    }
                    else
                    {
                        tipomaterial = Enum_Material.Concrete;
                        resist_material = Temp2[13];
                    }

                    var materiali = new Material(Materialname, float.Parse(Material_E), float.Parse(resist_material))
                    {
                        tipo_Material = tipomaterial
                    };
                    materials.Add(materiali);
                }
            }
        }

        private void GetMaterial2018(int inicio, int fin, ref string resist_material, ref string Material_E, ref Enum_Material tipomaterial, ref string Material_name, out int Pos, string texto)
        {
            Pos = texto.LastIndexOf((char)34);
            Material_name = texto.Substring(1, Pos - 1);
            string Aux = Material_name;
            var temp = E2KFile.GetRange(inicio, fin - inicio).FindAll(x => x.Contains($"{(char)34}{Aux}{(char)34}")).ToList();
            string[] dummy = { };
            foreach (string E2KLine in temp)
            {
                if (E2KLine.Contains(" TYPE "))
                {
                    dummy = E2KLine.ToUpper().Split();

                    if (dummy[9].Contains("STEEL") | dummy[9].Contains("CONCRETE"))
                    {
                        if (dummy[9].Contains("STEEL"))
                            tipomaterial = Enum_Material.Steel;
                        else
                            tipomaterial = Enum_Material.Concrete;
                    }
                }
                if (E2KLine.Contains(" E "))
                {
                    dummy = E2KLine.ToUpper().Split();
                    Material_E = dummy[12];
                }
                if (E2KLine.Contains(" FY ") | E2KLine.Contains(" FC "))
                {
                    dummy = E2KLine.ToUpper().Split();
                    if (E2KLine.Contains(" FY "))
                        resist_material = dummy[7];
                    else
                        resist_material = dummy[9];
                }
            }
        }

        private static string Texto_sub(string[] vector_texto, int indice, int Caracter)
        {
            int Pos;
            string Texto;
            Pos = vector_texto[indice].LastIndexOf((char)Caracter);
            Texto = vector_texto[indice].Substring(1, Pos - 1);

            return Texto;
        }
    }
}