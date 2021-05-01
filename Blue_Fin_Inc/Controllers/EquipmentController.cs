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
    public class EquipmentController : BaseController
    {
        //DB field
        private readonly ApplicationContext db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;

        //Constructor
        public EquipmentController(IWebHostEnvironment hostEnvironment, IConfiguration configuration, IWebHostEnvironment env)
        {
            db = new ApplicationContext(configuration, env);
            this._hostEnvironment = hostEnvironment;
            _configuration = configuration;
            db.Database.Migrate();
            db.SeedDB(configuration, env);
        }

        // GET: EquipmentController
        public async Task<IActionResult> Index()
        {
            var list = await db.Equipments.ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> EditIndex()
        {
            var list = await db.Equipments.ToListAsync();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string EquSearch)
        {
            ViewData["GetEquipmentDetails"] = EquSearch;

            var EquQuery = from p in db.Equipments select p;

            if (!String.IsNullOrEmpty(EquSearch))
            {
                EquQuery = EquQuery.Where(p => p.Name.Contains(EquSearch) || p.Description.Contains(EquSearch) || p.Manufacturer.Contains(EquSearch));
            }
            return View(await EquQuery.AsNoTracking().ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> EditIndex(string EquSearch)
        {
            ViewData["GetEditEquipmentDetails"] = EquSearch;

            var EquQuery = from p in db.Equipments select p;

            if (!String.IsNullOrEmpty(EquSearch))
            {
                EquQuery = EquQuery.Where(p => p.Name.Contains(EquSearch) || p.Description.Contains(EquSearch) || p.Manufacturer.Contains(EquSearch));
            }
            return View(await EquQuery.AsNoTracking().ToListAsync());
        }

        // GET: EquipmentController/Details/5
        public async Task<IActionResult> Details(int id, string json)
        {
            Equipment foundEquipment = await db.Equipments.FindAsync(id);
            if (foundEquipment != null)
            {
                if (json == "yes")
                {
                    return Ok(foundEquipment);
                }
                return View(foundEquipment);
            }
            else
            {
                return NotFound("No equipment found with product code: " + id + "\nIf this is new equipment please add it to the system.");
            }
        }

        // GET: EquipmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Manufacturer,Lenght,Width,Height,Colour,Weight,ProductCode,Name,Description,Stock,Price,ImageFile")] Equipment newEquipment)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(newEquipment.ImageFile.FileName);
                string extension = Path.GetExtension(newEquipment.ImageFile.FileName);
                newEquipment.ImageName = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/images/", filename);

                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await newEquipment.ImageFile.CopyToAsync(filestream);
                }

                db.Equipments.Add(newEquipment);
                await db.SaveChangesAsync();
                Notify("Product creation successfull!", title: newEquipment.Name + " has been added to the system!", notificationType: NotificationType.success);
                return View("Index", db.Equipments);
            }
            else
            {
                Notify("Please try again or contact your system admin!", title: newEquipment.Name + " could not be added to the system!", notificationType: NotificationType.error);
                return View("Index", db.Equipments);
            }
        }

        // POST: EquipmentController/Create

        // GET: EquipmentController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Equipment equip = await db.Equipments.FindAsync(id);
            if (equip == null)
            {
                return NotFound();
            }
            return View(equip);
        }

        // POST: EquipmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Manufacturer,Lenght,Width,Height,Colour,Weight,ProductCode,Name,Description,Stock,Price,ImageFile,ImageName")] Equipment equip)
        {
            if (id != equip.ProductCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var titleIn = "\"" + equip.Name + "\" has been updated succesfully!";
                    db.Update(equip);
                    await db.SaveChangesAsync();
                    Notify("Data saved successfully", title: titleIn, notificationType: NotificationType.success);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equip.ProductCode))
                    {
                        var titleIn = "\"" + equip.Name + "\" could not be updated!";
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
            return View(equip);
        }

        // GET: EquipmentController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var product = await db.Equipments.FirstOrDefaultAsync(p => p.ProductCode == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: EquipmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await db.Equipments.FindAsync(id);

            var imagePath = Path.Combine(_hostEnvironment.WebRootPath + "/images/" + product.ImageName);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            db.Equipments.Remove(product);
            await db.SaveChangesAsync();
            Notify("Product deletion successfull!", title: product.Name + "has been deleted from the system!", notificationType: NotificationType.success);
            return RedirectToAction(nameof(EditIndex));
        }

        private bool EquipmentExists(int id)
        {
            return db.Equipments.Any(e => e.ProductCode == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddStock(int id, int stock)
        {
            if (stock < 1)
            {
                Notify("Stock additon unsuccessfull!", title: "Stock to add must be greater than 0!", notificationType: NotificationType.error);
                return View("EditIndex", db.Equipments);
            }

            Equipment equip = db.Equipments.FirstOrDefault(p => p.ProductCode == id);
 
            db.Equipments.Attach(equip);
            equip.Stock += stock;

            db.Entry(equip).Property(x => x.Stock).IsModified = true;

            await db.SaveChangesAsync();
            Notify("Stock additon successfull!", title: stock + " units have been added to " + equip.Name + "!", notificationType: NotificationType.success);
            return View("EditIndex", db.Equipments);
        }
    }
}

