using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace v3lab
{
    public class CatalogService
    {
        private static CompositionContext db = new();
        private static List<Composition> catalog = new();

        public static List<Composition> getCatalog()
        {
            return catalog;
        }

        public static CompositionContext getDB()
        {
            return db;
        }

        public static void AddComposition(string author, string name)
        {
            catalog.Add(new Composition(author, name));
        }

        public static List<Composition> DelComposition(string candidate)
        {
            var candidates = new List<Composition>();
            foreach (var composition in catalog)
            {
                if (composition.FullName.Equals(candidate))
                {
                    candidates.Add(composition);
                }
            }

            foreach (var composition in candidates)
            {
                catalog.Remove(composition);
            }

            return candidates;
        }

        public static List<Composition> SearchComposition(string search)
        {
            List<Composition> result = new List<Composition>();
            foreach (var composition in catalog)
            {
                if (composition.FullName.Contains(search))
                {
                    result.Add(composition);
                }
            }

            return result;
        }

        public static void LoadCompositions(List<Composition> newCompositions)
        {
            catalog.AddRange(newCompositions);
        }

        public static void SaveCompositions()
        {
            db.DeleteAllCompositions();
            db.AddRange(catalog);
            db.SaveChanges();
        }

        public static List<Composition> JsonToTracks(string json)
        {
            List<JsonComposition> newCompositions;
            try
            {
                newCompositions = JsonConvert.DeserializeObject<List<JsonComposition>>(json);
            }
            catch (JsonSerializationException)
            {
                newCompositions = new();
            }

            List<Composition> result = new();
            foreach (var composition in newCompositions)
            {
                result.Add(new Composition(composition.author, composition.name));
            }

            return result;
        }

        public static string TracksToJson(List<Composition> tracks)
        {
            return JsonConvert.SerializeObject(tracks);
        }

        public static List<Composition> XmlToTracks(string xml)
        {
            List<Composition> result = new();
            XDocument xdoc;
            try
            {
                xdoc = XDocument.Parse(xml);

                foreach (XElement compositionElement in xdoc.Root.Descendants("composition"))
                {
                    result.Add(new Composition(
                        compositionElement.Attribute("author").Value,
                        compositionElement.Attribute("name").Value
                    ));
                }
            }
            catch (XmlException)
            {
            }

            return result;
        }

        public static string TracksToXml(List<Composition> tracks)
        {
            string xml = "<root>\n";
            foreach (var composition in tracks)
            {
                XElement compositionElement = new XElement("composition",
                    new XAttribute("author", composition.Author),
                    new XAttribute("name", composition.Name));
                xml += compositionElement + "\n";
            }

            xml += "</root>";
            return xml;
        }
    }
}