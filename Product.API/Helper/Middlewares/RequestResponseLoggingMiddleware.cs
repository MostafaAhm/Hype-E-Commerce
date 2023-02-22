using Product.Application.Contracts.Persistence;
using Product.Domain.Entities;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using Product.Application.Resources.Common;

namespace Product.API.Helper.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private APILogHistory _aPILogHistory;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            await LogRequest(context);
            await LogResponse(context, unitOfWork);
        }




        private async Task LogRequest(HttpContext context)
        {
            _aPILogHistory = new APILogHistory()
            {
                Method = context.Request.Method,
                Schema = context.Request.Scheme.ToString(),
                Host = context.Request.Host.ToString(),
                Path = context.Request.Path,
                QueryString = context.Request.QueryString.ToString(),
                RequestBody = await ReadBodyFromRequest(context.Request),
                CreatedBy = context.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value

            };
        }
        private async Task LogResponse(HttpContext context, IUnitOfWork unitOfWork)
        {
            HttpResponse response = context.Response;
            var originalResponseBody = response.Body;
            using var newResponseBody = new MemoryStream();
            response.Body = newResponseBody;

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                BaseResponse message = new BaseResponse()
                {
                    IsSuccess = false,
                    ResponseMessage = "An internal server has occurred",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

                var jsonResponse = JsonSerializer.Serialize(message);
                await context.Response.WriteAsync(jsonResponse);
            }


            newResponseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText =
                await new StreamReader(response.Body).ReadToEndAsync();

            newResponseBody.Seek(0, SeekOrigin.Begin);
            await newResponseBody.CopyToAsync(originalResponseBody);


            _aPILogHistory.ResponseBody = responseBodyText;
            _aPILogHistory.StatusCode = context.Response.StatusCode;

            //if (_aPILogHistory.StatusCode != (int)HttpStatusCode.OK && _aPILogHistory.StatusCode != (int)HttpStatusCode.NoContent)
            //{
            unitOfWork.APILogHistory.Add(_aPILogHistory);
            unitOfWork.Complete();
            //}

        }

        private async Task<string> ReadBodyFromRequest(HttpRequest request)
        {
            request.EnableBuffering();

            var body = request.Body;

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body.Position = 0;  //rewinding the stream to 0

            return bodyAsText;
        }
    }
}
