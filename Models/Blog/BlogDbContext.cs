using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace webtintuc.Models
{
    public class BlogDbContext : IdentityDbContext<AppUser>
    {

        public DbSet<NewsModel> news { set; get; }
        public DbSet<AuthorModel> author { set; get; }
        public DbSet<CategoryModel> category { set; get; }
        public DbSet<ContactModel> contact { set; get; }
        public DbSet<CommentModel> comment { set; get; }
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
            //
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }
    }

}