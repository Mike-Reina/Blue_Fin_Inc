using Blue_Fin_Inc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Controllers
{
    public class EquipmentController : Controller
    {
        //DB field
        private readonly ApplicationContext db;

        //Constructor
        public EquipmentController()
        {
            db = new ApplicationContext();

            db.Database.EnsureCreated();
        }


        static List<Equipment> EquipmentList = new List<Equipment>()
        {
            new Equipment("Juwel", 92, 41, 55, "Black", "50 kg", 1001, "Juwel Vision 180", "Painstaking workmanship from Germany, top - quality materials and perfectly tuned technology guarantee the very best of quality and safety, meaning a long service life for your new aquarium.", 610.99),
            new Equipment("Juwel", 45, 45, 45, "Black", "50 kg", 1002, "Juwel Cube 45", "Great Beginnner tank which wont take up much space, great quality for a great price", 200.00)
        };


        // GET: EquipmentController
        public ActionResult Index()
        {
            
            return View(EquipmentList);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string EquSearch)
        {
            ViewData["GetEquipmentDetails"] = EquSearch;

            var EquQuery = from p in EquipmentList select p;

            if (!String.IsNullOrEmpty(EquSearch))
            {
                EquQuery = EquQuery.Where(p => p.Name.Contains(EquSearch) || p.Description.Contains(EquSearch) || p.Manufacturer.Contains(EquSearch));
            }
            return View(EquQuery);
        }

        // GET: EquipmentController/Details/5
        public ActionResult Details(int id)
        {
            Equipment foundEquipment = EquipmentList.FirstOrDefault(p => p.ProductCode == id);
            if (foundEquipment != null)
            {
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

        // POST: EquipmentController/Create
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

        // GET: EquipmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EquipmentController/Edit/5
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

        // GET: EquipmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EquipmentController/Delete/5
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
