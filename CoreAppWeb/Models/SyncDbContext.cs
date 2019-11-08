using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAppWeb.Models
{
    /// <summary>
    /// DbContext --> Base class for EF Core
    /// 1. Connects to Db using Connection String
    /// 2. Manages Table Mapping with CLR Objects using DbSet<T>.
    /// 3. DbSet<T> is a cursor that contains ReadOnly Collection of Rows
    /// 4. DbSet<T> uses Change-Tracking for Create, Update and Delete Operations
    /// 5. DbContext.SaveChanges() / DbContext().SaveChangesAsync(), methods for 
    /// Transaction and Commit Transactions
    /// 6. Declarative Relationship across Models, HasMany<T>() and OwnMany<T>(), for
    /// One to Many Relationship. HasOne<T>() and OwnsOne<T>() for One-to-one relationship
    /// 7. DbContextObject Pooling with default 128n object per application
    /// 8. Connection Resiliancy 
    /// 9. The Default Async Operations
    /// DbContextOpetions<DbConext>, this class provided Db Metadata (e.g. ConnectionString)
    /// to DbContext while performing Db Operations
    /// </summary>
    public class SyncDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        // this will read Db Metadata from The DI Container
        public SyncDbContext(DbContextOptions<SyncDbContext> options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
           // modelBuilder.Entity<Category>().HasMany<Product>(p => p.Products);
        }
    }
}
