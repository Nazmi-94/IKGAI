using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using IKGAI.BLL.Models.DTO.Comment;

namespace IKGAI.Presentation.Controllers
{
    public class CommentController : Controller
    {
        private readonly HttpClient _httpClient;

        public CommentController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7121/api/CommentAPI/");
        }

        // Get all comments
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var comments = JsonSerializer.Deserialize<IEnumerable<CommentDto>>(responseData);
                return View(comments);
            }
            return View(new List<CommentDto>()); // Handle errors gracefully
        }

        // View comment details
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var comment = JsonSerializer.Deserialize<CommentDto>(responseData);
                return View(comment);
            }
            return NotFound();
        }

        // Create a new comment - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create a new comment - POST
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createCommentDto);
            }

            var jsonData = JsonSerializer.Serialize(createCommentDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            // Handle error messages
            ModelState.AddModelError("", "An error occurred while creating the comment.");
            return View(createCommentDto);
        }

        // Edit an existing comment - GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var comment = JsonSerializer.Deserialize<UpdateCommentDto>(responseData);
                return View(comment);
            }
            return NotFound();
        }

        // Edit an existing comment - POST
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCommentDto updateCommentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateCommentDto);
            }

            var jsonData = JsonSerializer.Serialize(updateCommentDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{updateCommentDto.Id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "An error occurred while updating the comment.");
            return View(updateCommentDto);
        }

        // Delete a comment - GET (Confirmation View)
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var comment = JsonSerializer.Deserialize<CommentDto>(responseData);
                return View(comment);
            }
            return NotFound();
        }

        // Delete a comment - POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "An error occurred while deleting the comment.");
            return RedirectToAction(nameof(Index));
        }
    }
}
