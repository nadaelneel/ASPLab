using Abp.Linq.Expressions;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ViewModel;

namespace Repository
{
    public class ProductManger : Manger<Product>
    {
        public ProductManger(MyDBContext db) : base(db)
        {
        }

        public List<ProductViewModel> Get()
        {
            return GetAll().Select(p=>p.ToViewModel()).ToList();
        }

        public ProductViewModel GetProductById(int id)
        {
            return Get().Where(p => p.ID == id).FirstOrDefault();
        }

        public void Add(AddProductViewModel addProduct)
        {
            var pro = addProduct.AddtoModel();

            base.Add(pro);
        }
         public void Delete(int id)
        {
            Product pro = GetAll().Where(i=>i.ID == id).FirstOrDefault();

            base.Delete(pro);
            
        }
        public IQueryable<Product> Search(int ID,
            string Name,
            double Price,
            string CategoryName,
            string orderBy ="ID",
            bool isAssending = true,
            int PageSize=3 ,
            int Pageindex =1)
        {
            var Filter = PredicateBuilder.New<Product>();
            var oldfilter = Filter;

            if(!string.IsNullOrEmpty(Name))
            {
                Filter = Filter.And(i => i.Name.ToLower().Contains(Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(CategoryName))
            {
                Filter = Filter.And(i => i.Category.Name.ToLower().Contains(CategoryName.ToLower()));
            }
            if (Price != 0)
            {
                Filter = Filter.And(i => i.Price <= Price);
            }
            if(oldfilter == Filter)
            {
                Filter = null;
            }

            return base.search(Filter,orderBy,isAssending,PageSize,Pageindex);
        }

        public void Edit(AddProductViewModel newPrd)
        {
            var oldprod = GetAll().Where(i => i.ID == newPrd.ID).FirstOrDefault();
            oldprod.Name = newPrd.Name;
            oldprod.Price = newPrd.Price;
            oldprod.Quantity = newPrd.Quantity;
            oldprod.CategoryID = newPrd.CategoryID;
            oldprod.Description = newPrd.Description;
           
            oldprod.ProductAttachments = new List<ProductAttachment>();
            foreach (var item in newPrd.ImageUrl)
            {
                oldprod.ProductAttachments.Add(new ProductAttachment()
                {
                    Image = item
                });
            }

            Update(oldprod);
        }

    }
}
