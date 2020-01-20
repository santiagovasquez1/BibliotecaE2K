namespace B_Lectura_E2K.Entidades
{
    public class Material
    {
        public string Material_name { get; set; }
        public Tipo_Material tipo_Material { get; set; }
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
            if (tipo_Material == Tipo_Material.Concrete)
                return $"{Material_name},Fc:{Resistencia}";
            else
                return $"{Material_name},Fy:{Resistencia}";
        }


    }
}