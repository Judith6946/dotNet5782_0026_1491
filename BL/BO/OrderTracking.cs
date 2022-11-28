
namespace BO;

/// <summary>
/// Order Tracking Entity - for following an order.
/// </summary>
public class OrderTracking
{

    /// <summary>
    ///  Unique ID of order.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// status of order.
    /// </summary>
    public Enums.OrderStatus StatusOrder { get; set; }


    /// <summary>
    /// List of touples, representing the order process.
    /// </summary>
    public IEnumerable<Tuple<DateTime, string>> Tracking { get; set; }


    /// <summary>
    /// Report an OrderTracking's description as a string. 
    /// </summary>
    /// <returns>A string representing an OrderTracking.</returns>
    public override string ToString() => this.ToStringProperty();

}
