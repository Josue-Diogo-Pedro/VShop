using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VShop.Web.Models;

public class ProductViewModel
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public long Stock { get; set; }
    public string? CategoryName { get; set; }
    public string? ImagemURL { get; set; }
    public int CategoryId { get; set; }
}
