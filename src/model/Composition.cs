using Newtonsoft.Json;

namespace v3lab
{
    public class Composition
    {
        public int CompositionId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        [JsonIgnore] public string FullName { get; set; }

        public Composition()
        {
        }

        public Composition(string author, string name)
        {
            Name = name;
            Author = author;
            FullName = $"{author} - {name}";
        }
    }

    public class JsonComposition
    {
        public string name { get; set; }
        public string author { get; set; }

        public JsonComposition()
        {
        }
    }
}