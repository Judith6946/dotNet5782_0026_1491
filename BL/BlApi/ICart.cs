using BO;

namespace BlApi;

/// <summary>
/// Interface of cart methods.
/// </summary>
public interface ICart
{
    /// <summary>
    /// Add an item to customer cart.
    /// </summary>
    /// <param name="cart">Customer cart.</param>
    /// <param name="id">Id of product to be added.</param>
    /// <returns>Updated cart.</returns>
    /// <exception cref="SoldOutException">Thrown when the required product is sold out.</exception>
    /// <exception cref="InvalidInputException">Thrown when Id was invalid.</exception>
    /// <exception cref="DalException">Thrown when error was occured while reaching the DB.</exception>
    public Cart AddItem(Cart cart, int id);

    /// <summary>
    /// Remove an item from cart.
    /// </summary>
    /// <param name="cart">Cart of customer.</param>
    /// <param name="id">Id of product to be removed.</param>
    /// <returns>Updated cart.</returns>
    /// <exception cref="NotFoundException">Thrown when product cant be found on this cart.</exception>
    /// <exception cref="InvalidInputException">Thrown when id was invalid.</exception>
    public Cart RemoveItem(Cart cart, int id);

    /// <summary>
    /// Update amount of product on customer cart.
    /// </summary>
    /// <param name="cart">Customer cart.</param>
    /// <param name="amount">Updated amount.</param>
    /// <param name="id">Id of product to be updated.</param>
    /// <returns>Updated cart.</returns>
    /// <exception cref="InvalidInputException">Thrown when amount or id is invalid</exception>
    /// <exception cref="NotFoundException">Thrown when product is not found on cart.</exception>
    /// <exception cref="SoldOutException">Thrown when product is sold out.</exception>
    public Cart UpdateAmount(Cart cart, int amount,int id);

    /// <summary>
    /// Make an order from customer cart.
    /// </summary>
    /// <param name="cart">Customer cart.</param>
    /// <exception cref="SoldOutException">Thrown when one of the products was sold out.</exception>
    /// <exception cref="InvalidInputException">Thrown when order details were invalid</exception>
    /// <exception cref="DalException">Thrown when error was occured while reaching the DB.</exception>
    public void MakeOrder(Cart cart);
}
