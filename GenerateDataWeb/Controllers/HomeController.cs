using GenerateDataWeb.Models;
using GenerateDataWeb.Models.GenerateDataWeb.Models;
using GenerateDataWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GenerateDataWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonService _personService;

        public HomeController(ILogger<HomeController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        public IActionResult Index()
        {
            // coba edit simulasi git
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] PersonWrapper data)
        {
            if (data?.Payload == null || !data.Payload.Any())
                return BadRequest("Data payload kosong");

            var result = await _personService.SubmitPersonDataAsync(data);

            if (result.Any())
                return BadRequest(result); // return multiple error lines

            return Ok(new { message = "Insert berhasil" });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
