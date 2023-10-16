using Microsoft.EntityFrameworkCore;
using Models;

namespace MVC
{
    public class CategoryMiddleWare
    {
    //    public RequestDelegate next;
    //    public CategoryMiddleWare(RequestDelegate next)
    //    {
    //        this.next = next;
    //    }
    //    public async Task InvokeAsync(HttpContext context)
    //    {
    //        MyDBContext db = new MyDBContext();
    //        if (context.Request.Path == "/category")
    //        {
    //            await context.Response.WriteAsJsonAsync(db.Categories.ToList());
    //        }
    //        else if (context.Request.Path == "/categorybyname")
    //        {
    //            string name =context.Request.Query["Name"];

    //            var mycategory = db.Categories.FirstOrDefault(c =>c.Name == name );

    //            await context.Response.WriteAsync(mycategory.Name);
    //        }

    //        await next(context);
    //    }
    }
}
