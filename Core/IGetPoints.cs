using B_Lectura_E2K.Entidades;
using System.Collections.Generic;

namespace BibliotecaE2K.Core
{
    public interface IGetPoints
    {
        List<MPoint> ExtraerPuntos(List<string> E2KRange);
    }
}
