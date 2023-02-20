using BO;

namespace BlApi;

/// <summary>
/// Interface of order methods
/// </summary>
public interface IOrder
{
    /// <summary>
    /// Get all orders
    /// </summary>
    /// <returns>Collection of all orders.</returns>
    /// <exception cref="DalException">Thrown when there was an error contacting the database.</exception>
    public IEnumerable<OrderForList?> GetOrders();

    /// <summary>
    /// Get an order object
    /// </summary>
    /// <param name="id">Id of required order.</param>
    /// <returns>Required order.</returns>
    /// <exception cref="InvalidInputException">Thrown when id is not valid.</exception>
    /// <exception cref="DalException">Thrown when there was an error contacting the database.</exception>
    public Order GetOrder(int id);

    /// <summary>
    /// Update ship date of order.
    /// </summary>
    /// <param name="id">Id of order to be updated.</param>
    /// <returns></returns>
    /// <exception cref="InvalidInputException">Thrown when id is not valid.</exception>
    /// <exception cref="AlreadyDoneException">Thrown when order was already shipped.</exception>
    /// <exception cref="DalException">Thrown when there was an error contacting the database.</exception>
    public Order UpdateOrderShipping(int id);

    /// <summary>
    /// Update delivery date of order.
    /// </summary>
    /// <param name="id">Id of order to be updated.</param>
    /// <returns></returns>
    /// <exception cref="InvalidInputException">Thrown when id is not valid.</exception>
    /// <exception cref="AlreadyDoneException">Thrown when order was already delivered.</exception>
    /// <exception cref="DalException">Thrown when there was an error contacting the database.</exception>
    public Order UpdateOrderDelivery(int id);

    /// <summary>
    /// Follow an order - get an orderTracking object that describes an order.
    /// </summary>
    /// <param name="id">Id of order to be followed.</param>
    /// <returns>An orderTracking object that describes an order.</returns>
    /// <exception cref="InvalidInputException">Thrown when id is not valid.</exception>
    /// <exception cref="DalException">Thrown when there was an error contacting the database.</exception>
    public OrderTracking FollowOrder(int id);

    /// <summary>
    /// Update an amount of item on order.
    /// </summary>
    /// <param name="id">Id of order</param>
    /// <param name="productId">Id of product</param>
    /// <param name="amount">Updated amount</param>
    /// <returns>Updated order</returns>
    /// <exception cref="InvalidInputException">Thrown when input is invalid</exception>
    /// <exception cref="ImpossibleException">Thrown when order was already shipped</exception>
    /// <exception cref="NotFoundException">Thrown when no such product was found on this order.</exception>
    /// <exception cref="DalException">Thrown when there was an error contacting the database.</exception>
    public Order UpdateOrder(int id,int productId,int amount);
}
