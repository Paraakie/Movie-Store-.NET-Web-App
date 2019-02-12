using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tuto4.Models
{
    public partial class Category
    {

        public int ID { get; set; }
        [Required(ErrorMessage = "The category name cannot be blank")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a category name between 3 and 50 characters in length")]
        [RegularExpression(@"^[,-\.\d a-zA-z0-9'\-'\s]*[\d]*$", ErrorMessage = "Please enter a category name beginning with a capital letter and enter only letters and spaces.")]

        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}