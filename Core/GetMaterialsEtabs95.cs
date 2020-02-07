using System.Collections.Generic;
using System.Linq;
using B_Lectura_E2K.Entidades;
using B_Lectura_E2K.Entidades.Enumeraciones;

namespace BibliotecaE2K.Core
{
    public class GetMaterialsEtabs95 : IGetMaterial
    {
        public List<Material> ExtraerMateriales(int inicio, int fin, List<string> ModelFile, List<string> MaterialsList)
        {
            var materials = new List<Material>();
            string resist_material = "";
            string Aux = "";
            string Material_name = "";
            string Material_E = "";
            int Pos = 0;
            string[] dummy = { };

            foreach (string AuxMaterial in MaterialsList)
            {
                Pos = AuxMaterial.LastIndexOf((char)34);
                Material_name = AuxMaterial.Substring(1, Pos - 1);
                Aux = Material_name;
                Enum_Material tipomaterial = Enum_Material.Concrete;

                var temp = ModelFile.GetRange(inicio, fin - inicio).FindAll(x => x.Contains($"{(char)34}{Aux}{(char)34}")).ToList();

                for (int i = 0; i < temp.Count-1; i += 2)
                {
                    var Temp1 = temp[i].Split();
                    var Temp2 = temp[i + 1].Split();

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

                        var materiali = new Material(Material_name, float.Parse(Material_E), float.Parse(resist_material))
                        {
                            tipo_Material = tipomaterial
                        };
                        materials.Add(materiali);
                    }
                }

            }

            return materials;
        }
    }
}
