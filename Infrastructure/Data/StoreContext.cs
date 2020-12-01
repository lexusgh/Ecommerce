using System.Linq;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base (options)
        {

        }

       public DbSet<Product> Products {get;set;}
       public DbSet<ProductType> ProductTypes {get;set;}
       public DbSet<ProductBrand> ProductBrands {get;set;}

       protected override void OnModelCreating(ModelBuilder model)
       {
           base.OnModelCreating(model);
           model.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

           if(Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
           {
                foreach(var items in model.Model.GetEntityTypes())
                {
                    var properties =  items.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(decimal));

                     foreach( var item in  properties)
                     {
                         model.Entity(items.Name).Property(item.Name).HasConversion<double>();
                     }
                }
           }
       }

    }
}