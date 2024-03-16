namespace eShop.API.Message;

public class EmailMessage
{
    public string OrderId { get; set; }
    public string To { get; set; } 
    public string Subject { get; set; }
    public string Body { get; set; }
}