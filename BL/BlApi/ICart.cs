

using BO;

namespace BlApi;

public interface ICart
{
    public Cart AddItem(Cart cart, int id);

    public Cart UpdateAmount(Cart cart, int amount,int id);

    public void MakeOrder(Cart cart);
}
