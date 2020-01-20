using System.Collections.Generic;

namespace B_Lectura_E2K.Entidades
{
    public class Modelo_Etabs
    {
        Version_Etabs version { get; set; }
        List<Story> Stories { get; set; }
        List<Material> Materials { get; set; }
        List<IConcreteSection> ConcreteSections { get; set; }
        List<ISteelSection> SteelSections { get; set; }
    }
}