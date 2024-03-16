namespace eShop.API.Entity;

public class Order
{
    public string Id { get; set; } 
    public string Email { get; set; } 
    public decimal TotalAmount { get; set; }
    public List<OrderItem> Items { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
}