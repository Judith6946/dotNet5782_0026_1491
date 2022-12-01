
using AutoMapper;
using Dal;
using DalApi;
using AutoMapper;
using BO;

namespace BlImplementation;

/// <summary>
/// Implementation of product methods (BL layer)
/// </summary>
internal class Product : BlApi.IProduct
{

    private IDal Dal = new DalList();
    private static IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile(new BoProfile())).CreateMapper();



    /// <summary>
    /// Add a product to DB.
    /// </summary>
    /// <param name="product">The product to be added.</param>
    /// <exception cref="InvalidInputException">Thrown when details are invalid.</exception>
    /// <exception cref="DalException">Thrown when DB could not add the product.</exception>
    public void AddProduct(BO.Product product)
    {
        if (product.ID <= 0 || product.Name == "" || product.InStock < 0 || product.Price < 0)
            throw new InvalidInputException("");

        try
        {
            Dal.Product.Add(mapper.Map<BO.Product, DO.Product>(product));
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while adding the product", e);
        }

    }


    /// <summary>
    /// Delete a product from DB.
    /// </summary>
    /// <param name="id">Id of product to be deleted</param>
    /// <exception cref="DalException">Thrown when DB could not delete the product. </exception>
    public void DeleteProduct(int id)
    {
        try
        {
            Dal.Product.Delete(id);
        }

        catch (Exception e)
        {
            throw new DalException("Exception was thrown while deleting the product", e);
        }

    }


    /// <summary>
    /// Get a product by id.
    /// </summary>
    /// <param name="id">Id of required product.</param>
    /// <returns>Product entity of the given id.</returns>
    /// <exception cref="InvalidInputException">Thrown when details are invalid.</exception>
    /// <exception cref="DalException">Thrown when DB could not get the product.</exception>
    public BO.Product GetProduct(int id)
    {

        if (id < 0)
            throw new InvalidInputException("Id cannot be a negative number.");
        try
        {
            DO.Product p = Dal.Product.GetById(id);
            return mapper.Map<DO.Product, BO.Product>(p);
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while getting the product", e);
        }
    }

  
    /// <summary>
    /// Get a product Item on cart by id.
    /// </summary>
    /// <param name="id">Id of required product.</param>
    ///  <param name="cart">C art of user.</param>
    /// <returns>Product Item entity of the given id.</returns>
    /// <exception cref="InvalidInputException">Thrown when details are invalid.</exception>
    /// <exception cref="DalException">Thrown when DB could not get the product.</exception>
    public BO.ProductItem GetProductItem(int id, BO.Cart cart)
    {

        if (id < 0)
            throw new InvalidInputException("Id cannot be nagative.");
        try
        {
            DO.Product p = Dal.Product.GetById(id);
            BO.ProductItem productItem = mapper.Map<DO.Product, BO.ProductItem>(p);
            productItem.Amount = cart.ItemsList.First(x => x.ProductId == id).Amount;
            return productItem;

        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while getting the product",e);
        }
    }

   
    /// <summary>
    /// Get products list.
    /// </summary>
    /// <returns>Products list.</returns>
    /// <exception cref="DalException">Thrown when DB could not get the products.</exception>
    public IEnumerable<BO.ProductForList> GetProducts()
    {
        try
        {
            List<BO.ProductForList> products = new List<BO.ProductForList>();
            foreach (DO.Product p in Dal.Product.GetAll())
                products.Add(mapper.Map<DO.Product, BO.ProductForList>(p));
            return products;
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while getting the products", e);
        }
    }


    /// <summary>
    /// Update a product. 
    /// </summary>
    /// <param name="product">Updated product.</param>
    /// <exception cref="InvalidInputException">Thrown when details are invalid.</exception>
    /// <exception cref="DalException">Thrown when DB could not update the products.</exception>
    public void UpdateProduct(BO.Product product)
    {
        try
        {
            if (product.ID <= 0 || product.Name == "" || product.InStock < 0 || product.Price < 0)
                throw new InvalidInputException("Product details are invalid.");
            Dal.Product.Update(mapper.Map<BO.Product,DO.Product>(product));
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while updating the products",e);
        }
    }



}
