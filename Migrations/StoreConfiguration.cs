namespace Tuto4.Migrations.StoreConfiguration
{
    using Models;
    using System.Collections.Generic;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MovieStore.Models;

    internal sealed class StoreConfiguration : DbMigrationsConfiguration<Tuto4.OSDB.StoreContext>
    {
        public StoreConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Tuto4.OSDB.StoreContext";
        }

        protected override void Seed(Tuto4.OSDB.StoreContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            // Movie Genres aka Categories
            var categories = new List<Category>
            {
                new Category {Name = "Action" },
                new Category {Name = "Adventure" },
                new Category {Name = "Comedy" },
                new Category {Name = "Fantasy" },
                new Category {Name = "Romance" },
                new Category {Name = "Science fiction"},
                new Category {Name = "Superhero" },
                new Category {Name = "Thriller" },
                new Category {Name = "Western" }
            };
            categories.ForEach(c => context.Categories.AddOrUpdate(p => p.Name, c));
            context.SaveChanges();

            // Movies aka Products
            var products = new List<Product>
            {
                new Product {Name = "Mission Impossible - Fallout", Description = "Ethan Hunts epic saga continues!", Price = 25, CategoryID= categories.Single(c=>c.Name == "Action").ID},
                new Product {Name = "Ralph Breaks the Internet", Description = "In this sequel, Ralph is faced with his biggest challgne yet, letting go.", Price = 35, CategoryID= categories.Single(c=>c.Name == "Adventure").ID},
                new Product {Name = "Deadpool", Description = "A superhero movie, that is mostly comedy.", Price = 15, CategoryID= categories.Single(c=>c.Name == "Comedy").ID},
                new Product {Name = "Fantastic Beasts: The Crimes of Grindelwald", Description = "The Fanacstic beast saga continues.", Price = 20, CategoryID= categories.Single(c=>c.Name == "Fantasy").ID},
                new Product {Name = "Crazy Rich Asians", Description = "Rachel Chu finds out her boyfriend's family is extremly wealthy and tries to impress his mother", Price = 30, CategoryID= categories.Single(c=>c.Name == "Romance").ID},
                new Product {Name = "Arrival", Description = "Loise has to learn an alien language.", Price = 10, CategoryID= categories.Single(c=>c.Name == "Science fiction").ID},
                new Product {Name = "Black Panther", Description = "In this oscor nomicated movie T Challa struggles excepting his role as new king of Wakanda", Price = 25, CategoryID= categories.Single(c=>c.Name == "Superhero").ID},
                new Product {Name = "It", Description = "An ancient, shape-shifting evil that emerges from the sewer every 27 years to prey on the town's children", Price = 20, CategoryID= categories.Single(c=>c.Name == "Thriller").ID},
                new Product {Name = "The Lone Ranger", Description = "A Cowboy, Indian duo take down the infamous Butch Cavendish", Price = 5, CategoryID= categories.Single(c=>c.Name == "Western").ID},

            };
            products.ForEach(c => context.Products.AddOrUpdate(p => p.Name, c));
            context.SaveChanges();

            // Orders
            var orders = new List<Order>
            {
                new Order { DeliveryAddress = new Address { AddressLine1="1 Some Street", Town="Town1",
                 Country="Country", PostCode="PostCode" }, TotalPrice=631,
                 UserID="admin@example.com", DateCreated=new DateTime(2014, 1, 1) ,
                 DeliveryName="Admin" },
                new Order { DeliveryAddress = new Address { AddressLine1="1 Some Street", Town="Town1",
                 Country="Country", PostCode="PostCode" }, TotalPrice=239,
                 UserID="admin@example.com", DateCreated=new DateTime(2014, 1, 2) ,
                 DeliveryName="Admin" },
                new Order { DeliveryAddress = new Address { AddressLine1="1 Some Street", Town="Town1",
                 Country="Country", PostCode="PostCode" }, TotalPrice=239,
                 UserID="admin@example.com", DateCreated=new DateTime(2014, 1, 3) ,
                 DeliveryName="Admin" },
                new Order { DeliveryAddress = new Address { AddressLine1="1 Some Street", Town="Town1",
                 Country="County", PostCode="PostCode" }, TotalPrice=631,
                 UserID="admin@example.com", DateCreated=new DateTime(2014, 1, 4) ,
                 DeliveryName="Admin" },
                new Order { DeliveryAddress = new Address { AddressLine1="1 Some Street", Town="Town1",
                 Country="Country", PostCode="PostCode" }, TotalPrice=19.49M,
                 UserID="admin@example.com", DateCreated=new DateTime(2014, 1, 5) ,
                 DeliveryName="Admin" }
            };
            orders.ForEach(c => context.Orders.AddOrUpdate(o => o.DateCreated, c));
            context.SaveChanges();

            // Order Lines
            var orderLines = new List<OrderLine>
            {
                    new OrderLine { OrderID = 1, ProductID = products.Single( c=> c.Name == "Mission Impossible - Fallout").ID,
                        ProductName ="Mission Impossible - Fallout", Quantity =1, UnitPrice=products.Single( c=> c.Name == "Mission Impossible - Fallout").Price },

                    new OrderLine { OrderID = 2, ProductID = products.Single( c=> c.Name == "Arrival").ID,
                     ProductName="Arrival", Quantity=1, UnitPrice=products.Single( c=> c.Name =="Arrival").Price },

                    new OrderLine { OrderID = 3, ProductID = products.Single( c=> c.Name == "Ralph Breaks the Internet").ID,
                        ProductName ="Ralph Breaks the Internet", Quantity=1, UnitPrice=products.Single( c=> c.Name == "Ralph Breaks the Internet").Price },

                    new OrderLine { OrderID = 4, ProductID = products.Single( c=> c.Name == "Crazy Rich Asians").ID,
                        ProductName ="Crazy Rich Asians", Quantity=1, UnitPrice=products.Single( c=> c.Name == "Crazy Rich Asians").Price },

                    new OrderLine { OrderID = 5, ProductID = products.Single( c=> c.Name == "Black Panther").ID,
                        ProductName ="Black Panther", Quantity=1, UnitPrice=products.Single( c=> c.Name == "Black Panther").Price }

            };

            orderLines.ForEach(c => context.OrderLines.AddOrUpdate(ol => ol.OrderID, c));
            context.SaveChanges();
        }       
    }
}
