
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
    public int Add(Product p)
    {
        Random rn = new Random();
        p.ID = rn.Next(100000, 999999);  
        DataSource.addProduct(p);
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
        //how?????????
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


//איפה נשמרת הכמות שיש במערכים
//איך מטפלים בחורים, מחיקה, הוספה וכו
//איפה מקדמים את האינדקסים
//למה המזהה האוטומוטי מאותחל??

//זה בסדר שהוספנו מתודות הוספה??
//מתודת הבדיקה, בסדר