using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IKGAi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IKGAi.Controllers
{
    public class CareerController : Controller
    {

        private DB db = new DB();

        // GET: CareerController
        public ActionResult Index()
        {
            List < Career > careers = new List<Career> ();
            careers = db.Career.ToList ();
            return View(careers);
        }

        // GET: CareerController/Details/5
        public ActionResult Details(int id)
        {
            var careerDetails = db.Career.Include(x=> x.User).Where(x => x.Id == id).SingleOrDefault();
            return View(careerDetails);
        }

        // GET: CareerController/Create
        public ActionResult Create()
        {
            var users = db.User.ToList ();
            var users_sel_list = new SelectList(users, "Id", "name");
            ViewBag.users = users_sel_list;
            return View();
        }

        // POST: CareerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Career newCareer)
        {
            try
            {
                db.Career.Add(newCareer);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
           
        }

        // GET: CareerController/Edit/5
        public ActionResult Edit(int id)
        {
            var career = db.Career.Find(id);
            var users = db.User.ToList ();
            var user_sel_list = new SelectList(users, "Id", "name");
            ViewBag.users = user_sel_list;
            return View(career);
        }

        // POST: CareerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Career updatedCareer)
        {
            try
            {
                var existingCareer = db.Career.Find(updatedCareer.Id);
                if (existingCareer != null)
                {
                    existingCareer.careerName = updatedCareer.careerName;
                    existingCareer.requirements = updatedCareer.requirements;
                    existingCareer.description = updatedCareer.description;
                    db.SaveChanges();
                }
                else
                {
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CareerController/Delete/5
        public ActionResult Delete(int id)
        {
            var careerDelete = db.Career.Find(id);
            return View();
        }

        // POST: CareerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var careerDelete = db.Career.Find(id);
                if(careerDelete != null)
                {
                    db.Career.Remove(careerDelete);
                    db.SaveChanges();

                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
