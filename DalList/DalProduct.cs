using DalApi;
using DO;
using System.Linq;

namespace Dal;

/// <summary>
/// Access product data. 
/// </summary>
internal class DalProduct : IProduct
{

    /// <summary>
    /// Add product. 
    /// </summary>
    /// <param name="p">Product to be added</param>
    /// <returns>Id of the new product.</returns>
    /// <exception cref="Exception">Thrown when product is already exist or when the array is full</exception>
    public int Add(Product p)
    {
        //Check whether the id does not already exist.
        if (getByCondition(x => x?.ID == p.ID) != null)
        {
            throw new AlreadyExistException("Product id is aready exist.");
        }


        //Adding product.
        DataSource.productsList.Add(p);
        return p.ID;
    }


    /// <summary>
    /// Get a product by its id. 
    /// </summary>
    /// <param name="id">Id of product.</param>
    /// <returns>Product object.</returns>
    /// <exception cref="Exception">Thrown when the product cant be found.</exception>
    public Product GetById(int id)
    {
        return getByCondition(x => x?.ID == id) ?? throw new NotFoundException("Cannot find this product.");
    }


    /// <summary>
    /// Get a product by condition.
    /// </summary>
    /// <param name="predicate">Condition function.</param>
    /// <returns></returns>
    /// <exception cref="InvalidInputException">Thrown when condition is null</exception>
    public Product? getByCondition(Func<Product?, bool>? predicate)
    {
        return DataSource.productsList.FirstOrDefault(predicate ??
            throw new InvalidInputException("condition cannot be null"), null)
    }


    /// <summary>
    /// Get all of products. 
    /// </summary>
    /// <returns>Products array.</returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? predicate = null)
    {
        List<Product?> products = new List<Product?>(DataSource.productsList);
        if (predicate == null)
            return products;
        return products.Where(predicate);
    }


    /// <summary>
    /// Delete a product by its id.
    /// </summary>
    /// <param name="id">Id of product to be deleted</param>
    public void Delete(int id)
    {
        _ = getByCondition(x => x?.ID == id) ?? throw new NotFoundException("Product is not exist.");
        DataSource.productsList.RemoveAll(x => x?.ID == id);
    }


    /// <summary>
    /// Update a product.
    /// </summary>
    /// <param name="p">Updated product.</param>
    /// <exception cref="Exception">Thrown when product cant be found.</exception>
    public void Update(Product p)
    {
        int index = DataSource.productsList.FindIndex(x => x?.ID == p.ID);
        if (index == -1)
        {
            throw new NotFoundException("Product is not exist.");
        }
        DataSource.productsList[index] = p;
    }


}


