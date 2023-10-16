using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public static class ProductExtaintion
    {
        public static Product AddtoModel(this AddProductViewModel addproduct)
        {
            var proAttachment = new List<ProductAttachment>();

            foreach (var item in addproduct.ImageUrl)
            {
                proAttachment.Add(new ProductAttachment()
                {
                    Image = item
                });
                
            }

            return new Product
            {
                ID = addproduct.ID,
                Name = addproduct.Name,
                Description = addproduct.Description,
                Price = addproduct.Price,
                Quantity = addproduct.Quantity,
                CategoryID = addproduct.CategoryID,
                ProductAttachments = proAttachment
            };
        }

        public static Product toModel(this ProductViewModel addproduct)
        {
            var proAttachment = new List<ProductAttachment>();

            foreach (var item in addproduct.Images)
            {
                proAttachment.Add(new ProductAttachment()
                {
                    Image = item
                });

            }

            return new Product
            {
                ID = addproduct.ID,
                Name = addproduct.Name,
                Description = addproduct.Description,
                Price = addproduct.Price,
                Quantity = addproduct.Quantity,
                CategoryID = addproduct.CategoryID,
                ProductAttachments = proAttachment
            };
        }

        public static ProductViewModel ToViewModel(this Product product)
        {
            return new ProductViewModel
            {
                ID = product.ID,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryID = product.CategoryID,
                CategoryName  = product.Category.Name,
                Images = product.ProductAttachments.Select(p => p.Image).ToList(),
            };
        }
    }
}
