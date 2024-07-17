using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IKGAi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace IKGAi.Controllers
{
    public class CommentController : Controller
    {
        private DB db = new DB();

        
        // GET: CommentController
        public ActionResult Index()
        {
            List<Comment> comments = new List<Comment>();
            comments = db.Comment.ToList();
            return View(comments);
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            var commentDetails = db.Comment.Include(x=> x.User).Where(x => x.Id == id).SingleOrDefault();
            //var commentDe = db.Comment.Find(id);
            return View(commentDetails);
        }

        // GET: CommentController/Create
        public ActionResult Create()
        {
            var users = db.User.ToList();
            var user_sel_list = new SelectList(users, "Id", "name");
            ViewBag.users = user_sel_list;
           
            return View();
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment newComment)
        {
            try
            {
                
                db.Comment.Add(newComment);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            var comment = db.Comment.Find(id);
            var users = db.User.ToList();
            var user_sel_list = new SelectList(users, "Id", "name");
            ViewBag.users = user_sel_list;
            return View(comment);
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment updatedCommnet)
        {
            try
            {
                var existingComment = db.Comment.Find(updatedCommnet.Id);
                if (existingComment != null)
                {
                    existingComment.commentText = updatedCommnet.commentText;
                    existingComment.commentDate = updatedCommnet.commentDate;
                    existingComment.userId = updatedCommnet.userId;
                    db.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            var comment = db.Comment.Find(id);

            return View(comment);
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var commetD = db.Comment.Find(id);
                if (commetD != null)
                {
                    db.Comment.Remove(commetD);
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
