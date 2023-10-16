using Models;

namespace MVC
{
    public class ProductMiddleWare
    {
        //public RequestDelegate next;

        //public ProductMiddleWare(RequestDelegate next)
        //{
        //    this.next = next;
        //}
        //public async Task InvokeAsync(HttpContext context)
        //{
        //    MyDBContext db = new MyDBContext();
        //    if (context.Request.Path == "/product")
        //    {
        //        await context.Response.WriteAsJsonAsync(db.Products.ToList());
        //    }
        //    else if (context.Request.Path == "/productbyid")
        //    {
        //        //_____/productbyid?ID=3
        //        int id = int.Parse(context.Request.Query["ID"]);

        //        var myproduct = db.Products.FirstOrDefault(p => p.ID == id);

        //        await context.Response.WriteAsync(myproduct.Name);
        //    }

        //    await next(context);
        //}
    }
}
