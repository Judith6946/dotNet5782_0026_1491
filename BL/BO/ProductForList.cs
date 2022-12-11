

namespace BO;

/// <summary>
/// Product Details For Lists.
/// </summary>
public class ProductForList
{

    /// <summary>
    ///  Unique ID of product.
    /// </summary>
    public int ID { get; set; }


    /// <summary>
    ///  Name of product-for-list.
    /// </summary>
    public string? Name { get; set; }


    /// <summary>
    ///  Price of product-for-list.
    /// </summary>
    public double Price { get; set; }


    /// <summary>
    ///  Category of product-for-list.
    /// </summary>
    public Enums.Category? Category { get; set; }


    /// <summary>
    /// Report a product's description as a string. 
    /// </summary>
    /// <returns>A string representing a product.</returns>
    public override string ToString() => this.ToStringProperty();
}
