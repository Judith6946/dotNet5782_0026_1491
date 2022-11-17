using DalApi;
using Dal;
using DO;


/// <summary>
/// Main program, manage the user interface.
/// </summary>
internal class Program
{

    static IDal dal = new DalList();

    #region ENUMS
    private enum Menue { EXIT, PRODUCT, ORDER, ORDER_ITEM };
    private enum Choice { ADD, SEARCH, PRINT, UPDATE, DELETE }
    private enum OrderItemChoice { ADD, SEARCH_BY_ID, PRINT, UPDATE, DELETE, SEARCH_BY_ORDER, SEARCH_BY_ORDER_AND_PRODUCT }

    #endregion


    private static void Main(string[] args)
    {
        try
        {
            Menue menue;
            menue = getMenu();

            while (menue != Menue.EXIT)
            {
                switch (menue)
                {
                    case Menue.PRODUCT:
                        productMenu();
                        break;
                    case Menue.ORDER:
                        orderMenu();
                        break;
                    case Menue.ORDER_ITEM:
                        orderItemMenu();
                        break;
                    default:
                        Console.WriteLine("No such menu, please try again.");
                        break;
                }

                menue = getMenu();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }


    #region Menus

    /// <summary>
    /// Print product menu, get an option from user and prints result. 
    /// </summary>
    private static void productMenu()
    {
        Choice c = getProductChoice();
        try
        {
            switch (c)
            {
                case Choice.ADD:
                    addProduct();
                    break;
                case Choice.SEARCH:
                    searchProduct();
                    break;
                case Choice.PRINT:
                    printAllProducts();
                    break;
                case Choice.UPDATE:
                    updateProduct();
                    break;
                case Choice.DELETE:
                    deleteProduct();
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// Print order menu, get an option from user and prints result. 
    /// </summary>
    private static void orderMenu()
    {
        Choice c = getOrderChoice();
        try
        {
            switch (c)
            {
                case Choice.ADD:
                    addOrder();
                    break;
                case Choice.SEARCH:
                    searchOrder();
                    break;
                case Choice.PRINT:
                    printAllOrders();
                    break;
                case Choice.UPDATE:
                    updateOrder();
                    break;
                case Choice.DELETE:
                    deleteOrder();
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// Print order-item menu, get an option from user and prints result. 
    /// </summary>
    private static void orderItemMenu()
    {
        OrderItemChoice choice = getOrderItemChoice();
        try
        {
            switch (choice)
            {
                case OrderItemChoice.ADD:
                    addOrderItem();
                    break;
                case OrderItemChoice.SEARCH_BY_ID:
                    searchOrderItem();
                    break;
                case OrderItemChoice.PRINT:
                    printAllOrderItems();
                    break;
                case OrderItemChoice.UPDATE:
                    updateOrderItem();
                    break;
                case OrderItemChoice.DELETE:
                    deleteOrderItem();
                    break;
                case OrderItemChoice.SEARCH_BY_ORDER:
                    searchByOrder();
                    break;
                case OrderItemChoice.SEARCH_BY_ORDER_AND_PRODUCT:
                    searchByOrderAndProduct();
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    #endregion


    #region  User choices methods

    /// <summary>
    /// Get a menu option chosen by user.
    /// </summary>
    /// <returns>The chosen menu.</returns>
    private static Menue getMenu()
    {
        Console.WriteLine("Choose a menu: \n press 1 to the product menu. \n press 2 to the order menu. \n press 3 to the order item menu. \n press 0 to exit.");
        Menue menue = (Menue)Enum.Parse(typeof(Menue), Console.ReadLine());
        return menue;
    }

    /// <summary>
    /// Get a product-menu option chosen by user.
    /// </summary>
    /// <returns>The chosen option.</returns>
    private static Choice getProductChoice()
    {
        Console.WriteLine("\n press 0 to add a product. \n press 1 to search product by id.\n press 2 to print all products.\n press 3 to update a product. \n press 4 to delete a product\n");
        Choice c = (Choice)Enum.Parse(typeof(Choice), Console.ReadLine());
        return c;
    }

    /// <summary>
    /// Get an order-menu option chosen by user.
    /// </summary>
    /// <returns>The chosen option.</returns>
    private static OrderItemChoice getOrderItemChoice()
    {
        Console.WriteLine("\n press 0 to add an order item. \n press 1 to search order item by id.\n press 2 to print all items.\n press 3 to update an order item. \n press 4 to delete an order item.\npress 5 to search by order id.\n press 6 to search by order and product id.\n");
        OrderItemChoice c = (OrderItemChoice)Enum.Parse(typeof(OrderItemChoice), Console.ReadLine());
        return c;
    }

    /// <summary>
    /// Get an order-item-menu option chosen by user.
    /// </summary>
    /// <returns>The chosen option.</returns>
    private static Choice getOrderChoice()
    {
        Console.WriteLine("\n press 0 to add an order. \n press 1 to search order by id.\n press 2 to print all orders.\n press 3 to update an order. \n press 4 to delete an order.\n ");
        Choice c = (Choice)Enum.Parse(typeof(Choice), Console.ReadLine());
        return c;
    }

    #endregion



    #region Product CRUD

    /// <summary>
    /// Get product details from user.
    /// </summary>
    /// <param name="id">Id of the new product</param>
    /// <returns>The new product object.</returns>
    private static Product getProductFromUser(int id)
    {
        Product p = new Product();
        p.ID = id;
        Console.WriteLine("please enter product name:");
        p.Name = Console.ReadLine();
        Console.WriteLine("please enter product price:");
        p.Price = int.Parse(Console.ReadLine());
        Console.WriteLine("please enter amount in stock");
        p.InStock = int.Parse(Console.ReadLine());
        Console.WriteLine("please choose a category: \npress 0 for necklace \npress 1 for ring \npress 2 for bracelet \npress 3 for earrings \npress 4 for personal_design");
        p.Category = (Enums.Category)Enum.Parse(typeof(Enums.Category), Console.ReadLine());

        return p;
    }

    /// <summary>
    /// Add a product to DB.
    /// </summary>
    private static void addProduct()
    {
        Console.WriteLine("please enter product id:");
        string s = Console.ReadLine();
        int id;
        if (int.TryParse(s, out id))
        {
            Product p = getProductFromUser(id);
            dal.Product.Add(p);
            Console.WriteLine("Adding succeeded");
        }
    }

    /// <summary>
    /// Print all products in DB.
    /// </summary>
    private static void printAllProducts()
    {
        IEnumerable<Product> products = dal.Product.GetAll();
        foreach (Product p in products)
        {
            Console.WriteLine(p);
        }
    }

    /// <summary>
    /// Search a product by id.
    /// </summary>
    private static void searchProduct()
    {
        Console.WriteLine("please enter product id:");
        int id = int.Parse(Console.ReadLine());
        Product p = dal.Product.GetById(id);
        Console.WriteLine(p);
    }

    /// <summary>
    /// Update a product.
    /// </summary>
    private static void updateProduct()
    {
        Console.WriteLine("please enter product id:");
        string s = Console.ReadLine();
        int id;
        if (int.TryParse(s, out id))
        {
            Console.WriteLine(dal.Product.GetById(id));
            Product p = getProductFromUser(id);
            dal.Product.Update(p);
            Console.WriteLine("Updating succeeded");
        }

    }

    /// <summary>
    /// Delete a product.
    /// </summary>
    private static void deleteProduct()
    {
        Console.WriteLine("please enter product id:");
        int id;
        if(int.TryParse(Console.ReadLine(),out id))
        {
            dal.Product.Delete(id);
            Console.WriteLine("Deleting succeeded");
        }
        
    }

    #endregion



    #region Order CRUD

    /// <summary>
    /// Get order details from user.
    /// </summary>
    /// <param name="id">Id of the new order</param>
    /// <returns>The new order object.</returns>
    private static Order getOrderFromUser(int id = 0)
    {
        DateTime dt;
        Order order = new Order();
        if (id > 0)
            order.ID = id;
        Console.WriteLine("please enter order customer name:");
        order.CustomerName = Console.ReadLine();
        Console.WriteLine("please enter order customer email:");
        order.CustomerEmail = Console.ReadLine();
        Console.WriteLine("please enter order customer adress:");
        order.CustomerAdress = Console.ReadLine();

        Console.WriteLine("please enter order date:");
        if (DateTime.TryParse(Console.ReadLine(), out dt))
            order.OrderDate = dt;
        else
            throw new Exception("Unvalid date.");

        Console.WriteLine("please enter order ship date, if exist:");
        if (DateTime.TryParse(Console.ReadLine(), out dt))
            order.ShipDate = dt;

        Console.WriteLine("please enter order delivery date, if exist:");
        if (DateTime.TryParse(Console.ReadLine(), out dt))
            order.DeliveryDate = dt;

        return order;
    }

    /// <summary>
    /// Add an order to DB.
    /// </summary>
    private static void addOrder()
    {
        Order order = getOrderFromUser();
        dal.Order.Add(order);
        Console.WriteLine("Adding succeeded");
    }

    /// <summary>
    /// Print all orders in DB.
    /// </summary>
    private static void printAllOrders()
    {
       IEnumerable<Order> orders = dal.Order.GetAll();
        foreach (Order order in orders)
        {
            Console.WriteLine(order);
        }
    }

    /// <summary>
    /// Search an order by id.
    /// </summary>
    private static void searchOrder()
    {
        Console.WriteLine("please enter order id:");
        int id;
        if (int.TryParse(Console.ReadLine(), out id))
        {
            Order order = dal.Order.GetById(id);
            Console.WriteLine(order);
        }

    }

    /// <summary>
    /// Update an order.
    /// </summary>
    private static void updateOrder()
    {
        Console.WriteLine("please enter order id:");
        string s = Console.ReadLine();
        int id;
        if (int.TryParse(s, out id))
        {
            Console.WriteLine(dal.Order.GetById(id));
            Order order = getOrderFromUser(id);
            dal.Order.Update(order);
            Console.WriteLine("Updating succeeded");
        }

    }

    /// <summary>
    /// Delete an order.
    /// </summary>
    private static void deleteOrder()
    {
        Console.WriteLine("please enter order id:");
        int id;
        if (int.TryParse(Console.ReadLine(), out id))
        {
            dal.Order.Delete(id);
            Console.WriteLine("Deleting succeeded");
        }
           
    }

    #endregion



    #region OrderItem CRUD

    /// <summary>
    /// Get order item details from user.
    /// </summary>
    /// <param name="id">Id of the new order item</param>
    /// <returns>The new order item object.</returns>
    private static OrderItem getOrderItemFromUser(int id = 0)
    {
        try
        {
            OrderItem item = new OrderItem();
            if (id > 0)
                item.ID = id;
            Console.WriteLine("please enter product id:");
            item.ProductId = int.Parse(Console.ReadLine());
            Console.WriteLine("please enter order id:");
            item.OrderId = int.Parse(Console.ReadLine());
            Console.WriteLine("please enter price:");
            item.Price = int.Parse(Console.ReadLine());
            Console.WriteLine("please enter amount:");
            item.Amount = int.Parse(Console.ReadLine());
            return item;
        }
        catch (Exception e)
        {
            throw new Exception("invalid value");
        }


    }

    /// <summary>
    /// Add an order item to DB.
    /// </summary>
    private static void addOrderItem()
    {
        OrderItem item = getOrderItemFromUser();
        dal.OrderItem.Add(item);
        Console.WriteLine("Adding succeeded");
    }

    /// <summary>
    /// Print all order items in DB.
    /// </summary>
    private static void printAllOrderItems()
    {
        IEnumerable<OrderItem> items = dal.OrderItem.GetAll();
        foreach (OrderItem item in items)
        {
            Console.WriteLine(item);
        }
    }

    /// <summary>
    /// Search an order item by id.
    /// </summary>
    private static void searchOrderItem()
    {
        Console.WriteLine("please enter order item id:");
        int id;
        if (int.TryParse(Console.ReadLine(), out id))
        {
            OrderItem items = dal.OrderItem.GetById(id);
            Console.WriteLine(items);
        }

    }

    /// <summary>
    /// Update an order item.
    /// </summary>
    private static void updateOrderItem()
    {
        Console.WriteLine("please enter order item id:");
        string s = Console.ReadLine();
        int id;
        if (int.TryParse(s, out id))
        {
            Console.WriteLine(dal.OrderItem.GetById(id));
            OrderItem item = getOrderItemFromUser(id);
            dal.OrderItem.Update(item);
            Console.WriteLine("Updating succeeded");
        }

    }

    /// <summary>
    /// Delete an order.
    /// </summary>
    private static void deleteOrderItem()
    {
        Console.WriteLine("please enter order item id:");
        int id;
        if (int.TryParse(Console.ReadLine(), out id))
        {
            dal.OrderItem.Delete(id);
            Console.WriteLine("Deleting succeeded");
        }
           
    }

    /// <summary>
    /// Print all items on a given order.
    /// </summary>
    private static void searchByOrder()
    {
        Console.WriteLine("please enter order id:");
        int id;
        if (int.TryParse(Console.ReadLine(), out id))
        {
            IEnumerable<OrderItem> items = dal.OrderItem.GetByOrder(id);
            foreach (OrderItem item in items)
            {
                Console.WriteLine(item);
            }
        }
    }

    /// <summary>
    /// Print all items on a given order and product.
    /// </summary>
    private static void searchByOrderAndProduct()
    {
        Console.WriteLine("please enter order and product id:");
        int orderId, productId;
        if (int.TryParse(Console.ReadLine(), out orderId) && int.TryParse(Console.ReadLine(), out productId))
        {
            OrderItem item = dal.OrderItem.GetByOrderAndProduct(orderId, productId);
            Console.WriteLine(item);
        }
    }


    #endregion



}