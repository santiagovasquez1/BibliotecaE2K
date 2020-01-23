namespace B_Lectura_E2K.Entidades
{
    public class Wall_Section
    {
        public string PierLabel { get; set; }
        public string SectionName { get; set; }
        public Material Material { get; set; }
        public float bw { get; set; }
        public float lw { get; set; }

        public Wall_Section(string name, Material material, float b)
        {
            SectionName = name;
            Material = material;
            bw = b;
        }

        public override string ToString()
        {
            return $"{SectionName} Mat : {Material} ,bw : {bw}";
        }
    }
}