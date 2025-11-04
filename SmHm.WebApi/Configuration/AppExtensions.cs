namespace SmHm.WebApi.Configuration
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseApplicationSpec(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("swagger/index.html");
                    return;
                }

                await next();
            });

            app.MapControllers();

            return app;
        }
    }
}
