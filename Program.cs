using EF_Task_Crud.Data;
using EF_Task_Crud.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Task_Crud
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            // Add Product data
            
            Product p = new Product() { Name="Bed",Price=7000};
            Product p2 = new Product() { Name="Chair"};
            Product p3 = new Product() { Name="Ventilator",Price=2000};

            dbContext.Products.Add(p);
            dbContext.Products.Add(p2);
            dbContext.Products.Add(p3);

            dbContext.SaveChanges();
            

            // Add Order data
            
            Order o = new Order() { Address="Tulkarm",CreatedAt = new DateTime(2023, 8, 16, 20, 38, 0) };
            Order o2 = new Order() { Address = "Jenin", CreatedAt = DateTime.Parse("2022-05-19 7:40:00") };
            Order o3 = new Order { Address = "Ramallah" , CreatedAt=new DateTime(2024, 10, 13, 2, 17, 4) };
       

            dbContext.Orders.Add(o);
            dbContext.Orders.Add(o2);
            dbContext.Orders.Add(o3);
            dbContext.SaveChanges();
            
            


            // Get Products

            Console.WriteLine("Products : ");
            var products=dbContext.Products.ToList();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}");
            }


            // Get Orders
            Console.WriteLine("Orders : ");
            var orders=dbContext.Orders.ToList();
            foreach(var order in orders)
            {
                Console.WriteLine($"ID: {order.Id}, Address: {order.Address}, CreatedAt: {order.CreatedAt}");
            }





            // Update Product Name
            
            var productsToUpdate = dbContext.Products.Where(p => p.Id == 1).FirstOrDefault();
            if (productsToUpdate != null)
            {
                productsToUpdate.Name = "Laptop";
                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Product not found.");
            }

            dbContext.SaveChanges();


            // Update Order CreatedAt
            var orderToUpdate = dbContext.Orders.FirstOrDefault(o => o.Id == 1);
            if (orderToUpdate != null)
            {
                orderToUpdate.CreatedAt = new DateTime(2023, 12, 31, 23, 59, 59);
                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Order not found.");
            }

            dbContext.SaveChanges();
            


            
             //remove product with id 2
            dbContext.Remove(dbContext.Products.First(p=>p.Id==2));

             //remove order with id 3
            dbContext.Remove(dbContext.Orders.First(p=>p.Id==3));


            dbContext.SaveChanges();
            
            






            //to Remove All existing Records in database
            /* 
            dbContext.Products.RemoveRange(dbContext.Products);
            dbContext.Orders.RemoveRange(dbContext.Orders);
            dbContext.SaveChanges();
            */

            // Reset identity columns
            //This ensures that each run starts with an empty database, and the IDs will start from 1 again.
            /*
            dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Products', RESEED, 0)");
            dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Orders', RESEED, 0)");
            */
        }


    }
}