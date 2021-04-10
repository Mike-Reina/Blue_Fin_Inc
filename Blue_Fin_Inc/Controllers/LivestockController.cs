using Blue_Fin_Inc.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Controllers
{
    public class LivestockController : Controller
    {
        //DB field
        private readonly ApplicationContext db;
        private readonly IWebHostEnvironment _hostEnvironment;

        //Constructor
        public LivestockController(IWebHostEnvironment hostEnvironment)
        {
            db = new ApplicationContext();
            this._hostEnvironment = hostEnvironment;

            db.Database.Migrate();
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
        public async Task<ActionResult> Create([Bind("CareLevel,Temperment,WaterType,Colours,WaterConditions,MaxSize,ProductCode,Name,Description,Stock,Price,ImageFile")] Livestock newLivestock)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(newLivestock.ImageFile.FileName);
                string extension = Path.GetExtension(newLivestock.ImageFile.FileName);
                newLivestock.ImageName = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/images/", filename);

                using (var filestream = new FileStream(path,FileMode.Create))
                {
                    await newLivestock.ImageFile.CopyToAsync(filestream);
                }

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
        public async Task<ActionResult> Edit(int id, [Bind("CareLevel,Temperment,WaterType,Colours,WaterConditions,MaxSize,ProductCode,Name,Description,Stock,Price,ImageFile,ImageName")] Livestock live)
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
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await db.Livestocks.FirstOrDefaultAsync(p => p.ProductCode == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: LivestockController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await db.Livestocks.FindAsync(id);

            var imagePath = Path.Combine(_hostEnvironment.WebRootPath + "/images/" + product.ImageName);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            db.Livestocks.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivestockExists(int id)
        {
            return db.Livestocks.Any(e => e.ProductCode == id);
        }
    }
}
