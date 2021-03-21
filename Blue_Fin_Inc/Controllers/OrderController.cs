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
        

        //ResuFul API Methods
        
        // GET: OrderController/Details/5
        public ActionResult Details()
        {
            Order order1 = new Order("D14F653", "+353830492724");
            Livestock livestock1 = new Livestock(CareLevel.Easy, Temperment.Peaceful, WaterType.Fresh, "Black, Silver, Red", "PH:6.0-6.5, KH 0-10, 22°C-26°C", "5cm", 2001, "Harlequin Rasbora", "The Harlequin Rasbora is easily identified by its characteristic black pork chop shaped patch and beautifully lustrous copper/orange body", 2.99);
            Equipment equipment1 = new Equipment("Juwel", 92, 41, 55, "Black", "50 kg", 1001, "Juwel Vision 180", "Painstaking workmanship from Germany, top - quality materials and perfectly tuned technology guarantee the very best of quality and safety, meaning a long service life for your new aquarium.", 610.99);
            
            order1.AddProduct(livestock1);
            order1.AddProduct(equipment1);
            order1.AddProduct(equipment1);
            order1.RemoveProduct(livestock1);

            return View(order1);
        }


        //To be added...
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
        public ActionResult Create(IFormCollection collection)
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

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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
