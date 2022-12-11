

namespace BO;

/// <summary>
/// Order Details For Lists.
/// </summary>
public class OrderForList
{
    /// <summary>
    ///  Unique ID of order.
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Name of customer.
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// status of order.
    /// </summary>
    public Enums.OrderStatus? StatusOrder { get; set; }

    /// <summary>
    /// amount of products in order.
    /// </summary>
    public int AmountOfItems { get; set; }

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
