
namespace BO;

public class OrderItem
{
    /// <summary>
    ///  Unique ID of order-item.
    /// </summary>
    public int ID { get; set; }


    /// <summary>
    /// Id of product.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Name of product.
    /// </summary>
    public string ProductName { get; set; }
   

    /// <summary>
    /// Price of product.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Quantity of product items in the order.
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Total price.
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// Report an OrderItem's description as a string. 
    /// </summary>
    /// <returns>A string representing an item in order.</returns>
    public override string ToString() => $@"
        Order item ID: {ID}
        Product ID={ProductId}, 
    	Price: {Price}
    	Amount: {Amount}
        ";
}
