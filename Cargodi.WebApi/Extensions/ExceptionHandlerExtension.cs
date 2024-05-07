using Cargodi.WebApi.Middlewares;

namespace Cargodi.WebApi.Extensions;

public static class ExceptionHandlerExtension
{
    public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}