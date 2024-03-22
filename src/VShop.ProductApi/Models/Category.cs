namespace VShop.ProductApi.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }

    //EF relations
    public ICollection<Product> Products { get; set; }
}
