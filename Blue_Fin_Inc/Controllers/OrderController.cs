using Blue_Fin_Inc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Controllers
{
    public class OrderController : BaseController
    {
        //DB field
        private readonly ApplicationContext db;
        private readonly IConfiguration _configuration;

        public static Order order1 = new Order();

        //Constructor
        public OrderController(IConfiguration configuration)
        {
            db = new ApplicationContext(configuration);
            _configuration = configuration;
            db.Database.Migrate();

        }
        
        //MVC Controller Methods
        
        // GET: OrderController/Details/
        public ActionResult Details(Order orderDB)
        {       
            if(orderDB.OrderNo > 0 )
            {
                order1.OrderNo = 0;
                order1.OrderDetails = "";
                return View(orderDB);
            }
            return View(order1);
           
        }

        // GET: OrderController/ShowOrderDetails/5?yes
        public async Task<IActionResult> ShowOrderDetails(int id, string json)
        {
            Order findOrder = await db.Orders.FirstOrDefaultAsync(o => o.OrderNo == id);
            if (findOrder != null)
            {
                if (json == "yes")
                {
                    return Ok(findOrder);
                }
                return View(findOrder);
            }
            else
            {
                return NotFound("No order found with order number # " + id + "!");
            }
        }


        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(string LiveSearch)
        {
            if (LiveSearch!= null)
            {
                ViewData["GetOrderDetails"] = LiveSearch;

                var LiveQuery = from o in db.Orders select o;

                if (!String.IsNullOrEmpty(LiveSearch))
                {
                    LiveQuery = LiveQuery.Where(o => o.CustomerName.Contains(LiveSearch) || o.Eircode.Contains(LiveSearch) || o.ContactNo.Contains(LiveSearch));
                }
                return View(await LiveQuery.AsNoTracking().ToListAsync());
            }
           
            var ordered = from o in db.Orders select o;
            ordered = ordered.OrderBy(o => o.OrderNo);
            return View(await ordered.ToListAsync());
        }


        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order, int id)
        {
            if (order1.ContactNo == null)
            {
                order1.CustomerName = order.CustomerName;
                order1.ContactNo = order.ContactNo;
                order1.Eircode = order.Eircode;
                order1.ContainsLivestock = false;
                order1.livestockList.Clear();
                order1.equipementList.Clear();
                order1.OrderPrice = 0; 
            }
           
            return RedirectToAction("Details", order1);
         }

        public async Task<IActionResult> Check(int id, string productType)
        {
            if (order1.ContactNo == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                if(productType == "Livestock")
                {
                    order1.AddLivestock(await db.Livestocks.FirstOrDefaultAsync(p => p.ProductCode == id));
                    return RedirectToAction("Index", "Livestock");
                }
                else if(productType == "Equipment")
                {
                    order1.AddEquipment(await db.Equipments.FirstOrDefaultAsync(p => p.ProductCode == id));
                    return RedirectToAction("Index", "Equipment");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }
        }

        public async Task <IActionResult> Remove(int id, string productType)
        {
            if (productType == "Livestock")
            {
                order1.RemoveLivestock( await db.Livestocks.FirstOrDefaultAsync(p => p.ProductCode == id));
                return RedirectToAction("Details");
            }
            else if (productType == "Equipment")
            {
                order1.RemoveEquipment(await db.Equipments.FirstOrDefaultAsync(p => p.ProductCode == id));
                return RedirectToAction("Details");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<ActionResult> PlaceOrder()
        {
            if(order1.livestockList.Count > 0)
            {
                order1.OrderDetails += "|| Livestock Items " + Environment.NewLine;
            }
            foreach (CartLivestock l in order1.livestockList) 
            {
                Livestock updateStock = await db.Livestocks.FirstOrDefaultAsync(p => p.ProductCode == l.ProductCode);
                updateStock.Stock -= l.Stock;
                order1.OrderDetails += " -- Product Code: " + l.ProductCode + ", Name: " + l.Name + ", Qty: " + l.Stock + Environment.NewLine;
            }
            if (order1.equipementList.Count > 0)
            {
                order1.OrderDetails += " || Equipment Items " + Environment.NewLine;
            }
            foreach (CartEquipment e in order1.equipementList)
            {
                Equipment updateStock = await db.Equipments.FirstOrDefaultAsync(p => p.ProductCode == e.ProductCode);
                updateStock.Stock -= e.Stock;
                order1.OrderDetails += " -- Product Code: " + e.ProductCode + ", Name: " + e.Name + ", Qty: " + e.Stock + Environment.NewLine;
            }
            order1.Date = DateTime.Now;
            await db.Orders.AddAsync(order1);
            await db.SaveChangesAsync();

            var titleIn = "Your order is now placed!";
            Notify(message:"", title: titleIn, notificationType: NotificationType.success);

            order1.ContactNo = null;
            
            List<Order> findAllOrders = await db.Orders.ToListAsync();
            Order lastOrder = findAllOrders.Last();
          
            return RedirectToAction("Details", lastOrder);
        }

        // GET: OrderController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Order findOrder = await db.Orders.FindAsync(id);
            if (findOrder == null)
            {
                return NotFound();
            }
            return View(findOrder);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderNo, CustomerName, Eircode, ContactNo, OrderPrice, Date, ContainsLivestock, OrderDetails")] Order order)
        {
            if(id != order.OrderNo)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Order findOrder = await db.Orders.AsNoTracking().SingleOrDefaultAsync(o => o.OrderNo == id);
                    order.OrderPrice = findOrder.OrderPrice;

                    var titleIn = "Order #" + order.OrderNo + " has been updated succesfully!";
                    db.Update(order);
                    await db.SaveChangesAsync();
                    Notify(message:"", title: titleIn, notificationType: NotificationType.success);
                }
                catch(DbUpdateConcurrencyException e)
                {
                    if (!OrderExists(order.OrderNo))
                    {
                        var titleIn = "Order #" + order.OrderNo + " could not be updated!";
                        Notify(message: "", title: titleIn, notificationType: NotificationType.error);
                        return View("Index");
                    }
                    else
                    {
                        throw new DbUpdateConcurrencyException(e.Message);
                    }
                }
                return RedirectToAction("Index");
            }
            return View(order);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Any(o => o.OrderNo == id);
        }

        // GET: OrderController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Order order = await db.Orders.FirstOrDefaultAsync(o=> o.OrderNo== id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int OrderNo)
        {
            Order findOrder = await db.Orders.FindAsync(OrderNo);
            try
            {
                var titleIn = "Order #" + findOrder.OrderNo + " was successfully deleted!";
                db.Orders.Remove(findOrder);
                await db.SaveChangesAsync();
                Notify(message: "", title: titleIn, notificationType: NotificationType.success);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                if (!OrderExists(findOrder.OrderNo))
                {
                    var titleIn = "Order #" + findOrder.OrderNo + " could not be deleted!";
                    Notify(message: "", title: titleIn, notificationType: NotificationType.error);
                    return View("Index");
                }
                else
                {
                    throw new Exception(e.Message);
                }
            }
        }
    }
}
