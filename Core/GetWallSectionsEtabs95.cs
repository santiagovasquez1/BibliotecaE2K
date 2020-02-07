using System.Collections.Generic;
using System.Linq;
using B_Lectura_E2K.Entidades;

namespace BibliotecaE2K.Core
{
    public class GetWallSectionsEtabs95 : IGetWallSections
    {
        public List<Wall_Section> Get_Walls(List<string> E2KFile, int inicio, int fin, Modelo_Etabs modelo)
        {
            var Temp = E2KFile.GetRange(inicio, fin - inicio).ToList();
            string[] dummy = { };
            string WallSectionName = "";
            string Temp_material = "";
            float bw = 0f;
            var Temp_wall = new List<Wall_Section>();
            Wall_Section walli = null;
            Material Material_dummy = null;

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

            return Temp_wall;
        }
    }
}