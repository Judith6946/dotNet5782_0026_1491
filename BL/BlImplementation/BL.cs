

using BlApi;

namespace BlImplementation;

sealed public class BL : IBl
{
    public IProduct Product => new Product();

    public IOrder Order =>  new Order();

    public ICart Cart => new Cart();

}
