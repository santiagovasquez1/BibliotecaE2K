using B_Lectura_E2K.Entidades;
using B_Lectura_E2K.Entidades.Enumeraciones;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaE2K.Core
{
    public class GetMaterialsEtabs2018 : IGetMaterial
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

                var materiali = new Material(Material_name, float.Parse(Material_E), float.Parse(resist_material))
                {
                    tipo_Material = tipomaterial
                };
                materials.Add(materiali);
            }
            return materials;
        }

    }
}