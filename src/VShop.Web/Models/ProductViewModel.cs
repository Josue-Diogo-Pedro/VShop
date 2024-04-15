using System.ComponentModel.DataAnnotations;

namespace VShop.Web.Models;

public class ProductViewModel
{
    public int ProductId { get; set; }

    [Required]
    public string? Name { get; set; }

    [Range(1, 9999)]
    [Required]
    public decimal Price { get; set; }

    [Required]
    public string? Description { get; set; }

    [Range(1, 9999)]
    [Required]
    public long Stock { get; set; }

    [Display(Name = "Category Name")]
    public string? CategoryName { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; } = 1;

    [Required]
    [Display(Name = "Image URL")]
    public string? ImagemURL { get; set; }

    [Display(Name = "Category")]
    public int CategoryId { get; set; }
}
