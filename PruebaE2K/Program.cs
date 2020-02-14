using B_Lectura_E2K.Entidades;
using BibliotecaE2K;
using BibliotecaE2K.Core;
using System;
using static System.Console;

namespace PruebaE2K
{
    internal class Program
    {
        public enum EVersion
        {
            ETABS9,
            ETABS2018
        }

        private static void Main(string[] args)
        {
            string Archivo = "";
            string FilePath = "";

            Printer.WriteTitle("Cargando E2K ETABSV9.5");
            Archivo = "Etabs95.$ET";
            FilePath = AppDomain.CurrentDomain.BaseDirectory + Archivo;
            Printer.WriteTitle("Refactoring 9.5");
            Etabs95 etabs95 = new Etabs95(FilePath);
            var Modelo95 = etabs95.Modelo;
            ImpirimirPisos(Modelo95);
            ImprimirMateriales(Modelo95);
            ImprimirPuntos(Modelo95);
            Printer.PresioneENTER();
            //ReadLine();

            Printer.WriteTitle("Cargando E2K ETABSV2018");
            Archivo = "Etabs2018.$ET";
            FilePath = AppDomain.CurrentDomain.BaseDirectory + Archivo;
            Printer.WriteTitle("Refactoring 2018");
            Etabs2018 etabs2018 = new Etabs2018(FilePath);
            var Modelo2018 = etabs2018.Modelo;
            ImpirimirPisos(Modelo2018);
            ImprimirMateriales(Modelo2018);
            ImprimirPuntos(Modelo2018);
            Printer.PresioneENTER();
            ReadLine();
        }

        private static void ImpirimirPisos(Modelo_Etabs modelo)
        {
            Printer.WriteTitle("Pisos modelo");

            foreach (var piso in modelo.Stories)
            {
                WriteLine(piso);
            }
        }

        private static void ImprimirMateriales(Modelo_Etabs modelo)
        {
            Printer.WriteTitle("Materiales modelo");
            foreach (var material in modelo.Materials)
            {
                WriteLine(material);
            }
        }

        private static void ImprimirPuntos(Modelo_Etabs modelo)
        {
            Printer.WriteTitle("Puntos modelo");
            foreach (var punto in modelo.Points)
            {
                WriteLine(punto);
            }
        }
    }
}