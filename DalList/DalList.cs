using DalApi;
namespace Dal;

sealed internal class DalList:IDal
{
    public static IDal Instance { get; } = new DalList();

    private DalList() { }
    public IProduct Product => new DalProduct();

    public IOrder Order => new DalOrder();

    public IOrderItem OrderItem => new DalOrderItem();

}
