
using AutoMapper;
using BO;
using DalApi;


namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private static DalApi.IDal Dal = DalApi.Factory.Get();
    private static IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile(new BoProfile())).CreateMapper();


    /// <summary>
    /// Follow an order - get an orderTracking object that describes an order.
    /// </summary>
    /// <param name="id">Id of order to be followed.</param>
    /// <returns>An orderTracking object that describes an order.</returns>
    /// <exception cref="InvalidInputException">Thrown when id is not valid.</exception>
    /// <exception cref="DalException">Thrown when there was an error contacting the database.</exception>
    public BO.OrderTracking FollowOrder(int id)
    {
        if (id <= 0)
            throw new InvalidInputException("Id is a positive value.");

        //get order and built orderTracking object.
        DO.Order order = getOrder(id);
        BO.OrderTracking orderTracking = mapper.Map<DO.Order, BO.OrderTracking>(order);

        //set the tracking list.
        orderTracking.Tracking!.Add(new Tuple<DateTime?, string?>(order.OrderDate, "Order approved"));
        if (order.ShipDate != null)
            orderTracking.Tracking!.Add(new Tuple<DateTime?, string?>(order.ShipDate, "Order sent"));
        if (order.DeliveryDate != null)
            orderTracking.Tracking!.Add(new Tuple<DateTime?, string?>(order.DeliveryDate, "Order delivered"));

        //return the tracking.
        return orderTracking;
    }


    /// <summary>
    /// Get an order object
    /// </summary>
    /// <param name="id">Id of required order.</param>
    /// <returns>Required order.</returns>
    /// <exception cref="InvalidInputException">Thrown when id is not valid.</exception>
    /// <exception cref="DalException">Thrown when there was an error contacting the database.</exception>
    public BO.Order GetOrder(int id)
    {

        //check id validity
        if (id < 0)
            throw new InvalidInputException("Id is a positive number.");

        //get order
        DO.Order o = getOrder(id);
        BO.Order order = mapper.Map<DO.Order, BO.Order>(o);

        //get order items
        IEnumerable<DO.OrderItem?> items = getOrderItemsByOrder(id);
        order.TotalPrice = items.Sum(x => x?.Price * x?.Amount) ?? 0;

        order.ItemsList = items.Select(item =>
        {
            BO.OrderItem? orderItem = mapper.Map<DO.OrderItem, BO.OrderItem>(item ?? new());
            orderItem.ProductName = getProduct(item?.ProductId ?? 0).Name;
            return orderItem;
        }).ToList<OrderItem?>();

      
        //return order object.
        return order;

    }


    /// <summary>
    /// Get all orders
    /// </summary>
    /// <returns>Collection of all orders.</returns>
    /// <exception cref="DalException">Thrown when there was an error contacting the database.</exception>
    public IEnumerable<BO.OrderForList> GetOrders()
    {
        return getOrders().Select(order =>
        {
            BO.OrderForList orderForList = mapper.Map<DO.Order, BO.OrderForList>(order ?? new());
            orderForList.TotalPrice = getOrderItemsByOrder(order?.ID ?? 0).Sum(x => x?.Price * x?.Amount) ?? 0;
            orderForList.AmountOfItems = getOrderItemsByOrder(order?.ID ?? 0).Sum(x => x?.Amount) ?? 0;
            return orderForList;
        });
    }


    /// <summary>
    /// Update delivery date of order.
    /// </summary>
    /// <param name="id">Id of order to be updated.</param>
    /// <returns></returns>
    /// <exception cref="InvalidInputException">Thrown when id is not valid.</exception>
    /// <exception cref="AlreadyDoneException">Thrown when order was already delivered.</exception>
    /// <exception cref="DalException">Thrown when there was an error contacting the database.</exception>
    public BO.Order UpdateOrderDelivery(int id)
    {
        //check id validity.
        if (id <= 0)
            throw new InvalidInputException("Id is a positive value.");

        //find order.
        DO.Order order = getOrder(id);
        if (order.DeliveryDate != null)
            throw new AlreadyDoneException("Order was already delivered.");

        //find order items
        IEnumerable<DO.OrderItem?> orderItems = getOrderItemsByOrder(id);

        //update delivery date.
        order.DeliveryDate = DateTime.Now;
        BO.Order order2 = mapper.Map<DO.Order, BO.Order>(order);
        order2.Status = BO.Enums.OrderStatus.delivered;
        order2.TotalPrice = orderItems.Sum(x => x?.Price) ?? 0;

        //add items
        order2.ItemsList = orderItems.Select(item =>
        {
            BO.OrderItem orderItem = mapper.Map<DO.OrderItem, BO.OrderItem>(item ?? new());
            orderItem.ProductName = getProduct(item?.ProductId ?? 0).Name;
            return orderItem;
        }).ToList<OrderItem?>();

        updateProduct(order);
        return order2;

    }


    /// <summary>
    /// Update ship date of order.
    /// </summary>
    /// <param name="id">Id of order to be updated.</param>
    /// <returns></returns>
    /// <exception cref="InvalidInputException">Thrown when id is not valid.</exception>
    /// <exception cref="AlreadyDoneException">Thrown when order was already shipped.</exception>
    /// <exception cref="DalException">Thrown when there was an error contacting the database.</exception>
    public BO.Order UpdateOrderShipping(int id)
    {
        //check id validity.
        if (id <= 0)
            throw new InvalidInputException("Id is a positive value.");

        //find order.
        DO.Order order = getOrder(id);
        if (order.ShipDate != null)
            throw new AlreadyDoneException("Order was already shipped.");

        //find order items
        IEnumerable<DO.OrderItem?> orderItems = getOrderItemsByOrder(id);

        //update delivery date.
        order.ShipDate = DateTime.Now;
        BO.Order order2 = mapper.Map<DO.Order, BO.Order>(order);
        order2.Status = BO.Enums.OrderStatus.sent;
        order2.TotalPrice = orderItems.Sum(x => x?.Price * x?.Amount) ?? 0;

        //add items
        order2.ItemsList = orderItems.Select(item =>
        {
            BO.OrderItem orderItem = mapper.Map<DO.OrderItem, BO.OrderItem>(item ?? new());
            orderItem.ProductName = getProduct(item?.ProductId ?? 0).Name;
            return orderItem;
        }).ToList<OrderItem?>();

        updateProduct(order);
        return order2;


    }

    /// <summary>
    /// Update an amount of item on order.
    /// </summary>
    /// <param name="id">Id of order</param>
    /// <param name="productId">Id of product</param>
    /// <param name="amount">Updated amount</param>
    /// <returns>Updated order</returns>
    /// <exception cref="InvalidInputException">Thrown when input is invalid</exception>
    /// <exception cref="ImpossibleException">Thrown when order was already shipped</exception>
    /// <exception cref="NotFoundException">Thrown when no such product was found on this order.</exception>
    /// <exception cref="DalException">Thrown when there was an error contacting the database.</exception>
    public BO.Order UpdateOrder(int id, int productId, int amount)
    {
        //check id validity.
        if (id <= 0)
            throw new InvalidInputException("Id is a positive value.");

        //find order.
        DO.Order order = getOrder(id);
        if (order.ShipDate != null)
            throw new ImpossibleException("Order was already shipped.");
        BO.Order order2 = mapper.Map<DO.Order, BO.Order>(order);

        //find order items
        IEnumerable<DO.OrderItem?> orderItems = getOrderItemsByOrder(id);
        DO.OrderItem item1 = orderItems.FirstOrDefault(x => ((DO.OrderItem?)x)?.OrderId == id&& ((DO.OrderItem?)x)?.ProductId==productId) 
            ?? throw new BO.NotFoundException("Could not found this product.");
        item1.Amount = amount;

        if (amount == 0)
            removeOrderItem(item1.ID); 
        else
            updateOrderItem(item1);

        order2.TotalPrice = orderItems.Sum(x => x?.Price * x?.Amount) ?? 0;

        //add items
        foreach (var item in orderItems)
        {
            if (item?.ID == item1.ID && amount == 0)
                continue;
            BO.OrderItem orderItem = mapper.Map<DO.OrderItem, BO.OrderItem>(item ?? new());
            orderItem.ProductName = getProduct(item?.ProductId ?? 0).Name;
            if (item?.ID == item1.ID)
            {
                orderItem.Amount = amount;
                orderItem.TotalPrice=orderItem.Price* orderItem.Amount;
            }            
            order2.ItemsList!.Add(orderItem);
        }


        return order2;
    }

    /// <summary>
    /// Get id of oldest order.
    /// </summary>
    /// <returns>Id of last modified order</returns>
    /// <exception cref="DalException">Thrown when orders details cannot be loaded</exception>
    public int? GetOldestOrder()
    {
        var orders = getOrders();
        var lastOrder = (from order in orders
                where order?.DeliveryDate == null
                let date = order?.ShipDate == null ? order?.OrderDate : order?.ShipDate
                orderby date
                select new { date = date, id = order?.ID }).MinBy(x => x.date);
        return lastOrder == null ? null : lastOrder.id;
    }

    /// <summary>
    /// Update status of desired order
    /// </summary>
    /// <param name="order_id">Order to be updated</param>
    /// <exception cref="ImpossibleException">Thrown when order was already delivered.</exception>
    ///<exception cref="DalException">Thrown when order details cannot be loaded.</exception>
    ///<exception cref="InvalidInputException">Thrown when id was not valid.</exception>
    public void UpdateStatus(int order_id)
    {
        if (order_id <= 0)
            throw new InvalidInputException("Id should be a positive value");
        var order = getOrder(order_id);
        if (order.DeliveryDate != null)
            throw new ImpossibleException("Cannot update status of order that has already been delivered");
        if (order.ShipDate == null)
            UpdateOrderShipping(order_id);
        else
            UpdateOrderDelivery(order_id);
    }


    #region UTILS

    /// <summary>
    /// Get all orders.
    /// </summary>
    /// <returns>Collection of orders</returns>
    /// <exception cref="DalException">Thrown when DB could not get the orders.</exception>
    private static IEnumerable<DO.Order?> getOrders()
    {
        try
        {
            return Dal.Order.GetAll();
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while getting the orders.", e);
        }

    }

    /// <summary>
    /// Get an order by id.
    /// </summary>
    /// <param name="id">Id of required order</param>
    /// <returns>Order entity</returns>
    /// <exception cref="DalException">Thrown when DB could not get the order.</exception>
    private static DO.Order getOrder(int id)
    {
        try
        {
            return Dal.Order.GetById(id);
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while getting the order.", e);
        }

    }


    /// <summary>
    /// Get all the order items of a specific order.
    /// </summary>
    /// <param name="id">Id of required order</param>
    /// <returns>Collection of order items in order.</returns>
    /// <exception cref="DalException">Thrown when DB could not get the order items.</exception>
    private static IEnumerable<DO.OrderItem?> getOrderItemsByOrder(int id)
    {
        try
        {
            return Dal.OrderItem.GetByOrder(id);
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while getting the order items.", e);
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
    /// Update an order.
    /// </summary>
    /// <param name="product">Order object to be update.</param>
    /// <exception cref="DalException">Thrown when DB could not update the order.</exception>
    private static void updateProduct(DO.Order order)
    {
        try
        {
            Dal.Order.Update(order);
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while updating the order.", e);
        }

    }

    /// <summary>
    /// Update an order item.
    /// </summary>
    /// <param name="product">Order item object to be update.</param>
    /// <exception cref="DalException">Thrown when DB could not update the order item.</exception>
    private static void updateOrderItem(DO.OrderItem item)
    {
        try
        {
            Dal.OrderItem.Update(item);
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while updating the order item.", e);
        }

    }

    /// <summary>
    /// Remove an order item.
    /// </summary>
    /// <param name="itemId">ID of Order item object to be removed.</param>
    /// <exception cref="DalException">Thrown when DB could not remove the order item.</exception>
    private static void removeOrderItem(int itemId)
    {
        try
        {
            Dal.OrderItem.Delete(itemId);
        }
        catch (Exception e)
        {
            throw new DalException("Exception was thrown while deleting the order item.", e);
        }

    }

    

    #endregion



}


