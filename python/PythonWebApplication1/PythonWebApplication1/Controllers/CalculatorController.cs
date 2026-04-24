using Microsoft.AspNetCore.Mvc;
using PythonWebApplication1.Models;
using System.Diagnostics;
using System.Text.Json;

namespace PythonWebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult Add(CalculationRequest request)
        {
            string pythonExe = "python";

            string scriptPath = Path.Combine(Directory.GetCurrentDirectory(),
                                             "PythonScripts",
                                             "calculator.py");

            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = pythonExe,
                Arguments = $"\"{scriptPath}\" {request.Num1} {request.Num2}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(start))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                Console.WriteLine("Output: " + output);
                Console.WriteLine("Error: " + error);

                if (!string.IsNullOrWhiteSpace(error))
                {
                    return BadRequest(new { error = error });
                }

                var jsonResult = JsonSerializer.Deserialize<object>(output);
                return Ok(jsonResult);
            }
        }
    }
}