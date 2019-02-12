using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tuto4.ViewModels
{
    public class ProductViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "The Product name cannot be blank")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a product name between 3 and 50 characters in length")]
        [RegularExpression(@"^[a-zA-z0-9'-'\s]*$", ErrorMessage = "Please enter a product name made up of only letters and spaces")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Product description cannot be blank")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Please enter a product description between 10 and 200 characters in length")]
        [RegularExpression(@"^[a-zA-Z0-9'-'\s]*$", ErrorMessage = "Please enter a product description made up of only letters and spaces")]
        public string Description { get; set; }

        public decimal Price { get; set; }
        [Required(ErrorMessage = "The Product price cannot be blank")]
        [Range(0.10, 10000, ErrorMessage = "Please enter a product price between 0.10 and 10000")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [RegularExpression("[0-9]+(\\.[0-9)][0-9]?)?", ErrorMessage = "The Price must be a number upto two decimal places")]

        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public SelectList CategoryList { get; set; }
        public List<SelectList> ImageLists { get; set; }
        public string[] ProductImages { get; set; }



    }
}