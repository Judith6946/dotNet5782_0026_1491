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
        int index = DataSource.orderItemsList.ToList().FindIndex(x => x.ID == id);
        if (index == -1)
            throw new NotFoundException("Cannot find this item.");
        return DataSource.orderItemsList.ToList()[index];
    }



    /// <summary>
    /// Get all of products. 
    /// </summary>
    /// <returns>Products array.</returns>
    public IEnumerable<OrderItem> GetAll()
    {
        return new List<OrderItem>(DataSource.orderItemsList);
    }



    /// <summary>
    /// Delete a OrderItem by its id.
    /// </summary>
    /// <param name="id">Id of OrderItem to be deleted</param>
    public void Delete(int id)
    {
        if (!DataSource.orderItemsList.Any(x => x.ID == id))
        {
            throw new NotFoundException("Item is not exist.");
        }
        DataSource.orderItemsList.RemoveAll(x => x.ID == id);
    }



    /// <summary>
    /// Update a orderItem.
    /// </summary>
    /// <param name="p">Updated orderItem.</param>
    /// <exception cref="Exception">Thrown when orderItem cant be found.</exception>
    public void Update(OrderItem o)
    {
        int index = DataSource.orderItemsList.FindIndex(x => x.ID == o.ID);
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
        int index = DataSource.orderItemsList.ToList().FindIndex(x => x.OrderId == orderId && x.ProductId == productId);
        if (index == -1)
            throw new NotFoundException("Cannot find this item.");
        return DataSource.orderItemsList.ToList()[index];
    }



    /// <summary>
    /// Get all items on a given order. 
    /// </summary>
    /// <param name="orderId">The order id.</param>
    /// <returns>All order items of the given order.</returns>
    /// /// <exception cref="Exception">Thrown when the order has no items.</exception>
    public IEnumerable<OrderItem> GetByOrder(int orderId)
    {
        return DataSource.orderItemsList.Where(x => x.OrderId == orderId).ToList<OrderItem>();
    }


}
