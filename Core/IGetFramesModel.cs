using B_Lectura_E2K.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaE2K.Core
{
    public interface IGetFramesModel
    {
        List<IFrameModel> ExtraerFramesModelo();
        void Get_Story_Frame();
    }
}
