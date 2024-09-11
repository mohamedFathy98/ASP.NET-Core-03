namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // builder.Services.AddControllers(); //Api
            builder.Services.AddControllersWithViews();//MVC
            var app = builder.Build();
            // app.MapGet("/Products/Get/2", () => "Hello World!");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "Default",
                    pattern: "{Controller=Home}/{Action=Index}/{id?}"
                    // constraints: new {id = new IntRouteConstraint()}
                    );
            });

            app.Run();
        }
    }
}

// https://baseUrl/products/Git/2