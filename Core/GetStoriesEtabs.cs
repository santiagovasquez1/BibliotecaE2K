using System.Collections.Generic;
using B_Lectura_E2K.Entidades;

namespace BibliotecaE2K.Core
{
    public class GetStoriesEtabs : IGetStories
    {
        public List<Story> ExtraerStories(List<Story> Stories, List<string> ModeloFile, ref Story Storyi, int inicio, int fin, ref float Elevation)
        {
            for (int i = fin; i >= inicio; i--)
            {
                var Temp = ModeloFile[i].Split();
                string Story_name = Temp[3].Replace("\"", "");
                string Story_Height = Temp[6];
                Elevation += float.Parse(Story_Height);

                Storyi = new Story(Story_name, float.Parse(Story_Height));
                Storyi.StoryElevation = Elevation;
                Stories.Add(Storyi);
            }

            return Stories;
        }
    }
}