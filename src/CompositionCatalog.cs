namespace v3lab {
  public class CompositionCatalog {
    static List<Composition> catalog = new List<Composition>();

    public static void ShowAllCompositions() {
      if (catalog.Count > 0) {
        Console.WriteLine("All compositions in catalog:");
        foreach (var composition in catalog) {
          Console.WriteLine(composition.FullName);
        }
      } else {
        Console.WriteLine("Catalog is empty.");
      }
    }
    public static void AddComposition() {
      Console.WriteLine("Input author's name:");
      string author = Console.ReadLine();
      Console.WriteLine("Input the composition's name:");
      string name = Console.ReadLine();
      catalog.Add(new Composition(author, name));
    }
    public static void DelComposition() {
      Console.WriteLine("Input the full name of the track to remove:");
      string candidate = Console.ReadLine();
      var candidates = new List<Composition>();
      bool flag = false;
      foreach (var composition in catalog) {
        if (composition.FullName.Equals(candidate)) {
          Console.WriteLine($"Track \"{candidate}\" deleted.");
          candidates.Add(composition);
          flag = true;
        }
      }
      foreach (var composition in candidates) {
        catalog.Remove(composition);
      }
      if (!flag)
        Console.WriteLine("Composition not found.");
    }
    public static void SearchComposition() {
      Console.WriteLine(
          "Input the part of the name to find composition in the catalog:");
      string search = Console.ReadLine();
      bool flag = false;
      foreach (var composition in catalog) {
        if (composition.FullName.Contains(search)) {
          Console.WriteLine(composition.FullName);
          flag = true;
        }
      }
      if (!flag)
        Console.WriteLine("No one item was found by this criteria");
    }
  }
}
