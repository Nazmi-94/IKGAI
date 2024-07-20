using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IKGAi.Models;


namespace IKGAi.Controllers
{
    public class UserController : Controller
    {
        private static DB _db;

        public UserController(DB db)
        {
            _db = db;
        }

        // GET: UserController
        public ActionResult Index()
        {
            List<User> users = new List<User>(); 
            users = _db.User.ToList();

            return View(users);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            var userDetails = _db.User.Find(id);
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

                _db.User.Add(newUser);
                _db.SaveChanges();

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
            var userEdit = _db.User.Find(id);
            return View(userEdit);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User updatedUser)
        {
            try
            {
                var existingUser = _db.User.Find(updatedUser.Id);
                if (existingUser != null)
                {
                    existingUser.name = updatedUser.name;
                    existingUser.email = updatedUser.email;
                    existingUser.password = updatedUser.password;
                    _db.SaveChanges();
                   
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
            var userDelete = _db.User.Find(id);
            return View(userDelete);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var userDelete = _db.User.Find(id); 
                if (userDelete != null)
                {
                    _db.User.Remove(userDelete);
                    _db.SaveChanges();  
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
