using Microsoft.Azure.Functions.Worker.Http; 
using Microsoft.Extensions.Logging;           
using System.Net;                             
using System.Text.Json; 

public static class ErrorResponseHandler
{
    public static async Task<HttpResponseData> HandleExceptionAsync(
        HttpRequestData req,
        ILogger logger,
        Exception ex)
    {
        HttpStatusCode statusCode;
        string errorMessage;

        switch (ex)
        {
            case JsonException _:
                statusCode = HttpStatusCode.BadGateway;
                errorMessage = ex.Message;
                break;
            case HttpRequestException _:
                statusCode = HttpStatusCode.BadGateway;
                errorMessage = ex.Message;
                break;
            case ArgumentException argEx:
                statusCode = HttpStatusCode.BadRequest;
                errorMessage = argEx.Message;
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                errorMessage = "An unexpected error occurred.";
                break;
        }

        logger.LogError(ex, errorMessage);

        var response = req.CreateResponse(statusCode);
        await response.WriteAsJsonAsync(new { error = errorMessage });
        return response;
    }
}
