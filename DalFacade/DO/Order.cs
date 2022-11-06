

using System.Diagnostics;

namespace DO;
/// <summary>
/// Structure for Order
/// </summary>
public struct Order
{
    /// <summary>
    /// Unique ID of order.
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Name of Customer.
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// Email of customer.
    /// </summary>
    public string CustomerEmail { get; set; }

    /// <summary>
    /// Adress of customer.
    /// </summary>
    public string CustomerAdress { get; set; }

    /// <summary>
    /// Date of order.
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// Ship date of order.
    /// </summary>
    public DateTime ShipDate { get; set; }

    /// <summary>
    /// Delivery date of order.
    /// </summary>
    public DateTime DeliveryDate { get; set; }

    /// <summary>
    /// Report an Order's description as a string. 
    /// </summary>
    /// <returns>A string representing an order.</returns>
    public override string ToString() => $@"
        Product ID={ID}, 
        Order Name={CustomerName}
    	Order Email={CustomerEmail}
        Order Adress={CustomerAdress}
        Order OrderDate={OrderDate}
        Order ShipDate={ShipDate}
        Order DeliveryDate={DeliveryDate}
        ";
}
