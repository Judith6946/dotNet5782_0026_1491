
using BO;

namespace BlApi;

/// <summary>
/// Interface of order methods
/// </summary>
public interface IOrder
{
    public IEnumerable<OrderForList> GetOrders();

    public Order GetOrder(int id);

    public Order UpdateOrderShipping(int id);

    public Order UpdateOrderDelivery(int id);

    public OrderTracking FollowOrder(int id);

    public Order UpdateOrder(int id,int productId,int amount);
}
