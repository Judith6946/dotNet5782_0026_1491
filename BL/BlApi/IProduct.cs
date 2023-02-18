using BO;

namespace BlApi;

/// <summary>
/// Interface for product CRUD.
/// </summary>
public interface IProduct
{
   
    public IEnumerable<ProductForList?> GetProducts();
    public IEnumerable<BO.ProductForList?> GetProductsByFunc(Func<BO.ProductForList, bool> condition);

    public Product GetProduct(int id);

    public ProductItem GetProductItem(int id, Cart cart);

    public void AddProduct(Product product);

    public void UpdateProduct(Product product);

    public void DeleteProduct(int id);

    public IEnumerable<BO.ProductItem?> GetProductItems(BO.Cart cart);

    public IEnumerable<BO.ProductItem?> GetProductItemsByFunc(BO.Cart cart, Func<BO.ProductItem, bool> condition);

    /// <summary>
    /// Get products grouping by category
    /// </summary>
    /// <returns>Products grouping by category</returns>
    /// <exception cref="DalException">Thrown when DB could not get the products.</exception>
    public IEnumerable<Tuple<BO.Enums.Category, IEnumerable<BO.ProductForList>>> GetProductsByCategories();

}
