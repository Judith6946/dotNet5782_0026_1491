

using BO;

namespace BlApi;

/// <summary>
/// Interface of cart methods.
/// </summary>
public interface ICart
{
    public Cart AddItem(Cart cart, int id);

    public Cart UpdateAmount(Cart cart, int amount,int id);

    public void MakeOrder(Cart cart);
}
