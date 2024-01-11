namespace v3lab
{
    class ProgramEntry
    {
        static void Main()
        {
            Console.WriteLine("Choose type app:");
            Console.WriteLine("\t\"web\" to launch web app");
            Console.WriteLine("\t\"console\" to launch console app");
            string appType = Console.ReadLine();
            if (appType.Equals("web"))
            {
                var builder = WebApplication.CreateBuilder();
                var app = builder.Build();

                app.MapGet("/", () => "Hello World!");
                app.MapGet("/tracks", () => CatalogService.getCatalog());
                app.MapPost("/tracks/search",
                    (JsonComposition composition) =>
                        CatalogService.SearchComposition(GetFullName(composition)));
                app.MapPost("/tracks",
                    (JsonComposition composition) =>
                        CatalogService.AddComposition(composition.author, composition.name)
                );
                app.MapPost("/tracks/delete",
                    (JsonComposition composition) =>
                        CatalogService.DelComposition(GetFullName(composition)));
                app.MapPost("/tracks/load/{tracks}", (List<Composition> tags) => CatalogService.LoadCompositions(tags));
                app.MapPost("/tracks/save", () => CatalogService.SaveCompositions());
                app.Run();
            }
            else
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("\tType one of commands:");
                Console.WriteLine("\t\t\"list\" to display all items of catalog");
                Console.WriteLine("\t\t\"search\" to go find items in catalog");
                Console.WriteLine("\t\t\"add\" to add new item");
                Console.WriteLine("\t\t\"del\" to remove some item from list");
                Console.WriteLine("\t\t\"load\" to load songs lists");
                Console.WriteLine("\t\t\"save\" to save songs lists");
                Console.WriteLine("\t\t\"quit\" to exit");
                Console.WriteLine("----");

                while (true)
                {
                    Console.WriteLine("Input the command:");
                    string command = Console.ReadLine();
                    switch (command)
                    {
                        case "list":
                            CompositionCatalog.ShowAllCompositions();
                            break;
                        case "search":
                            CompositionCatalog.SearchComposition();
                            break;
                        case "add":
                            CompositionCatalog.AddComposition();
                            break;
                        case "del":
                            CompositionCatalog.DelComposition();
                            break;
                        case "load":
                            CompositionCatalog.LoadCompositions();
                            break;
                        case "save":
                            CompositionCatalog.SaveCompositions();
                            break;
                        case "quit":
                            return;
                        default:
                            Console.WriteLine("Wrong command!");
                            break;
                    }

                    Console.WriteLine("----");
                }
            }
        }

        private static string GetFullName(JsonComposition composition)
        {
            return new Composition(composition.author, composition.name).FullName;
        }
    }
}