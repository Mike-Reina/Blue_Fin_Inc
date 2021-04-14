﻿using Blue_Fin_Inc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Controllers
{
    public class OrderController : Controller
    {
        //DB field
        private readonly ApplicationContext db;

        public static Order order1 = new Order();

        //Constructor
        public OrderController()
        {
            db = new ApplicationContext();

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

        // GET: OrderController/ShowOrderDetails/5
        public ActionResult ShowOrderDetails(int id, string json)
        {
            Order findOrder = db.Orders.FirstOrDefault(o => o.OrderNo == id );
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
            ordered = ordered.OrderBy(o => o.CustomerName);
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

        public ActionResult Check(int id, string productType)
        {
            if (order1.ContactNo == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                if(productType == "Livestock")
                {
                    order1.AddLivestock(db.Livestocks.FirstOrDefault(p => p.ProductCode == id));
                    return RedirectToAction("Index", "Livestock");
                }
                else if(productType == "Equipment")
                {
                    order1.AddEquipment(db.Equipments.FirstOrDefault(p => p.ProductCode == id));
                    return RedirectToAction("Index", "Equipment");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }
        }

        public ActionResult Remove(int id, string productType)
        {
            if (productType == "Livestock")
            {
                order1.RemoveLivestock(db.Livestocks.FirstOrDefault(p => p.ProductCode == id));
                return RedirectToAction("Details");
            }
            else if (productType == "Equipment")
            {
                order1.RemoveEquipment(db.Equipments.FirstOrDefault(p => p.ProductCode == id));
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
                Livestock updateStock = db.Livestocks.FirstOrDefault(p => p.ProductCode == l.ProductCode);
                updateStock.Stock -= l.Stock;
                order1.OrderDetails += " -- Product Code: " + l.ProductCode + ", Name: " + l.Name + ", Qty: " + l.Stock + Environment.NewLine;
            }
            if (order1.equipementList.Count > 0)
            {
                order1.OrderDetails += " || Equipment Items " + Environment.NewLine;
            }
            foreach (CartEquipment e in order1.equipementList)
            {
                Equipment updateStock = db.Equipments.FirstOrDefault(p => p.ProductCode == e.ProductCode);
                updateStock.Stock -= e.Stock;
                order1.OrderDetails += " -- Product Code: " + e.ProductCode + ", Name: " + e.Name + ", Qty: " + e.Stock + Environment.NewLine;
            }
            order1.Date = DateTime.Now;
            db.Orders.Add(order1);
            await db.SaveChangesAsync();
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
                    db.Update(order);
                    await db.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderNo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
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
            db.Orders.Remove(findOrder);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
