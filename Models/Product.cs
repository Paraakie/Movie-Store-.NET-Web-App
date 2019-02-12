using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tuto4.Models
{
    public partial class Product
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "The Product name cannot be blank")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Please enter a product name between 3 and 50 characters in length")]
        [RegularExpression(@"^[!:;\',-\.\d a-zA-z0-9'\-'\s]*[\d]*$", ErrorMessage = "Please enter a product name made up of only letters and spaces")]
        [Display(Name = "Product Name")]

        public string Name { get; set; }

        [Required(ErrorMessage = "The Product description cannot be blank")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Please enter a product description between 10 and 200 characters in length")]
        [RegularExpression(@"^[!,:;'-\.\d a-zA-z0-9'\-'\s]*[\d]*$", ErrorMessage = "Please enter a product description made up of only letters and spaces")]

        public string Description { get; set; }

        [Required(ErrorMessage = "The Product price cannot be blank")]
        [Range(0.10, 10000, ErrorMessage = "Please enter a product price between 0.10 and 10000")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }
        
        //[RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "The price must be a number up to two decimal places")]

        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<ProductImageMapping> ProductImageMappings { get; set; }
    }
}