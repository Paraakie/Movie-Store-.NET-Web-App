using System.ComponentModel.DataAnnotations;

namespace Tuto4.Models
{
    [MetadataType(typeof(CategoryMetaData))]
    public partial class Category
    {
    }


    public class CategoryMetaData
    {
        [Display(Name = "Category Name")]
        public string Name;
    }
}