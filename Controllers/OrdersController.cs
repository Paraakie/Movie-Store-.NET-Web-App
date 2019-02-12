using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using MovieStore.Models;
using Tuto4;
using Tuto4.OSDB;
using System.Threading.Tasks;
using System;
using Tuto4.Models;
using System.Collections.Generic;

namespace MovieStore.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private StoreContext db = new StoreContext();

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: Orders
        public ActionResult Index(string orderSearch, string startDate, string endDate, string orderSortOrder)
        {
            var orders = db.Orders.OrderBy(o => o.DateCreated).Include(o => o.OrderLines);
            if (!User.IsInRole("Admin"))
            {
                // return View(db.Orders.ToList());
                orders = orders.Where(o => o.UserID == User.Identity.Name);
            }
            if (!String.IsNullOrEmpty(orderSearch))
            {
                orders = orders.Where(
                    o => o.OrderID.ToString().Equals(orderSearch) ||
        o.UserID.Contains(orderSearch) || o.DeliveryName.Contains(orderSearch) ||
        o.DeliveryAddress.AddressLine1.Contains(orderSearch) ||
        o.DeliveryAddress.AddressLine2.Contains(orderSearch) ||
        o.DeliveryAddress.Town.Contains(orderSearch) ||
        o.DeliveryAddress.Country.Contains(orderSearch) ||
        o.DeliveryAddress.PostCode.Contains(orderSearch) ||
        o.TotalPrice.ToString().Equals(orderSearch) ||
        o.OrderLines.Any(ol => ol.ProductName.Contains(orderSearch)));
            }

            DateTime parsedStartDate;
            if (DateTime.TryParse(startDate, out parsedStartDate))
            {
                orders = orders.Where(o => o.DateCreated >= parsedStartDate);
            }
            DateTime parsedEndDate;
            if (DateTime.TryParse(endDate, out parsedEndDate))
            {
                orders = orders.Where(o => o.DateCreated <= parsedEndDate);
            }
            ViewBag.DateSort = String.IsNullOrEmpty(orderSortOrder) ? "date" : "";
            ViewBag.UserSort = orderSortOrder == "user" ? "user_desc" : "user";
            ViewBag.PriceSort = orderSortOrder == "price" ? "price_desc" : "price";
            ViewBag.CurrentOrderSearch = orderSearch;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            switch (orderSortOrder)
            {
                case "user":
                    orders = orders.OrderBy(o => o.UserID);
                    break;
                case "user_desc":
                    orders = orders.OrderByDescending(o => o.UserID);
                    break;
                case "price":
                    orders = orders.OrderBy(o => o.TotalPrice);
                    break;
                case "price_desc":
                    orders = orders.OrderByDescending(o => o.TotalPrice);
                    break;
                case "date":
                    orders = orders.OrderBy(o => o.DateCreated);
                    break;
                default:
                    orders = orders.OrderByDescending(o => o.DateCreated);
                    break;
            }

            return View(orders);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Order order = db.Orders.Find(id);
            Order order = db.Orders.Include(o => o.OrderLines).Where(o => o.OrderID == id).SingleOrDefault();
            if (order == null)
            {
                return HttpNotFound();
            }
            if (order.UserID == User.Identity.Name || User.IsInRole("Admin"))
            {
                return View(order);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

        }
        // GET: Orders/Create (aka Review)
        public async Task<ActionResult> Review()
        {
            Basket basket = Basket.GetBasket();
            Order order = new Models.Order();
            order.UserID = User.Identity.Name;
            ApplicationUser user = await UserManager.FindByNameAsync(order.UserID);
            order.DeliveryName = user.FirstName + " " + user.LastName;
            order.DeliveryAddress = user.Address;
            order.OrderLines = new List<OrderLine>();
            foreach (var basketLine in basket.GetBasketLines())
            {
                OrderLine line = new OrderLine
                {
                    Product = basketLine.Product,
                    ProductID = basketLine.ProductID,
                    ProductName = basketLine.Product.Name,
                    Quantity = basketLine.Quantity,
                    UnitPrice = basketLine.Product.Price
                };
                order.OrderLines.Add(line);
            }
            order.TotalPrice = basket.GetTotalCost();
            return View(order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,DeliveryName,DeliveryAddress")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.DateCreated = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                //add the orderlines to the database after creating the order

                Basket basket = Basket.GetBasket();
                order.TotalPrice = basket.CreateOrderLines(order.OrderID);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = order.OrderID });
            }
            return RedirectToAction("Review");
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,UserID,DeliveryName,DeliveryAddress,TotalPrice,DateCreated")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
