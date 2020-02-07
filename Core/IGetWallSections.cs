using System.Collections.Generic;
using B_Lectura_E2K.Entidades;

namespace BibliotecaE2K.Core
{
    public interface IGetWallSections
    {
         List<Wall_Section>Get_Walls(List<string> E2KFile, int inicio, int fin, Modelo_Etabs modelo);
    }
}