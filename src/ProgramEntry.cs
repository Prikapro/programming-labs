namespace v3lab {
  class ProgramEntry {
    static void Main() {
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

      while (true) {
        Console.WriteLine("Input the command:");
        string command = Console.ReadLine();
        switch (command) {
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
}

