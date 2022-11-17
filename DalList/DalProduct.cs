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
            throw new Exception("Product id is aready exist.");
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
        Product p = DataSource.productsList.FirstOrDefault(x => x.ID == id, new Product { ID = 0 });
        if (p.ID == 0)
            throw new Exception("Cannot find this product.");
        return p;
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
        DataSource.productsList.RemoveAll(x => x.ID == id);
    }


    /// <summary>
    /// Update a product.
    /// </summary>
    /// <param name="p">Updated product.</param>
    /// <exception cref="Exception">Thrown when product cant be found.</exception>
    public void Update(Product p)
    {
        if (!DataSource.productsList.Any(x => x.ID == p.ID))
        {
            throw new Exception("Productis not exist.");
        }
        //update- remove and add...
        DataSource.productsList.RemoveAll(x => x.ID == p.ID);
        DataSource.productsList.Add(p);
    }


}


