using Blue_Fin_Inc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Controllers
{
    public class LivestockController : Controller
    {
        static List<Livestock> LivestockList = new List<Livestock>()
        {
            new Livestock(CareLevel.Easy, Temperment.Peaceful, WaterType.Fresh, "Black, Silver, Red", "PH:6.0-6.5, KH 0-10, 22°C-26°C", "5cm", 2001, "Harlequin Rasbora", "The Harlequin Rasbora is easily identified by its characteristic black pork chop shaped patch and beautifully lustrous copper/orange body", 2.99),
            new Livestock(CareLevel.Easy, Temperment.Aggresive, WaterType.Fresh, "Black, Blue, Red", "PH:6.0-6.5, KH 0-10, 22°C-26°C", "7.5cm", 2002, "Crown Tail Betta", "The Crown Tail Betta has a striking, elaborate tail that differentiates it from other Bettas. The Crown Tail has a teardrop shape to its tail while the Twin Tail is split, almost giving the suggestion of having two tails.", 19.99)
        };

        // GET: LivestockController
        public ActionResult Index()
        {
            return View(LivestockList);
        }

        // GET: LivestockController/Details/5
        public ActionResult Details(int id)
        {
            Livestock foundLivestock = LivestockList.FirstOrDefault(p => p.ProductCode == id);
            if (foundLivestock != null)
            {
                return View(foundLivestock);
            }
            else
            {
                return NotFound("No livestock found with product code: " + id + "\nIf this is new livestock please add it to the system.");
            }
        }

        // GET: LivestockController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LivestockController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Livestock newLivestock)
        {
            if (ModelState.IsValid)
            {
                LivestockList.Add(newLivestock);
                return View("Details");
            }
            else
            {
                return View("Index");
            }
        }

        // GET: LivestockController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LivestockController/Edit/5
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

        // GET: LivestockController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LivestockController/Delete/5
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
