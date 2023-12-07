namespace v3lab {
  public struct Composition {
    public string Name { get; set; }
    public string Author { get; set; }
    public string FullName { get; set; }
    public Composition(string author, string name) {
      Name = name;
      Author = author;
      FullName = $"{author} - {name}";
    }
  }
}
