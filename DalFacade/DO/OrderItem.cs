﻿
using System.Xml.Linq;

namespace DO;

/// <summary>
/// Structure for OrderItem.
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Id of product.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Id of order.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Price of product.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Quantity of product items in the order.
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Report an OrderItem's description as a string. 
    /// </summary>
    /// <returns>A string representing an item in order.</returns>
    public override string ToString() => $@"
        Product ID={ProductId}, 
        Order ID={OrderId}
    	Price: {Price}
    	Amount: {Amount}
        ";

}
