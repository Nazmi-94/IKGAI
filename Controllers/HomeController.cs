using AutoMapper;
using IKGAI.BLL.Models.DTO.Comment;
using IKGAI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace IKGAi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7121/");
            _mapper = mapper;
        }

        public IActionResult Login()
        {
            return View(); // Returns the Login.cshtml as a full-page view
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Call the API endpoint to get all comments
                var response = await _httpClient.GetAsync("api/CommentAPI");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Failed to fetch comments. Status Code: {response.StatusCode}");
                    return StatusCode((int)response.StatusCode, "Failed to load comments.");
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var commentDtos = JsonConvert.DeserializeObject<List<CommentDto>>(jsonString);

                // Pass the comments to the view
                return View(commentDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching comments from the API.");
                return StatusCode(500, "Internal server error.");
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
