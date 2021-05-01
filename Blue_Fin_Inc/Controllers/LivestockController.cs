using Blue_Fin_Inc.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Controllers
{
    public class LivestockController : BaseController
    {
        //DB field
        private readonly ApplicationContext db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;

        //Constructor
        public LivestockController(IWebHostEnvironment hostEnvironment, IConfiguration configuration, IWebHostEnvironment env)
        {
            db = new ApplicationContext(configuration, env);
            this._hostEnvironment = hostEnvironment;
            _configuration = configuration;
            db.Database.Migrate();
            db.SeedDB(configuration, env);
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
        public ActionResult Details(int id, string json)
        {
            Livestock foundLivestock = db.Livestocks.FirstOrDefault(p => p.ProductCode == id);
            if (foundLivestock != null)
            {
                if (json == "yes")
                {
                    return Ok(foundLivestock);
                }
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
                Notify("Product creation successfull!", title: newLivestock.Name + " has been added to the system!", notificationType: NotificationType.success);
                return View("Index", db.Livestocks);
            }
            else
            {
                Notify("Please try again or contact your system admin!", title: newLivestock.Name + " could not be added to the system!", notificationType: NotificationType.error);
                return View("Index", db.Livestocks);
            }
        }

        // GET: LivestockController/Edit/5
        public async Task <IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Livestock live = await db.Livestocks.FindAsync(id);
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
                    var titleIn = "\"" + live.Name + "\" has been updated succesfully!";
                    db.Update(live);
                    await db.SaveChangesAsync();
                    Notify("Data saved successfully", title: titleIn, notificationType: NotificationType.success);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivestockExists(live.ProductCode))
                    {
                        var titleIn = "\"" + live.Name + "\" could not be updated!";
                        Notify("Could not update data!", title: titleIn, notificationType: NotificationType.error);
                        return RedirectToAction(nameof(EditIndex));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(EditIndex));
            }
            return View(live);
        }

        // GET: LivestockController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
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
            Notify("Product deletion successfull!", title: product.Name + "has been deleted from the system!", notificationType: NotificationType.success);
            return RedirectToAction(nameof(EditIndex));
        }

        private bool LivestockExists(int id)
        {
            return db.Livestocks.Any(e => e.ProductCode == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddStock(int id, int stock)
        {
            if (stock < 1)
            {
                Notify("Stock additon unsuccessfull!", title: "Stock to add must be greater than 0!", notificationType: NotificationType.error);
                return View("EditIndex", db.Livestocks);
            }

            Livestock live= db.Livestocks.FirstOrDefault(p => p.ProductCode == id);

            db.Livestocks.Attach(live);
            live.Stock += stock;

            db.Entry(live).Property(x => x.Stock).IsModified = true;

            await db.SaveChangesAsync();
            Notify("Stock additon successfull!", title: stock + " " + live.Name + " have been added to the stock!", notificationType: NotificationType.success);
            return View("EditIndex", db.Livestocks);
        }
    }
}
