using DalApi;
using DO;

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

        if (DataSource.productsList.Any(x => x.ID == p.ID))
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
        int index = DataSource.productsList.ToList().FindIndex(x => x.ID == id);
        if (index == -1)
            throw new NotFoundException("Cannot find this product.");
        return DataSource.productsList.ToList()[index];
    }


    /// <summary>
    /// Get all of products. 
    /// </summary>
    /// <returns>Products array.</returns>
    public IEnumerable<Product> GetAll()
    {
        return new List<Product>(DataSource.productsList);
    }


    /// <summary>
    /// Delete a product by its id.
    /// </summary>
    /// <param name="id">Id of product to be deleted</param>
    public void Delete(int id)
    {
        if (!DataSource.productsList.Any(x => x.ID == id))
        {
            throw new NotFoundException("Productis not exist.");
        }
        DataSource.productsList.RemoveAll(x => x.ID == id);
    }


    /// <summary>
    /// Update a product.
    /// </summary>
    /// <param name="p">Updated product.</param>
    /// <exception cref="Exception">Thrown when product cant be found.</exception>
    public void Update(Product p)
    {
        int index = DataSource.productsList.FindIndex(x => x.ID == p.ID);
        if (index == -1)
        {
            throw new NotFoundException("Productis not exist.");
        }
        DataSource.productsList[index] = p;
    }


}


