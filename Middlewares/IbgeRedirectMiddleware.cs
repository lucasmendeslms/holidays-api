namespace HolidayApi.Middlewares
{
    public class IbgeRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public IbgeRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/feriados"))
            {
                Console.WriteLine("Teste " + context.Request.Path.Value.Split('/')[2]);
            }
        }

    }
}


// public class IbgeRedirectMiddleware
// {
//     private readonly RequestDelegate _next;

//     public IbgeRedirectMiddleware(RequestDelegate next)
//     {
//         _next = next;
//     }

//     public async Task Invoke(HttpContext context)
//     {
//         if (context.Request.Method == HttpMethods.Put &&
//             context.Request.Path.StartsWithSegments("/feriados"))
//         {
//             var pathSegments = context.Request.Path.Value.Split('/');
//             if (pathSegments.Length >= 3)
//             {
//                 var ibgeCode = pathSegments[2];

//                 if (ibgeCode.Length == 2) // Estadual
//                     context.Items["RedirectPath"] = $"/feriados/estaduais/{pathSegments[3]}";
//                 else if (ibgeCode.Length == 7) // Municipal
//                     context.Items["RedirectPath"] = $"/feriados/municipais/{pathSegments[3]}";
//             }
//         }

//         await _next(context);
//     }
// }
