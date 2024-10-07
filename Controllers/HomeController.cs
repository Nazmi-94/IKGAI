using IKGAi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace IKGAi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static DB _db;

        public HomeController(ILogger<HomeController> logger, DB db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index(int id)
        {
            var lastComments = _db.Comment.Include(c=> c.User).Take(3).ToList();
            return View(lastComments);

        }

        [HttpGet]
        public IActionResult LoadMoreComments(int skip)
        {
            try
            {
                var moreComments = _db.Comment
                    .Skip(skip)
                    .Take(3)
                    .Select(comment => new
                    {
                        id = comment.Id,
                        user = new { name = comment.User.name }, 
                        commentText = comment.commentText
                    })
                    .ToList();

                return Json(moreComments);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
