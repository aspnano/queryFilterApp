using Microsoft.EntityFrameworkCore;

namespace queryFilterApp.Models
{
    public class ApplicationDbContext : DbContext
    {

        // Constructor -- convention used by Entity Framework 
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // DbSets -- create for all entity types to be managed with EF
        public DbSet<Product> Products { get; set; }


        // On Model Creating -  apply global query filters when the app starts up and creates the model
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasQueryFilter(s => s.IsDeleted == false); // filter out deleted entities (soft delete)
        }

        // On Save Changes -- handle soft delete fields
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<Product>().ToList()) 
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:   // intercept delete requests, forward as modified, mark field as deleted       
                        entry.Entity.IsDeleted = true;
                        entry.State = EntityState.Modified; 
                        break;
                }
            }

            var result = base.SaveChanges();
            return result;
        }
    }
}
