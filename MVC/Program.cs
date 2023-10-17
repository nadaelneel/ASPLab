
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Models;
using MVC;
using Repository;

public class Program
{
    static void Main()
    {
        WebApplicationBuilder builder =
             WebApplication.CreateBuilder();

        builder.Services.AddDbContext<MyDBContext>(db =>
        {
            db.UseLazyLoadingProxies().UseSqlServer(
                builder.Configuration.GetConnectionString("MyDB"));
        });
        //
        builder.Services.AddIdentity<User, IdentityRole>(i => {
            i.User.RequireUniqueEmail = true;
            i.SignIn.RequireConfirmedPhoneNumber = false;
            i.SignIn.RequireConfirmedEmail = false;
            i.SignIn.RequireConfirmedAccount = false;
        })
           .AddEntityFrameworkStores<MyDBContext>();
        builder.Services.Configure<IdentityOptions>(i =>
        {
            i.Password.RequireNonAlphanumeric = false;
            i.Password.RequireUppercase = false;

        });
        builder.Services.ConfigureApplicationCookie(i =>
        {
            i.LoginPath = "/Account/SignIn";

        });
        builder.Services.AddScoped(typeof(ProductManger));
        builder.Services.AddScoped(typeof(CategoryManger));
        builder.Services.AddScoped(typeof(UniteOfWork));
        builder.Services.AddScoped(typeof(AccountManger));
        builder.Services.AddScoped(typeof(RoleManager));
        //builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, UesrClaimsFactory>();
        builder.Services.AddControllersWithViews();

        var App = builder.Build();

        App.UseStaticFiles(new StaticFileOptions() { 
            
            FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory()+ "/Content"),
            RequestPath = ""
        });
        App.MapControllerRoute("Default", "{Controller=Product}/{Action=GetAll}/{id?}");

        //App.MapDefaultControllerRoute();
        App.Run();
    }
}
