using BO;

namespace BlApi;

/// <summary>
/// Interface for product CRUD.
/// </summary>
public interface IProduct
{
    /// <summary>
    /// Get products list.
    /// </summary>
    /// <returns>Products list.</returns>
    /// <exception cref="DalException">Thrown when DB could not get the products.</exception>
    public IEnumerable<ProductForList?> GetProducts();

    /// <summary>
    /// Get products list by function.
    /// </summary>
    /// <param name="condition">Condition to be checked on each product.</param>
    /// <returns>Products list.</returns>
    /// <exception cref="DalException">Thrown when DB could not get the products.</exception>
    public IEnumerable<BO.ProductForList?> GetProductsByFunc(Func<BO.ProductForList, bool> condition);

    /// <summary>
    /// Get a product by id.
    /// </summary>
    /// <param name="id">Id of required product.</param>
    /// <returns>Product entity of the given id.</returns>
    /// <exception cref="InvalidInputException">Thrown when details are invalid.</exception>
    /// <exception cref="DalException">Thrown when DB could not get the product.</exception>
    public Product GetProduct(int id);

    /// <summary>
    /// Get a product Item on cart by id.
    /// </summary>
    /// <param name="id">Id of required product.</param>
    ///  <param name="cart">C art of user.</param>
    /// <returns>Product Item entity of the given id.</returns>
    /// <exception cref="InvalidInputException">Thrown when details are invalid.</exception>
    /// <exception cref="DalException">Thrown when DB could not get the product.</exception>
    public ProductItem GetProductItem(int id, Cart cart);

    /// <summary>
    /// Add a product to DB.
    /// </summary>
    /// <param name="product">The product to be added.</param>
    /// <exception cref="InvalidInputException">Thrown when details are invalid.</exception>
    /// <exception cref="DalException">Thrown when DB could not add the product.</exception>
    public void AddProduct(Product product);

    /// <summary>
    /// Update a product. 
    /// </summary>
    /// <param name="product">Updated product.</param>
    /// <exception cref="InvalidInputException">Thrown when details are invalid.</exception>
    /// <exception cref="DalException">Thrown when DB could not update the products.</exception>
    public void UpdateProduct(Product product);

    /// <summary>
    /// Delete a product from DB.
    /// </summary>
    /// <param name="id">Id of product to be deleted</param>
    /// <exception cref="DalException">Thrown when DB could not delete the product. </exception>
    public void DeleteProduct(int id);

    /// <summary>
    /// Get product items list.
    /// </summary>
    /// <returns>Product items list.</returns>
    /// <exception cref="DalException">Thrown when DB could not get the products.</exception>
    public IEnumerable<BO.ProductItem?> GetProductItems(BO.Cart cart);

    /// <summary>
    /// Get product items by func filter
    /// </summary>
    /// <param name="cart">Cart of customer.</param>
    /// <param name="condition">Condition to be checked.</param>
    /// <returns>Collection of product item filtered by func</returns>
    /// <exception cref="DalException">Thrown when products cant be loaded.</exception>
    public IEnumerable<BO.ProductItem?> GetProductItemsByFunc(BO.Cart cart, Func<BO.ProductItem, bool> condition);

    /// <summary>
    /// Get products grouping by category
    /// </summary>
    /// <returns>Products grouping by category</returns>
    /// <exception cref="DalException">Thrown when DB could not get the products.</exception>
    public IEnumerable<Tuple<BO.Enums.Category, IEnumerable<BO.ProductForList>>> GetProductsByCategories();

}
