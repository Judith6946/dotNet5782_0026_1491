﻿
using AutoMapper;
using BlApi;
using BO;
using DalApi;

namespace BlImplementation;

/// <summary>
///  Implementation of cart methods (BL layer)
/// </summary>
internal class Cart : ICart
{
    private static DalApi.IDal Dal = DalApi.Factory.Get();
    private static IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile(new BoProfile())).CreateMapper();



    /// <summary>
    /// Add an item to customer cart.
    /// </summary>
    /// <param name="cart">Customer cart.</param>
    /// <param name="id">Id of product to be added.</param>
    /// <returns>Updated cart.</returns>
    /// <exception cref="SoldOutException">Thrown when the required product is sold out.</exception>
    /// <exception cref="InvalidInputException">Thrown when Id was invalid.</exception>
    /// <exception cref="DalException">Thrown when error was occured while reaching the DB.</exception>
    public BO.Cart AddItem(BO.Cart cart, int id)
    {
        if (id <= 0)
            throw new InvalidInputException("Id should be a positive number.");

        if (cart.ItemsList == null)
            cart.ItemsList = new List<OrderItem?>();

        //find item on the items list of cart.
        int index = cart.ItemsList.FindIndex(x => x!.ProductId == id);

        //if you find the item - update amount.
        if (index != -1)
        {
            BO.OrderItem item = cart.ItemsList[index]!;
            if (isSoldOut(id, item.Amount + 1))
                throw new SoldOutException("This product was sold out.");
            item.Amount++;
            item.TotalPrice += item.Price;
            cart.TotalPrice += item.Price;
        }

        //did not find the item - add it now.
        else
        {
            DO.Product p = getProduct(id);
            if (p.InStock <= 0)
                throw new SoldOutException("This product was sold out.");
            BO.OrderItem item = mapper.Map<DO.Product, BO.OrderItem>(p);
            item.TotalPrice = item.Price;
            cart.ItemsList.Add(item);
            cart.TotalPrice += p.Price;
        }
        cart.ItemsList = cart.ItemsList.OrderBy(x => x?.ID).ToList();
        return cart;
    }

    /// <summary>
    /// Make an order from customer cart.
    /// </summary>
    /// <param name="cart">Customer cart.</param>
    /// <exception cref="SoldOutException">Thrown when one of the products was sold out.</exception>
    /// <exception cref="InvalidInputException">Thrown when order details were invalid</exception>
    /// <exception cref="DalException">Thrown when error was occured while reaching the DB.</exception>
    public void MakeOrder(BO.Cart cart)
    {

        checkOrderValidity(cart);
        //add new order
        DO.Order order = mapper.Map<BO.Cart, DO.Order>(cart);
        order.DeliveryDate = order.ShipDate = null;
        int id = addOrder(order);

        //add all order items...
        foreach (var item in cart.ItemsList!)
        {
            //update amount of product.
            DO.Product p = getProduct(item!.ProductId);
            p.InStock -= item.Amount;
            if (p.InStock < 0)
                throw new SoldOutException($"product {p.Name}, id={p.ID} was sold out");
            updateProduct(p);

            //add an order item.
            DO.OrderItem orderItem = mapper.Map<BO.OrderItem, DO.OrderItem>(item);
            orderItem.OrderId = id;
            addOrderItem(orderItem);
        }

    }

    /// <summary>
    /// Remove an item from cart.
    /// </summary>
    /// <param name="cart">Cart of customer.</param>
    /// <param name="id">Id of product to be removed.</param>
    /// <returns>Updated cart.</returns>
    /// <exception cref="NotFoundException">Thrown when product cant be found on this cart.</exception>
    /// <exception cref="InvalidInputException">Thrown when id was invalid.</exception>
    public BO.Cart RemoveItem(BO.Cart cart, int id)
    {
        if (id <= 0)
            throw new InvalidInputException("Id should be a positive number.");
        if (cart.ItemsList == null)
            throw new NotFoundException("Cannot find this product in your cart");

        //find item on the items list of cart.
        int index = cart.ItemsList.FindIndex(x => x!.ProductId == id);
        if (index == -1)
            throw new NotFoundException("Cannot find this product in your cart");

        int newAmount = cart.ItemsList[index]!.Amount - 1;
        return UpdateAmount(cart, newAmount, id);

    }


    /// <summary>
    /// Update amount of product on customer cart.
    /// </summary>
    /// <param name="cart">Customer cart.</param>
    /// <param name="amount">Updated amount.</param>
    /// <param name="id">Id of product to be updated.</param>
    /// <returns>Updated cart.</returns>
    /// <exception cref="InvalidInputException">Thrown when amount or id is invalid</exception>
    /// <exception cref="NotFoundException">Thrown when product is not found on cart.</exception>
    /// <exception cref="SoldOutException">Thrown when product is sold out.</exception>
    public BO.Cart UpdateAmount(BO.Cart cart, int amount, int id)
    {
        if (cart.ItemsList == null)
            throw new InvalidInputException("your cart is empty");

        //check input validity
        if (id<=0)
            throw new InvalidInputException("Id should be a positive number.");

        //check input validity
        if (amount < 0)
            throw new InvalidInputException("Amount cannot be negative.");

        //find the item
        BO.OrderItem? item = cart.ItemsList.FirstOrDefault(x => x!.ProductId == id, null);
        if (item == null)
            throw new BO.NotFoundException("Cannot find this product on your cart.");

        //check amount
        if (item.Amount < amount && isSoldOut(item.ProductId, amount))
            throw new SoldOutException("This product is sold out.");

        cart.ItemsList.RemoveAll(x => x!.ProductId == item.ProductId);
        if (amount != 0)
        {
            //update amount
            cart.TotalPrice = cart.TotalPrice - item.TotalPrice + item.Price * amount;
            item.Amount = amount;
            item.TotalPrice = item.Price * amount;

            //add updated item
            cart.ItemsList.Add(item);
        }

        //return updated cart.
        cart.ItemsList = cart.ItemsList.OrderBy(x => x?.ID).ToList();
        return cart;

    }



    #region UTILS

    /// <summary>
    /// Determines whether the product is sold out.
    /// </summary>
    /// <param name="id">Id of product to be checked.</param>
    /// <param name="amount">Needed amount of product.</param>
    /// <returns>Whether the product is sold out.</returns>
    /// <exception cref="DalException">Thrown when DB could not get the product details.</exception>
    private static bool isSoldOut(int id, int amount)
    {
        try
        {
            return Dal.Product.GetById(id).InStock <= amount;
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while getting the product.", e);
        }
    }


    /// <summary>
    /// Get a product by id.
    /// </summary>
    /// <param name="id">Id of required product</param>
    /// <returns>Product entity</returns>
    /// <exception cref="DalException">Thrown when DB could not get the product.</exception>
    private static DO.Product getProduct(int id)
    {
        try
        {
            return Dal.Product.GetById(id);
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while getting the product.", e);
        }

    }


    /// <summary>
    /// Add an order to DB.
    /// </summary>
    /// <param name="order">Order object to be added.</param>
    /// <returns>Id of new order.</returns>
    /// <exception cref="DalException">Thrown when DB could not add the order.</exception>
    private static int addOrder(DO.Order order)
    {
        try
        {
            int id = Dal.Order.Add(order);
            return id;
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while adding the order.", e);
        }

    }


    /// <summary>
    /// Add an order item to DB.
    /// </summary>
    /// <param name="orderItem">Order item object to be added.</param>
    /// <returns>Id of new order item.</returns>
    /// <exception cref="DalException">Thrown when DB could not add the order item.</exception>
    private static int addOrderItem(DO.OrderItem orderItem)
    {
        try
        {
            int id = Dal.OrderItem.Add(orderItem);
            return id;
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while adding the order item.", e);
        }

    }


    /// <summary>
    /// Update a product.
    /// </summary>
    /// <param name="product">Product object to be update.</param>
    /// <exception cref="DalException">Thrown when DB could not update the product.</exception>
    private static void updateProduct(DO.Product product)
    {
        try
        {
            Dal.Product.Update(product);
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while updating the product.", e);
        }

    }

    /// <summary>
    /// Check order validity of cart.
    /// </summary>
    /// <param name="cart">Cart to be checked.</param>
    /// <exception cref="InvalidInputException">Thrown when order details were invalid</exception>
    /// <exception cref="SoldOutException">Trown when one of the products was sold out.</exception>
    private static void checkOrderValidity(BO.Cart cart)
    {
        if (cart.ItemsList == null || cart.ItemsList.Count == 0)
            throw new InvalidInputException("Your cart is empty");

        if(!cart.CustomerEmail.IsEmail())
            throw new InvalidInputException("Email is invalid");

        if (!cart.CustomerName.IsName()||!cart.CustomerAdress.IsName())
            throw new InvalidInputException("Name and Address should be at least 3 chars");

        foreach (var item in cart.ItemsList)
        {
            if (item == null)
                throw new InvalidInputException("item cannot be null");
            if (item.Price < 0 || item.Amount < 0)
                throw new InvalidInputException("price & amount cannot be negative");
            var product = getProduct(item.ProductId);
            if (product.InStock < item.Amount)
                throw new SoldOutException($"product {item.ProductId} was sold out");
        }
    }

    #endregion

}
