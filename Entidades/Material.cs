using B_Lectura_E2K.Entidades.Enumeraciones;
using System;
using System.Collections.Generic;

namespace B_Lectura_E2K.Entidades
{
    public class Material : IComparable
    {
        public string Material_name { get; set; }
        public Enum_Material tipo_Material { get; set; }
        public double Modulo_Elasticidad { get; set; }
        public double Resistencia { get; set; }

        public Material(string name, double modulo, double resistencia)
        {
            Material_name = name;
            Modulo_Elasticidad = modulo;
            Resistencia = resistencia;
        }

        public override string ToString()
        {
            if (tipo_Material == Enum_Material.Concrete)
                return $"{Material_name},Fc:{Resistencia}";
            else
                return $"{Material_name},Fy:{Resistencia}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Material)
            {
                Material temp = (Material)obj;
                if (Resistencia == temp.Resistencia) return true;
                return false;
            }

            return base.Equals(obj);
        }

        public int CompareTo(object obj)
        {
            if (obj is Material)
            {
                Material temp = (Material)obj;
                if (Resistencia > temp.Resistencia) return 1;
                if (Resistencia < temp.Resistencia) return -1;
            }
            return 0;
        }

        public override int GetHashCode()
        {
            var hashCode = -1922259499;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Material_name);
            hashCode = hashCode * -1521134295 + tipo_Material.GetHashCode();
            hashCode = hashCode * -1521134295 + Modulo_Elasticidad.GetHashCode();
            hashCode = hashCode * -1521134295 + Resistencia.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Material m1, Material m2)
        {
            return m1.Equals(m2);
        }

        public static bool operator !=(Material m1, Material m2)
        {
            return !m1.Equals(m2);
        }

        public static bool operator <(Material m1, Material m2)
        {
            if (m1.CompareTo(m2) < 0)
                return true;
            else
                return false;
        }

        public static bool operator >(Material m1, Material m2)
        {
            if (m1.CompareTo(m2) > 0)
                return true;
            else
                return false;
        }
    }
}