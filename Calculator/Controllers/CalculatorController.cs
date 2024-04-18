using Calculator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Calculator.Controllers
{
    public static class Operation
    {
        public const string Add = "+";
        public const string Substract = "-";
        public const string Multiply = "*";
        public const string Divide = "÷";
        public const string Square = "Sqrt";
        public const string Floor = "Floor";
        public const string Pi = "π";
        public const string Sin = "Sin";
        public const string Procent = "%";
        public const string Ceil = "Ceil";
        public const string Cos = "Cos";
        public const string Tan = "Tan";
    }

    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpGet]
        public string Compute(double numOne, double numTwo, string operation)
        {
            double computed = 0;
            bool success = true;
            string message = string.Empty;

            try
            {
                switch (operation)
                {
                    case Operation.Add:
                        computed = numOne + numTwo;
                        break;
                    case Operation.Substract:
                        computed = numOne - numTwo;
                        break;
                    case Operation.Multiply:
                        computed = numOne * numTwo;
                        break;
                    case Operation.Divide:
                        computed = numOne / numTwo;
                        break;
                    case Operation.Square:
                        computed = Math.Pow(numTwo, 2);
                        break;
                    case Operation.Floor:
                        computed = Math.Floor(numTwo);
                        break;
                    case Operation.Pi:
                        computed = Math.PI;
                        break;
                    case Operation.Sin:
                        computed = Math.Sin(numTwo);
						break;
                    case Operation.Procent:
                        computed = numTwo / 100;
						break;
                    case Operation.Ceil:
                        computed = Math.Ceiling(numTwo);
						break;
                    case Operation.Cos:
                        computed = Math.Cos(numTwo);
						break;
                    case Operation.Tan:
                        computed = Math.Tan(numTwo);
						break;
                    default:
                        success = false;
                        message = "Wrong operation";
                        break;
                }
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }

            var result = new Result
            {
                data = computed.ToString(),
                message = message,
                success = success,
            };

            return JsonSerializer.Serialize(result);
        }
    }
}
