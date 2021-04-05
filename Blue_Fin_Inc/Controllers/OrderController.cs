using Blue_Fin_Inc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            db.Database.EnsureCreated();

        }
        
        //MVC Controller Methods
        
        // GET: OrderController/Details/5
        public ActionResult Details()
        {        
            return View(order1);
        }

        
        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
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

            return RedirectToAction("Details");
         }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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
        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
