using IKGAI.BLL.Models.DTO.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace IKGAI.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: UserController
        public async Task<ActionResult> Index()
        {
            // Fetch all users from the API
            var response = await _httpClient.GetAsync("https://localhost:7121/api/Users");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<UserDto>>(jsonString);
                return View(users); // Pass the list to the view
            }
            else
            {
                return View(new List<UserDto>()); // Return an empty list in case of an error
            }
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7121/api/Users/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserDto>(jsonString);
                return View(user);
            }
            else
            {
                return NotFound(); // Handle user not found
            }
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View(); // Render a form for creating a new user
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
                return View(createUserDto);

            var content = new StringContent(JsonConvert.SerializeObject(createUserDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7121/api/Users", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index)); // Redirect to the user list
            }

            ModelState.AddModelError("", "An error occurred while creating the user.");
            return View(createUserDto);
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7121/api/Users{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserDto>(jsonString);
                return View(user); // Render a confirmation view
            }
            else
            {
                return NotFound();
            }
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7121/api/Users/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index)); // Redirect to the user list
            }
            else
            {
                ModelState.AddModelError("", "An error occurred while deleting the user.");
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}
