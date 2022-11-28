
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
    /// עוד לא צריך להתיחס לזה, אבל אח"כ אמור להיות שם תאריך וסטטוס הזמנה.
    /// </summary>
    public IEnumerable<Tuple<DateTime, string>> Tracking { get; set; }



}
