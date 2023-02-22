

using DalApi;
using System.Diagnostics;

namespace Dal;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();

    private DalXml() { }

    public IProduct Product => new Product();

    public IOrder Order => new Order();

    public IOrderItem OrderItem => new OrderItem();

}
