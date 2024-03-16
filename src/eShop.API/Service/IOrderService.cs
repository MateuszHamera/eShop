using eShop.API.Entity;

namespace eShop.API.Service;

public interface IOrderService
{
    void PublishOrderConfirmationEmail(Order order);
}