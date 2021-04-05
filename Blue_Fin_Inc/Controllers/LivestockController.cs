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

        public async Task<IActionResult> EditIndex()
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

        [HttpGet]
        public async Task<IActionResult> EditIndex(string LiveSearch)
        {
            ViewData["GetEditLivestockDetails"] = LiveSearch;

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
            if (id == null)
            {
                return NotFound();
            }

            Livestock live = db.Livestocks.FirstOrDefault(p => p.ProductCode == id);
            if (live == null)
            {
                return NotFound();
            }
            return View(live);
        }

        // POST: LivestockController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("CareLevel,Temperment,WaterType,Colours,WaterConditions,MaxSize,ProductCode,Name,Description,Stock,Price")] Livestock live)
        {
            if (id != live.ProductCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(live);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivestockExists(live.ProductCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(live);
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
        private bool LivestockExists(int id)
        {
            return db.Livestocks.Any(e => e.ProductCode == id);
        }
    }
}
