
using DO;

namespace Dal;

/// <summary>
/// Access product data. 
/// </summary>
public class DataProduct
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
        for (int i = 0; i < DataSource.Config.productIndex; i ++)
        {
            if (DataSource.productsArr[i].ID == p.ID)
                throw new Exception("Product id is aready exist.");
        }

        //check if the array is not full.
        if (DataSource.Config.productIndex >= DataSource.productsArr.Length)
            throw new Exception("No place for the new product.");

        //Adding product.
        DataSource.productsArr[DataSource.Config.productIndex]=p;
        DataSource.Config.productIndex++;
        return p.ID;
    }

    /// <summary>
    /// Get a product by its id. 
    /// </summary>
    /// <param name="id">Id of product.</param>
    /// <returns>Product object.</returns>
    /// <exception cref="Exception">Thrown when the product cant be found.</exception>
    public Product Get(int id)
    {
        for (int i = 0; i < DataSource.Config.productIndex; i++)
        {
            if (DataSource.productsArr[i].ID == id)
                return DataSource.productsArr[i];
        }
        throw new Exception("Cannot find this product.");
    }

    /// <summary>
    /// Get all of products. 
    /// </summary>
    /// <returns>Products array.</returns>
    public Product[] GetAll()
    {
        int size = DataSource.Config.productIndex;
        Product[] products = new Product[size];
        Array.Copy(DataSource.productsArr, products, size);
        return products;
    }

    /// <summary>
    /// Delete a product by its id.
    /// </summary>
    /// <param name="id">Id of product to be deleted</param>
    public void Delete(int id)
    {
        
        int i = 0;
        bool found = false;

        //Search product.
        for(;i<DataSource.Config.productIndex&&!found;i++)
        {
            if (DataSource.productsArr[i].ID==id)
                found= true;
        }
        
        //Move next products.
        for(;i< DataSource.Config.productIndex &&found;i++)
        {
            DataSource.productsArr[i - 1] = DataSource.productsArr[i];
        }

        if (found)
            DataSource.Config.productIndex--;
        else
            throw new Exception("Cannot find this product.");
    }

    /// <summary>
    /// Update a product.
    /// </summary>
    /// <param name="p">Updated product.</param>
    /// <exception cref="Exception">Thrown when product cant be found.</exception>
    public void Update(Product p)
    {
        bool found = false;
        for (int i = 0; i < DataSource.Config.productIndex; i++)
        {
            if (DataSource.productsArr[i].ID == p.ID)
            {
                found = true;
                DataSource.productsArr[i] = p;
            }
        }

        if (!found)
            throw new Exception("cannot find this product");
    }

}


