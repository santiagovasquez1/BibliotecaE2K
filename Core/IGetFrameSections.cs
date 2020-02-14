using B_Lectura_E2K.Entidades;
using System.Collections.Generic;

namespace BibliotecaE2K.Core
{
    public interface IGetFrameSections
    {
        List<IConcreteSection> GetConcreteFrameSection(string[] dummy,
                                                       string FrameName, string Temp_material, Material Material_dummy,
                                                       int inicio, int fin, IConcreteSection framei,
                                                       int indiceM, int indiceB,
                                                       Modelo_Etabs modelo, List<string> E2KFile);
    }
}