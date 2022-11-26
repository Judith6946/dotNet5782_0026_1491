
using BO;

namespace BlApi;

public interface IProduct
{
    public IEnumerable<ProductForList> GetProducts();

    public Product GetProduct(int id);

    public ProductItem GetProductItem(int id, Cart cart);

    public void AddProduct(Product product);

    public void UpdateProduct(Product product);

    public void DeleteProduct(int id);


}
