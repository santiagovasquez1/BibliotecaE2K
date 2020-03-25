using B_Lectura_E2K.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace B_Lectura_E2K.Core
{
    public interface IFrameFactory
    {
        List<IFrameModel> CreateFrames(string line, List<MPoint> Points, List<string> LineAssigment, List<Story> Stories, List<ISection> Sections);
    }
}
