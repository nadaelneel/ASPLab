using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ViewModel
{
    public class AddProductViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Please , Enter Product Name")]
        [StringLength(50 , ErrorMessage ="Must be More than 3 letters and less than 50" , MinimumLength =3)]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryID { get; set; }

        public  List<String> ImageUrl { get; set; } = new List<string>();

      
        public IFormFileCollection  Images { get; set; }
    }
}
