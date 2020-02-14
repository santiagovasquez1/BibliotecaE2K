using B_Lectura_E2K.Entidades;
using B_Lectura_E2K.Entidades.ConcreteSection;
using B_Lectura_E2K.Entidades.Enumeraciones;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaE2K.Core
{
    public class GetFrameSectionsEtabs : IGetFrameSections
    {
        public List<IConcreteSection> GetConcreteFrameSection(string[] dummy,
                                                              string FrameName, string Temp_material, Material Material_dummy,
                                                              int inicio, int fin, IConcreteSection framei,
                                                              int indiceM, int indiceB,
                                                              Modelo_Etabs modelo, List<string> E2KFile)
        {
            var Temp = E2KFile.GetRange(inicio, fin - inicio).FindAll(x => x.Contains(" MATERIAL "));
            List<IConcreteSection> concreteSections = new List<IConcreteSection>();

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
            return concreteSections;
        }
    }
}