
using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    public OrderItem GetByOrderAndProduct(int orderId, int productId);

    public IEnumerable<OrderItem?> GetByOrder(int orderId);
}

