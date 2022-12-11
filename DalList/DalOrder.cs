using DO;
using DalApi;
namespace Dal;


/// <summary>
/// Access order data. 
/// </summary>
public class DalOrder : IOrder
{

    /// <summary>
    /// Add an order to the order list.
    /// </summary>
    /// <param name="o">Order object to be added.</param>
    /// <returns>New order id.</returns>
    public int Add(Order o)
    {
        //Adding order.
        o.ID = DataSource.Config.OrderLastId;
        DataSource.ordersList.Add(o);
        return o.ID;
    }


    /// <summary>
    /// Get a order by its id. 
    /// </summary>
    /// <param name="id">Id of order.</param>
    /// <returns>Order object.</returns>
    /// <exception cref="Exception">Thrown when the order cant be found.</exception>
    public Order GetById(int id)
    {
        return getByCondition(x => x?.ID == id) ?? throw new NotFoundException("Cannot find this order.");
    }


    /// <summary>
    /// Get an order by condition.
    /// </summary>
    /// <param name="predicate">Condition function.</param>
    /// <returns></returns>
    /// <exception cref="InvalidInputException">Thrown when condition is null</exception>
    /// <exception cref="NotFoundException">Thrown when order cant be found.</exception>
    public Order? getByCondition(Func<Order?, bool>? predicate)
    {
        return DataSource.ordersList.FirstOrDefault(predicate ??
            throw new InvalidInputException("condition cannot be null"), null) ??
            throw new NotFoundException("cannot find this product.");
    }


    /// <summary>
    /// Get all of orders. 
    /// </summary>
    /// <returns>Orders array.</returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? predicate = null)
    {
        List<Order?> orders = new List<Order?>(DataSource.ordersList);
        if (predicate == null)
            return orders;
        return orders.Where(predicate);
    }


    /// <summary>
    /// Delete a order by its id.
    /// </summary>
    /// <param name="id">Id of order to be deleted</param>
    public void Delete(int id)
    {
        _ = getByCondition(x => x?.ID == id) ?? throw new NotFoundException("Order is not exist.");
        DataSource.ordersList.RemoveAll(x => x?.ID == id);
    }


    /// <summary>
    /// Update a order.
    /// </summary>
    /// <param name="o">Updated order.</param>
    /// <exception cref="Exception">Thrown when order cant be found.</exception>
    public void Update(Order o)
    {
        int index = DataSource.ordersList.FindIndex(x => x?.ID == o.ID);
        if (index == -1)
        {
            throw new NotFoundException("order is not exist.");
        }
        DataSource.ordersList[index] = o;
    }


}
