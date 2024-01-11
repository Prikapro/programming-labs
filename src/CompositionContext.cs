using Microsoft.EntityFrameworkCore;

namespace v3lab
{
    public class CompositionContext : DbContext
    {
        public DbSet<Composition> Compositions { get; set; }

        public string DbPath { get; }

        public CompositionContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            DbPath = @"db/composition.db";
        }

        public void DeleteAllCompositions()
        {
            Compositions.RemoveRange(Compositions.ToList());
            SaveChanges();
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}