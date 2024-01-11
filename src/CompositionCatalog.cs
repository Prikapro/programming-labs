using System.Xml.Linq;
using Newtonsoft.Json;

namespace v3lab
{
    public class CompositionCatalog
    {
        public static void ShowAllCompositions()
        {
            if (CatalogService.getCatalog().Count > 0)
            {
                Console.WriteLine("All compositions in catalog:");
                foreach (var composition in CatalogService.getCatalog())
                {
                    Console.WriteLine(composition.FullName);
                }
            }
            else
            {
                Console.WriteLine("Catalog is empty.");
            }
        }

        public static void AddComposition()
        {
            Console.WriteLine("Input author's name:");
            string author = Console.ReadLine();
            Console.WriteLine("Input the composition's name:");
            string name = Console.ReadLine();
            CatalogService.AddComposition(author, name);
        }

        public static void DelComposition()
        {
            Console.WriteLine("Input the full name of the track to remove:");
            string candidate = Console.ReadLine();
            List<Composition> candidates = CatalogService.DelComposition(candidate);
            if (candidates.Count == 0)
            {
                Console.WriteLine("Composition not found.");
                return;
            }

            for (int i = 0; i < candidates.Count; i++)
            {
                Console.WriteLine($"Track \"{candidate}\" deleted.");
            }
        }

        public static void SearchComposition()
        {
            Console.WriteLine(
                "Input the part of the name to find composition in the catalog:");
            string search = Console.ReadLine();
            List<Composition> compositions = CatalogService.SearchComposition(search);
            if (compositions.Count == 0)
            {
                Console.WriteLine("No one item was found by this criteria");
                return;
            }

            foreach (var composition in compositions)
            {
                Console.WriteLine(composition.FullName);
            }
        }

        public static void LoadCompositions()
        {
            ChooseFormat(true);
        }

        public static void SaveCompositions()
        {
            ChooseFormat(false);
        }

        private static void ChooseFormat(Boolean isLoad)
        {
            Console.WriteLine("Choose format");
            Console.WriteLine("\t\"json\" to choose json format");
            Console.WriteLine("\t\"xml\" to choose json format");
            Console.WriteLine("\t\"db\" to choose db format");
            string format = Console.ReadLine();
            if (isLoad)
            {
                switch (format)
                {
                    case "json":
                        LoadJsonCompositions();
                        break;
                    case "xml":
                        LoadXmlCompositions();
                        break;
                    case "db":
                        LoadDBCompositions();
                        break;
                    default:
                        Console.WriteLine("Incorrect format");
                        break;
                }
            }
            else
            {
                switch (format)
                {
                    case "json":
                        SaveJsonCompositions();
                        break;
                    case "xml":
                        SaveXmlCompositions();
                        break;
                    case "db":
                        SaveDBCompositions();
                        break;
                    default:
                        Console.WriteLine("Incorrect format");
                        break;
                }
            }
        }

        private static void LoadDBCompositions()
        {
            CatalogService.getCatalog().AddRange(CatalogService.getDB().Compositions.ToList());
        }

        private static void SaveDBCompositions()
        {
            CatalogService.SaveCompositions();
        }


        private static void LoadJsonCompositions()
        {
            Console.WriteLine(
                "load composition: give name of file in folder '/files'"
            );
            string search = Console.ReadLine();
            try
            {
                using (StreamReader reader = new StreamReader(@"files/" + search))
                {
                    string json = reader.ReadToEnd();

                    List<JsonComposition> newCompositions =
                        JsonConvert.DeserializeObject<List<JsonComposition>>(json);

                    foreach (var composition in newCompositions)
                    {
                        CatalogService.getCatalog().Add(new Composition(composition.author, composition.name));
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Incorrect path: files/" + search);
            }
        }


        private static void SaveJsonCompositions()
        {
            Console.WriteLine("write output file.json in '/files' folder");
            string search = Console.ReadLine();
            string json = JsonConvert.SerializeObject(CatalogService.getCatalog());
            File.WriteAllText(@"files/" + search, json);
        }

        private static void LoadXmlCompositions()
        {
            Console.WriteLine("load composition: give name of file in folder '/files'");
            string search = Console.ReadLine();
            try
            {
                using (StreamReader reader = new StreamReader(@"files/" + search))
                {
                    string xml = reader.ReadToEnd();

                    XDocument xdoc = XDocument.Parse(xml);
                    foreach (XElement compositionElement in xdoc.Root.Descendants("composition"))
                    {
                        CatalogService.getCatalog().Add(new Composition(
                            compositionElement.Attribute("author").Value,
                            compositionElement.Attribute("name").Value
                        ));
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Incorrect path: files/" + search);
            }
        }

        private static void SaveXmlCompositions()
        {
            Console.WriteLine("write output file.xml in '/files' folder");
            string search = Console.ReadLine();
            string xml = "<root>\n";
            foreach (var composition in CatalogService.getCatalog())
            {
                XElement compositionElement = new XElement("composition",
                    new XAttribute("author", composition.Author),
                    new XAttribute("name", composition.Name));
                xml += compositionElement + "\n";
            }

            xml += "</root>";

            File.WriteAllText(@"files/" + search, xml);
        }
    }
}