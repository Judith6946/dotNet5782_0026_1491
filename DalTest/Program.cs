
using Dal;
using DO;


/// <summary>
/// Main program, manage the user interface.
/// </summary>
internal class Program
{

    private static DalProduct dalProduct = new DalProduct();
    private static DalOrder dalOrder = new DalOrder();
    private static DalOrderItem dalOrderItem = new DalOrderItem();

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
        Console.WriteLine("\n press 0 to add an order. \n press 1 to search order by id.\n press 2 to print all orders.\n press 3 to update an order. \n press 4 to delete an order\npress 5 to search by order id.\n press 6 to search by order and product id.\n");
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
            dalProduct.Add(p);
        }
    }

    /// <summary>
    /// Print all products in DB.
    /// </summary>
    private static void printAllProducts()
    {
        Product[] products = dalProduct.GetAll();
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
        Product p = dalProduct.Get(id);
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
            Console.WriteLine(dalProduct.Get(id));
            Product p = getProductFromUser(id);
            dalProduct.Update(p);
        }

    }

    /// <summary>
    /// Delete a product.
    /// </summary>
    private static void deleteProduct()
    {
        Console.WriteLine("please enter product id:");
        int id = int.Parse(Console.ReadLine());
        dalProduct.Delete(id);
    }

    #endregion



    #region Order CRUD

    /// <summary>
    /// Get order details from user.
    /// </summary>
    /// <param name="id">Id of the new order</param>
    /// <returns>The new order object.</returns>
    private static Order getOrderFromUser(int id)
    {
        DateTime dt;
        Order order = new Order();
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
        Console.WriteLine("please enter order id:");
        string s = Console.ReadLine();
        int id;
        if (int.TryParse(s, out id))
        {
            Order order = getOrderFromUser(id);
            dalOrder.Add(order);
        }
    }

    /// <summary>
    /// Print all orders in DB.
    /// </summary>
    private static void printAllOrders()
    {
        Order[] orders = dalOrder.GetAll();
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
            Order order = dalOrder.Get(id);
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
            Console.WriteLine(dalOrder.Get(id));
            Order order = getOrderFromUser(id);
            dalOrder.Update(order);
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
            dalOrder.Delete(id);
    }

    #endregion



}