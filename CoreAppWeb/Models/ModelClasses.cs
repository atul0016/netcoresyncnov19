using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CoreAppWeb.Models
{
    public class Category
    {
        [Key]
        public int CategoryRowId { get; set; }
        [Required(ErrorMessage ="Category Id is Must")]
        public string CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name is Must")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Base Price is Must")]
        public int BasePrice { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }

    public class Product
    {
        [Key]
        public int ProductRowId { get; set; }
        [Required(ErrorMessage = "Product Id is Must")]
        public string ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is Must")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Manufacturer is Must")]
        public string Manufacturer { get; set; }
        [Required(ErrorMessage = "Price is Must")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Category Id is Must")]
        public int CategoryRowId { get; set; }
        public Category Category { get; set; }
    }
}
