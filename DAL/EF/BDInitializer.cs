using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class BDInitializer : IDatabaseInitializer<ProdDBContext>
    {
        public void InitializeDatabase(ProdDBContext context) // метод для кастомной инициализации БД - Code First
        {

            //if (context.Database.Exists()) context.Database.Delete();
            //context.Database.Create();

            if (context.Database.CreateIfNotExists()) // создать БД если её нет, и записать в БД начальные данные
            {

                // начальные данные для новых таблиц БД
                
                    Product pr1 = new Product()
                    {
                        ProductName = "FCV-123",
                        ProductPrice = 122.21f
                    };
                    Product pr2 = new Product()
                    {
                        ProductName = "CVB-1763",
                        ProductPrice = 100.10f
                    };
                    Product pr3 = new Product()
                    {
                        ProductName = "CFHB-17EB",
                        ProductPrice = 10000.10f
                    };
                    Product pr4 = new Product()
                    {
                        ProductName = "CFDFC-43E",
                        ProductPrice = 12000.10f
                    };
                    
                    context.Products_.AddRange(new[] { pr1, pr2, pr3, pr4 });
                    context.SaveChanges();


                    Order or1 = new Order()
                    {
                        OrderDate = new DateTime(2018, 5, 1, 8, 30, 52)
                    };
                    Order or2 = new Order()
                    {
                        OrderDate = new DateTime(2018, 6, 11, 8, 30, 50)
                    };


                    context.Orders_.AddRange(new[] { or1, or2});
                    context.SaveChanges();

                    OrderDetail od1 = new OrderDetail() {Product = pr1, Quantity = 2, Order = or1};
                    OrderDetail od2 = new OrderDetail() {Product = pr2, Quantity = 3, Order = or1};
                    OrderDetail od3 = new OrderDetail() { Product = pr3, Quantity = 1, Order = or2 };
                    OrderDetail od4 = new OrderDetail() { Product = pr4, Quantity = 4, Order = or2 };

                    context.OrderDetails_.AddRange(new[]{od1, od2, od3, od4});
                    context.SaveChanges();

                
            }
        }
    }
}
