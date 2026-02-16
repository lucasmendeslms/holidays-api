using HolidayApi.Strategies;
using HolidayApi.Data.ValueObjects;
using HolidayApi.Data.Models;

namespace HolidayApi.Middlewares
{
    public class RequestHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;

        public RequestHandlerMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _scopeFactory = scopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            if (string.IsNullOrEmpty(path) || path == "/" || path.StartsWith("/swagger"))
            {
                await _next(context);
                return;
            }

            var ibgeCodeParameter = context.Request.RouteValues["ibgeCode"]?.ToString();
            var dateParameter = context.Request.RouteValues["date"]?.ToString();

            if (string.IsNullOrEmpty(ibgeCodeParameter) || string.IsNullOrEmpty(dateParameter))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = "Missing parameters: all fields are required"
                });
                return;
            }

            var ibgeCode = IbgeCode.Validate(int.Parse(ibgeCodeParameter));
            var date = HolidayDate.Create(dateParameter);

            if (ibgeCode.IsFailure)
            {
                context.Response.StatusCode = ibgeCode.Error!.Code;
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = ibgeCode.Error.Description
                });
                return;
            }

            if (date.IsFailure)
            {
                context.Response.StatusCode = date.Error!.Code;
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = date.Error.Description
                });
                return;
            }

            using var scope = _scopeFactory.CreateScope();

            var strategy = scope.ServiceProvider.GetRequiredService<HolidayStrategyContext>().SetStrategy(ibgeCode.Value!);

            if (strategy.IsFailure)
            {
                context.Response.StatusCode = strategy.Error!.Code;
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = strategy.Error.Description
                });
                return;
            }

            context.Items[typeof(RequestContext)] = new RequestContext(ibgeCode.Value!, date.Value!, strategy.Value!);

            await _next(context);
        }
    }
}