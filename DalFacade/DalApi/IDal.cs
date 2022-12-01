
using DO;
namespace DalApi;

/// <summary>
/// Interface of Dal 
/// </summary>
public interface IDal
{
    IProduct Product { get; }
    IOrder Order { get; }
    IOrderItem OrderItem { get; }

}
