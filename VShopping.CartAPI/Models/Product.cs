namespace VShopping.CartAPI.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = String.Empty;
    public long Stock { get; set; }
    public string ImageURL { get; set; } = String.Empty;
    public string CategoryName { get; set; } = String.Empty;
}
