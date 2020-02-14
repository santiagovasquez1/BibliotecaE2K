using B_Lectura_E2K.Entidades;
using System.Collections.Generic;

namespace BibliotecaE2K.Core
{
    public class GetPointsEtabs : IGetPoints
    {
        public List<MPoint> ExtraerPuntos(List<string> E2KRange)
        {
            double Xc, Yc;
            double Zc = 0;
            string PointLabel = "";
            List<MPoint> mpoints = new List<MPoint>();

            foreach (string E2KLine in E2KRange)
            {
                var TempPoint = E2KLine.Split();
                PointLabel = TempPoint[3].Replace("\"", "");
                Xc = double.Parse(TempPoint[5]);
                Yc = double.Parse(TempPoint[6]);

                if (TempPoint[7] != "")
                {
                    Zc = double.Parse(TempPoint[7]);
                }

                var puntoi = new MPoint(PointLabel, Xc, Yc, Zc);
                mpoints.Add(puntoi);
            }

            return mpoints;
        }
    }
}