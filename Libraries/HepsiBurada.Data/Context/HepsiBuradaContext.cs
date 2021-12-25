using HepsiBurada.Model.DbEntity;
using Microsoft.EntityFrameworkCore;

namespace HepsiBurada.Data.Context
{
    public class HepsiBuradaContext : DbContext
    {
        public HepsiBuradaContext(DbContextOptions<HepsiBuradaContext> options) : base(options) { }

        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
