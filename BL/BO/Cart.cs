

namespace BO;

/// <summary>
/// Customer Cart Entity.
/// </summary>
public class Cart
{
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
    /// List of items on order.
    /// </summary>
    public IEnumerable<OrderItem> ItemsList { get; set; }

    /// <summary>
    /// Total price of order.
    /// </summary>
    public double TotalPrice { get; set; }


    /// <summary>
    /// Report a Cart's description as a string. 
    /// </summary>
    /// <returns>A string representing a cart.</returns>
    public override string ToString() => this.ToStringProperty();

}
