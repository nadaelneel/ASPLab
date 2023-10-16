using Models;
using System.Runtime.CompilerServices;

namespace MVC
{
    public static class MiddleWareExtaintion
    {
        public static IApplicationBuilder HandelProductRequest(this IApplicationBuilder ab)
        {
           return ab.UseMiddleware<ProductMiddleWare>();
        }
        public static IApplicationBuilder HandelCategoryRequest(this IApplicationBuilder ab)
        {
            return ab.UseMiddleware<CategoryMiddleWare>();
        }
    }
}
