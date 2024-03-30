using AuthService.Routs;

namespace AuthService.Handlers
{
    public static class EndpointConfig
    {
        public static void ConfigureEndpoints(this WebApplication app)
        {
            app.MapGet("/", async (HttpContext context) =>
            {
                
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Index.html");
                if (File.Exists(filePath))
                {        
                    var content = await File.ReadAllTextAsync(filePath);
                    await context.Response.WriteAsync(content);
                }
                else
                    context.Response.StatusCode = 404;
            });
            app.MapPost("/register", RegisterHandler.Handle);
            app.MapPost("/authorize", AuthorizeHandler.Handle);
            app.MapGet("/feed", FeedHandler.Handle);
        }
    }
}
