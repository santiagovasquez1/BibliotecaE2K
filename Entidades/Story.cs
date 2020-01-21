namespace B_Lectura_E2K.Entidades
{
    public class Story
    {
        public string StoryName { get; set; }
        public float StoryHeight { get; set; }
        public float StoryElevation { get; set; }

        public Story(string name, float height)
        {
            StoryName = name;
            StoryHeight = height;
        }

        public override string ToString()
        {
            return $"{StoryName} Elevation : {StoryElevation}";
        }
    }
}