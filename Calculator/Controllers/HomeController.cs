using Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Calculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        public string compute(float numOne, float numTwo, string operation)
        {
            float computed;
            bool success = true;

            switch(operation)
            {
                case "+":
                    computed = numOne + numTwo;
                    break;
                case "-":
                    computed = numOne - numTwo;
                    break;
                case "*":
                    computed = numOne * numTwo;
                    break;
                case "÷":
                    computed = numOne / numTwo;
                    break;
				default: 
                    computed = 0;
                    success = false;
                    break;
            }


			var result = new Result
            {
                data = computed.ToString(),
                success = success,
            };

            return JsonSerializer.Serialize(result);
        }

	}
}
