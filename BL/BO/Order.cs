
namespace BO;

/// <summary>
/// Order Entity
/// </summary>
public class Order
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
    /// Status of order.
    /// </summary>
    public Enums.OrderStatus Status { get; set; }

    /// <summary>
    /// List of items on order.
    /// </summary>
    public IEnumerable<OrderItem> ItemsList { get; set; }

    /// <summary>
    /// Total price of order.
    /// </summary>
    public double TotalPrice { get; set; }
   

    /// <summary>
    /// Report an Order's description as a string. 
    /// </summary>
    /// <returns>A string representing an order.</returns>
    public override string ToString() => this.ToStringProperty();


}
