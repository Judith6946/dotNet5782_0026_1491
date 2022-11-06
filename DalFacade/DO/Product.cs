﻿


namespace DO;

/// <summary>
/// Structure for Product.
/// </summary>
public struct Product
{

    /// <summary>
    /// Unique ID of product.
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Name of product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Price of product.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Category of product.
    /// </summary>
    public Enums.Category Category { get; set; } 

    /// <summary>
    /// Quantity of products in stock.
    /// </summary>
    public int InStock { get; set; }

    /// <summary>
    /// Report a product's description as a string. 
    /// </summary>
    /// <returns>A string representing a product.</returns>
    public override string ToString() => $@"
        Product ID={ID}: {Name}, 
        category - {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
        ";

}
