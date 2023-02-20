
using AutoMapper;
using DalApi;
using BO;
using System.Security.Cryptography;

namespace BlImplementation;

/// <summary>
/// Implementation of product methods (BL layer)
/// </summary>
internal class Product : BlApi.IProduct
{

    private static DalApi.IDal Dal = DalApi.Factory.Get();
    private static IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile(new BoProfile())).CreateMapper();



    /// <summary>
    /// Add a product to DB.
    /// </summary>
    /// <param name="product">The product to be added.</param>
    /// <exception cref="InvalidInputException">Thrown when details are invalid.</exception>
    /// <exception cref="DalException">Thrown when DB could not add the product.</exception>
    public void AddProduct(BO.Product product)
    {
        if (product.ID <= 0 || !Validation.IsName(product.Name) || product.InStock < 0 || product.Price < 0)
            throw new InvalidInputException("product details are not valid. id, instock and price must be positive, name should be at least 3 chars long");

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
    /// Get products grouping by category
    /// </summary>
    /// <returns>Products grouping by category</returns>
    /// <exception cref="DalException">Thrown when DB could not get the products.</exception>
    public IEnumerable<Tuple<BO.Enums.Category, IEnumerable<BO.ProductForList>>> GetProductsByCategories()
    {
        try
        {
            return (from product in Dal.Product.GetAll()
                    let product2 = mapper.Map<DO.Product, BO.ProductForList>((DO.Product)product)
                    group product2 by product2?.Category into p
                    select new { category = p.Key, products = p })
                   .Select(x => new Tuple<BO.Enums.Category, IEnumerable<BO.ProductForList>>((Enums.Category)x.category!, x.products));
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while getting the products", e);
        }
    }


    /// <summary>
    /// Delete a product from DB.
    /// </summary>
    /// <param name="id">Id of product to be deleted</param>
    /// <exception cref="DalException">Thrown when DB could not delete the product. </exception>
    public void DeleteProduct(int id)
    {
        if (id <= 0)
            throw new InvalidInputException("Id was negative.");
        if (isOrdered(id))
            throw new ImpossibleException("Cannot delete a product that already has been ordered.");
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
        _ = cart ?? throw new InvalidInputException("cart was null");
        _ = cart?.ItemsList ?? throw new InvalidInputException("no products in your cart.");

        BO.OrderItem item = cart?.ItemsList?.FirstOrDefault(x => x?.ProductId == id, null) ??
                 throw new InvalidInputException("No such product in your cart.");

        try
        {
            DO.Product p = Dal.Product.GetById(id);
            BO.ProductItem productItem = mapper.Map<DO.Product, BO.ProductItem>(p);
            productItem.Amount = item.Amount;
            return productItem;
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while getting the product", e);
        }
    }


    /// <summary>
    /// Get product items list.
    /// </summary>
    /// <returns>Product items list.</returns>
    /// <exception cref="DalException">Thrown when DB could not get the products.</exception>
    public IEnumerable<BO.ProductItem?> GetProductItems(BO.Cart cart)
    {
        try
        {
            return Dal.Product.GetAll().Select(x =>
            {
                BO.OrderItem? item = cart?.ItemsList?.FirstOrDefault(y => y?.ProductId == ((DO.Product)x!).ID, null);
                var productItem = mapper.Map<DO.Product, BO.ProductItem>((DO.Product)x!);
                productItem.Amount = item?.Amount ?? 0;
                productItem.Available = ((DO.Product)x).InStock > productItem.Amount;
                return productItem;
            });

        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while getting the products", e);
        }
    }

    /// <summary>
    /// Get product items by func filter
    /// </summary>
    /// <param name="cart">Cart of customer.</param>
    /// <param name="condition">Condition to be checked.</param>
    /// <returns>Collection of product item filtered by func</returns>
    /// <exception cref="DalException">Thrown when products cant be loaded.</exception>
    public IEnumerable<ProductItem?> GetProductItemsByFunc(BO.Cart cart, Func<ProductItem, bool> condition)
    {
        try
        {
            return Dal.Product.GetAll().Select(x =>
            {
                BO.OrderItem? item = cart?.ItemsList?.FirstOrDefault(y => y?.ProductId == ((DO.Product)x!).ID, null);
                var productItem = mapper.Map<DO.Product, BO.ProductItem>((DO.Product)x!);
                productItem.Amount = item?.Amount ?? 0;
                return productItem;
            }).Where(condition);


        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while getting the products", e);
        }
    }


    /// <summary>
    /// Get products list.
    /// </summary>
    /// <returns>Products list.</returns>
    /// <exception cref="DalException">Thrown when DB could not get the products.</exception>
    public IEnumerable<BO.ProductForList?> GetProducts()
    {
        try
        {
            return (from product in Dal.Product.GetAll()
                    orderby product?.ID
                    select mapper.Map<DO.Product, BO.ProductForList>((DO.Product)product));
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while getting the products", e);
        }
    }

    /// <summary>
    /// Get products list by function.
    /// </summary>
    /// <param name="condition">Condition to be checked on each product.</param>
    /// <returns>Products list.</returns>
    /// <exception cref="DalException">Thrown when DB could not get the products.</exception>
    public IEnumerable<BO.ProductForList?> GetProductsByFunc(Func<BO.ProductForList, bool> condition)
    {
        try
        {
            return (from product in Dal.Product.GetAll()
                    orderby product?.ID
                    let productForList = mapper.Map<DO.Product, BO.ProductForList>((DO.Product)product)
                    where condition(productForList)
                    select productForList);
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
        if (product.ID <= 0 || !Validation.IsName(product.Name)|| product.InStock < 0 || product.Price < 0)
            throw new InvalidInputException("product details are not valid. id, instock and price must be positive, name should be at least 3 chars long");
        try
        {
            Dal.Product.Update(mapper.Map<BO.Product, DO.Product>(product));
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while updating the products", e);
        }
    }


    /// <summary>
    /// Check if a product is ordered.
    /// </summary>
    /// <param name="productId">Id of product to be checked</param>
    /// <returns>Weather the product was ordered</returns>
    /// <exception cref="DalException">Thrown when DB could not get the data.</exception>
    private bool isOrdered(int productId)
    {
        try
        {
            IEnumerable<DO.OrderItem?> items = Dal.OrderItem.GetAll();

            return (from item in items
                    where item?.ProductId == productId
                    select item).ToArray().Length > 0;



        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while getting orders details", e);
        }
    }



}
