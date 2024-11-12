using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IKGAI.Infrastructure.Data;
using IKGAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace IKGAi.Controllers
{
    public class UserProfileController : Controller
    {
        private DB _db;
        public UserProfileController(DB db)
        {
            _db = db;
        }


        // GET: UserProfileController
        public ActionResult Index()
        {
            List<UserProfile> listProfiles = new List<UserProfile>();
            listProfiles = _db.Profiles.ToList();
            return View(listProfiles);
        }

        // GET: UserProfileController/Details/5
        public ActionResult Details(int id)
        {

            var profile = _db.Profiles.Find(id);
            return View(profile);
        }

        // GET: UserProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserProfile newProfile)
        {
            try
            {
                _db.Profiles.Add(newProfile);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            var profile = _db.Profiles.Find(id);
            return View(profile);

        }

        // POST: UserProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile updatedProfile)
        {
            try
            {
                var existingProfile = _db.Profiles.Find(updatedProfile.profileId);
                if (existingProfile != null)
                {
                    existingProfile.experience = updatedProfile.experience;
                    existingProfile.education = updatedProfile.education;
                    existingProfile.skills = updatedProfile.skills;
                    existingProfile.interests = updatedProfile.interests;
                }    
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            var profile = _db.Profiles.Find(id);

            return View(profile);
        }

        // POST: UserProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var profile = _db.Profiles.Find(id);
                if (profile != null)
                {
                    _db.Profiles.Remove(profile);
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
