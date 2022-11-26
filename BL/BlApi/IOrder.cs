
using BO;

namespace BlApi;

public interface IOrder
{
    public IEnumerable<OrderForList> GetOrders();

    public Order GetOrder(int id);

    public Order UpdateOrderShipping(int id);

    public Order UpdateOrderDelivery(int id);

    public OrderTracking FollowOrder(int id);

    //add bunos!!!!!!!!!!
}
