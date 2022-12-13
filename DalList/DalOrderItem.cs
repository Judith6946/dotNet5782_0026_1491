using DO;
using DalApi;
namespace Dal;

/// <summary>
/// Access order item data. 
/// </summary>
public class DalOrderItem : IOrderItem
{


    /// <summary>
    /// Add item to order. 
    /// </summary>
    /// <param name="o">item to be added to the order</param>
    /// <returns>Id of the new product.</returns>
    public int Add(OrderItem o)
    {
        //Adding item.
        o.ID = DataSource.Config.OrderItemLastId;
        DataSource.orderItemsList.Add(o);
        return o.ID;
    }



    /// <summary>
    /// Get a item by its id. 
    /// </summary>
    /// <param name="id">Id of item .</param>
    /// <returns>OrderItem object.</returns>
    /// <exception cref="Exception">Thrown when the OrderItem cant be found.</exception>
    public OrderItem GetById(int id)
    {
        return getByCondition(x => x?.ID == id) ?? throw new NotFoundException("Cannot find this order item.");
    }



    /// <summary>
    /// Get all of products. 
    /// </summary>
    /// <returns>Products array.</returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? predicate = null)
    {
        List<OrderItem?> items = new List<OrderItem?>(DataSource.orderItemsList);
        if (predicate == null)
            return items;
        return items.Where(predicate);
    }

    /// <summary>
    /// Get an order item by condition.
    /// </summary>
    /// <param name="predicate">Condition function.</param>
    /// <returns></returns>
    /// <exception cref="InvalidInputException">Thrown when condition is null</exception>
    /// <exception cref="NotFoundException">Thrown when order item cant be found.</exception>
    public OrderItem? getByCondition(Func<OrderItem?, bool>? predicate)
    {
        return DataSource.orderItemsList.FirstOrDefault(predicate ??
            throw new InvalidInputException("condition cannot be null"), null) ??
            throw new NotFoundException("cannot find this product.");
    }



    /// <summary>
    /// Delete a OrderItem by its id.
    /// </summary>
    /// <param name="id">Id of OrderItem to be deleted</param>
    public void Delete(int id)
    {
        _ = getByCondition(x => x?.ID == id) ?? throw new NotFoundException("Order item is not exist.");
        DataSource.orderItemsList.RemoveAll(x => x?.ID == id);
    }



    /// <summary>
    /// Update a orderItem.
    /// </summary>
    /// <param name="p">Updated orderItem.</param>
    /// <exception cref="Exception">Thrown when orderItem cant be found.</exception>
    public void Update(OrderItem o)
    {
        int index = DataSource.orderItemsList.FindIndex(x => x?.ID == o.ID);
        if (index == -1)
        {
            throw new NotFoundException("Item is not exist.");
        }
        DataSource.orderItemsList[index]= o;
    }



    /// <summary>
    /// Search an order item by order id and product id.
    /// </summary>
    /// <param name="orderId">Id of order</param>
    /// <param name="productId">Id of product</param>
    /// <returns>The found order item object.</returns>
    /// <exception cref="Exception">Thrown when no such item was found.</exception>
    public OrderItem GetByOrderAndProduct(int orderId, int productId)
    {
        return getByCondition(x => x?.OrderId == orderId && x?.ProductId == productId)?? 
            throw new NotFoundException("Cannot find this item.");
    }



    /// <summary>
    /// Get all items on a given order. 
    /// </summary>
    /// <param name="orderId">The order id.</param>
    /// <returns>All order items of the given order.</returns>
    /// /// <exception cref="Exception">Thrown when the order has no items.</exception>
    public IEnumerable<OrderItem?> GetByOrder(int orderId)
    {
        return GetAll(x => x?.OrderId == orderId);
    }


}
