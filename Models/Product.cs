using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace crud2.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageURL { get; set; }

        //Navigational Propery
       public int CategoryId { get; set; }
        [ForeignKey (nameof(CategoryId))]
        public Category Category { get; set; }
        [NotMapped]
        public IFormFile ProductPicture {get; set;}
    }
}
