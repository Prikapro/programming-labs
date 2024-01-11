namespace v3lab
{
    public class CatalogService
    {
        private static CompositionContext db = new CompositionContext();
        private static List<Composition> catalog = db.Compositions.ToList();

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
    }
}