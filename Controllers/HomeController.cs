using IKGAI.Domain.Entities;
using IKGAI.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        public IActionResult Login()
        {
            return View(); // Returns the Login.cshtml as a full page view
        }
        public async Task<IActionResult> Index(int id)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("https://localhost:7108/api/CommentAPI");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

               
                // Deserialize the JSON string into a List<Comment>
                List<Comment> comments;
                try
                {
                    comments = JsonConvert.DeserializeObject<List<Comment>>(jsonString);
                }
                catch (Exception ex)
                {
                    comments = new List<Comment>(); // Initialize an empty list if deserialization fails
                }

                // Return the comments to the view
                return View(comments);
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
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
                        user = new { name = comment.User.Name }, 
                        commentText = comment.CommentText
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


        public IActionResult GetWeatherForecast;


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
