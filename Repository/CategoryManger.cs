using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryManger : Manger<Category>
    {
        public CategoryManger(MyDBContext db) : base(db)
        {
        }
    }
}
