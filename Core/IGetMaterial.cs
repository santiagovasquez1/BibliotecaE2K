using System.Collections.Generic;
using B_Lectura_E2K.Entidades;

namespace BibliotecaE2K.Core
{
    public interface IGetMaterial
    {
         List<Material> ExtraerMateriales(int inicio, int fin, List<string> ModelFile, List<string> MaterialsList);
    }
}