using B_Lectura_E2K.Entidades;
using System.Collections.Generic;

namespace BibliotecaE2K.Core
{
    public interface IGetStories
    {
        List<Story> ExtraerStories(List<Story> Stories, List<string> ModeloFile, ref Story Storyi, int inicio, int fin, ref float Elevation);
    }
}