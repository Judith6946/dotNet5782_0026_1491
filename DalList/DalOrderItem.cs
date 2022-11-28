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
        //Check whether the id does not already exist.

        if (DataSource.orderItemsList.Any(x => x.ID == o.ID))
        {
            throw new AlreadyExistException("order item id is already exist.");
        }


        //Adding item.
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
        OrderItem item = DataSource.orderItemsList.FirstOrDefault(x => x.ID == id, new OrderItem { ID = 0 });
        if (item.ID == 0)
            throw new NotFoundException("Cannot find this item.");
        return item;
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
        DataSource.orderItemsList.Insert(index, o);
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
        OrderItem item = DataSource.orderItemsList.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId, new OrderItem { ID = 0 });
        if (item.ID == 0)
            throw new NotFoundException("Cannot find this item.");
        return item;

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
