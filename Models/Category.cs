
using System.ComponentModel.DataAnnotations;

namespace crud2.Models
{
    public class Category
    {
        public Category() 
        {
            Products = new HashSet<Product>();
        }    
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
