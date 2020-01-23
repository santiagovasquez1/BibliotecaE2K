﻿using B_Lectura_E2K.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
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
            Printer.WriteTitle("Cargando E2K ETABSV9.5");
            var E2kFile = Cargar_E2K(EVersion.ETABS9);
            var engine = new B_Lectura_E2K.Modelo_engine();
            engine.Inicializar(E2kFile);
            var modelo95 = engine.modelo;
            ImpirimirPisos(modelo95);
            ImprimirMateriales(modelo95);

            Printer.WriteTitle("Cargando E2K ETABSV2018");
            E2kFile = Cargar_E2K(EVersion.ETABS2018);
            engine.Inicializar(E2kFile);
            var modelo2018 = engine.modelo;
            ImpirimirPisos(modelo2018);
            ImprimirMateriales(modelo2018);

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

        private static List<string> Cargar_E2K(EVersion version)
        {
            string Archivo;

            if (version == EVersion.ETABS9)
            {
                Archivo = "ETABS95.$ET";
            }
            else
            {
                Archivo = "Etabs2018.$ET";
            }

            string Ruta = AppDomain.CurrentDomain.BaseDirectory + Archivo;
            string sline;
            List<string> Temp = new List<string>();

            var Reader = new StreamReader(Ruta);
            do
            {
                sline = Reader.ReadLine();
                Temp.Add(sline);
            } while (!(sline == null));

            Reader.Close();

            return Temp;
        }
    }
}