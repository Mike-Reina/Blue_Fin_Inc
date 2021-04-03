using Blue_Fin_Inc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Controllers
{
    public class LivestockController : Controller
    {
        //DB field
        private readonly ApplicationContext db;

        //Constructor
        public LivestockController()
        {
            db = new ApplicationContext();

            db.Database.EnsureCreated();
            db.SeedDB();
        }

        // GET: LivestockController
        public async Task<IActionResult> Index()
        {
            var list = await db.Livestocks.ToListAsync();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string LiveSearch)
        {
            ViewData["GetLivestockDetails"] = LiveSearch;

            var LiveQuery = from p in db.Livestocks select p;

            if (!String.IsNullOrEmpty(LiveSearch))
            {
                LiveQuery = LiveQuery.Where(p => p.Name.Contains(LiveSearch) || p.Description.Contains(LiveSearch));
            }
            return View(await LiveQuery.AsNoTracking().ToListAsync());
        }

        // GET: LivestockController/Details/5
        public ActionResult Details(int id)
        {
            Livestock foundLivestock = db.Livestocks.FirstOrDefault(p => p.ProductCode == id);
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
                db.Livestocks.Add(newLivestock);
                db.SaveChanges();
                return View("Index", db.Livestocks);
            }
            else
            {
                return View("Index", db.Livestocks);
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
            if (id == null)
            {
                return NotFound();
            }

            var item = db.Livestocks.Where(p => p.ProductCode == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
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
