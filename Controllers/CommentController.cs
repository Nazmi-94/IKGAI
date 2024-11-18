using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IKGAI.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IKGAI.Domain.Entities;
using IKGAI.Infrastructure.Data;
using Newtonsoft.Json;
using IKGAI.Domain.Entities.IModels;
using IKGAI.BLL.Models.RequestModels;
using System.Net.Http;
using System.Text;

namespace IKGAi.Controllers
{
    public class CommentController : Controller
    {
        private static DB _db;
        private readonly HttpClient _httpClient;
        public CommentController(DB db, HttpClient httpClient)
        {
            _db = db;
            _httpClient = httpClient;
        }

        // GET: ProduktetController
        public async Task<ActionResult> Index()
        {
            //HttpClient client = new HttpClient();

            var response = await _httpClient.GetAsync("https://localhost:7108/api/CommentAPI");
            if (response.IsSuccessStatusCode)
            {
                // Read the content as a string
                var jsonString = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON string to a C# object
                //var produktet = JsonSerializer.Deserialize<List<Produktet>>(jsonString);
                // Or if using Newtonsoft.Json
                var comment = JsonConvert.DeserializeObject<List<Comment>>(jsonString);

                // Now you can work with the `produktet` object
                return View(comment);
            }
            else
            {
                // Handle the error response
                throw new Exception($"Error: {response.StatusCode}");
            }

        }


        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            //var commentDetails = _db.Comment.Include(x=> x.User).Where(x => x.Id == id).SingleOrDefault();
            ////var commentDe = db.Comment.Find(id);
            //return View(commentDetails);

            var commentDetails = _db.Comment.Include(x => x.User).Where(x => x.Id == id).SingleOrDefault();
            return PartialView("_DetailsPartial", commentDetails);  
        }

        // GET: CommentController/Create
        public ActionResult Create()
        {
          
            var users = _db.User.ToList();
            var user_sel_list = new SelectList(users, "Id", "name");
            ViewBag.users = user_sel_list;
           
            return PartialView("_CreatePartial");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentRequestModel commentRequestModel)
        {
            if (!ModelState.IsValid)
                return View(commentRequestModel);

            var content = new StringContent(JsonSerializer.Serialize(commentRequestModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/CommentAPI/Create", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index"); // Redirect to a list of comments or another page

            ModelState.AddModelError("", "An error occurred while creating the comment.");
            return View(commentRequestModel);
        }



        //// GET: CommentController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    //var comment = _db.Comment.Find(id);
        //    //var users = _db.User.ToList();
        //    //var user_sel_list = new SelectList(users, "Id", "name");
        //    //ViewBag.users = user_sel_list;
        //    //return View(comment);
        //    var comment = _db.Comment.Find(id);
        //    var users = _db.User.ToList();
        //    var user_sel_list = new SelectList(users, "Id", "name");
        //    ViewBag.users = user_sel_list;
        //    return PartialView("_EditPartial", comment); 
        //}


        [HttpGet("api/comment/edit/{id}")]
        public IActionResult GetCommentForEdit(int id)
        {
            var comment = _db.Comment.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            var users = _db.User.ToList();

            return Json(new { comment, users });
        }

        public IActionResult RenderEditPartialView()
        {
            return PartialView("_EditPartial");
        }

        [HttpPost("api/comment/edit")]
        public IActionResult SubmitEditComment([FromBody] Comment updatedComment)
        {
            var existingComment = _db.Comment.Find(updatedComment.Id);

            if (existingComment == null)
            {
                return Json(new { success = false, message = "Comment not found." });
            }

            existingComment.CommentText = updatedComment.CommentText;
            existingComment.CommentDate = updatedComment.CommentDate;
            existingComment.UserId = updatedComment.UserId;

            _db.SaveChanges();

            return Json(new { success = true, updatedComment });
        }
        //// POST: CommentController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Comment updatedCommnet)
        //{
        //    try
        //    {
        //        var existingComment = _db.Comment.Find(updatedCommnet.Id);
        //        if (existingComment != null)
        //        {
        //            existingComment.commentText = updatedCommnet.commentText;
        //            existingComment.commentDate = updatedCommnet.commentDate;
        //            existingComment.userId = updatedCommnet.userId;
        //            _db.SaveChanges();
        //            return Json(updatedCommnet);  // Return the updated comment data
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return Json(new { success = false, message = "An error occurred" });
        //    }
        //}

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            //var comment = _db.Comment.Find(id);

            //return View(comment);
            var comment = _db.Comment.Find(id);
            return PartialView("_DeletePartial", comment);  
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            //try
            //{
            //    var commetD = _db.Comment.Find(id);
            //    if (commetD != null)
            //    {
            //        _db.Comment.Remove(commetD);
            //        _db.SaveChanges();
            //    }    
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
            var comment = _db.Comment.Find(id);
            if (comment == null)
            {
                return Json(new { success = false, message = "Comment not found." });
            }

            _db.Comment.Remove(comment);
            _db.SaveChanges();

            return Json(new { success = true, id = id });
        }
    }
}
