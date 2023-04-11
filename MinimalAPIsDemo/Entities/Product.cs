namespace MinimalAPIsDemo.Entities;

public class Product
{
    public int ProductId { get; set; }
    
    public string ProductName { get; set; } = default!;
    
    public string ProductDescription { get; set; } = default!;
    
    public int ProductPrice { get; set; }
    
    public int ProductStock { get; set; }
}