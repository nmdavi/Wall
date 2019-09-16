using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Wall.Models
{
    public class EFContext : DbContext
    {
        public EFContext() : base("WallConn")
        {
            Database.SetInitializer(new DbInitialize());
        }

        public DbSet<FraseViewModel> Frases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
