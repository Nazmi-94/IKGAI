using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IKGAi.Models;


namespace IKGAi.Controllers
{
    public class UserController : Controller
    {
        private DB db = new DB();

        // GET: UserController
        public ActionResult Index()
        {
            List<User> users = new List<User>(); 
            users = db.User.ToList();

            return View(users);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            var userDetails = db.User.Find(id);
            return View(userDetails);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User newUser)
        {
            try
            {

                db.User.Add(newUser);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            var userEdit = db.User.Find(id);
            return View(userEdit);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User updatedUser)
        {
            try
            {
                var existingUser = db.User.Find(updatedUser.Id);
                if (existingUser != null)
                {
                    existingUser.name = updatedUser.name;
                    existingUser.email = updatedUser.email;
                    existingUser.password = updatedUser.password;
                    db.SaveChanges();
                   
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            var userDelete = db.User.Find(id);
            return View(userDelete);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var userDelete = db.User.Find(id); 
                if (userDelete != null)
                {
                    db.User.Remove(userDelete);
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
