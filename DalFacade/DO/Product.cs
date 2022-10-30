

namespace DO;

/// <summary>
/// Structure for Product
/// </summary>
public struct Product
{

    /// <summary>
    /// Unique ID of product
    /// </summary>
    public int ID { get; set; }
    public string Name { get; set; }

    public double Price { get; set; }
    public string Category { get; set; } //check!!!!!!!!!

    public int InStock { get; set; }

    public override string ToString() => $@"
        Product ID={ID}: {Name}, 
        category - {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
        ";

}
