using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class ProdDBContext : DbContext
    {

        static ProdDBContext()
        {
            Database.SetInitializer<ProdDBContext>(new BDInitializer());  //remove row after Initializer
        }
        public ProdDBContext() : base("ProdConection")
        {
            // prevent Loop Reference
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }



        public DbSet<Product> Products_ { get; set; }
        public DbSet<Order> Orders_ { get; set; }
        public DbSet<OrderDetail> OrderDetails_ { get; set; }
    }
}
