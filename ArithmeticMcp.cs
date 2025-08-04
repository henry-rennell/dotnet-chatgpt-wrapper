using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Numerics;
using System.Net;
using FunctionApp.Models;
using Microsoft.Extensions.Logging;
using Azure;

namespace FunctionApp
{
    public class ArithmeticMcp
    {
        private readonly ILogger<ArithmeticMcp> _logger;
        public ArithmeticMcp(ILogger<ArithmeticMcp> logger)
        {
            _logger = logger;
        }
        [Function("ArithmeticMcp")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "multiply")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);

            try
            {
                var bodyString = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonSerializer.Deserialize<MultiplyRequest>(
                    bodyString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // checking for integrity of data sent to tool, both a and b are present, both strings
                if (data == null || string.IsNullOrWhiteSpace(data.a) || string.IsNullOrWhiteSpace(data.b))
                {
                    await response.WriteAsJsonAsync(new { result = (string)null, error = "Both 'a' and 'b' must be provided." });
                    return response;
                }
                if (!BigInteger.TryParse(data.a, out var a) || !BigInteger.TryParse(data.b, out var b))
                {
                    await response.WriteAsJsonAsync(new { result = (string)null, error = "'a' and 'b' must be integers" });
                    return response;
                }
                var product = a * b;
                await response.WriteAsJsonAsync(new { result = product.ToString(), error = (string)null });
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Processing Multiply Request:" + ex.Message);
                await response.WriteAsJsonAsync(new { result = (string)null, error = "Internal error: " + ex.Message });
                return response;
            }
        }

    }
}