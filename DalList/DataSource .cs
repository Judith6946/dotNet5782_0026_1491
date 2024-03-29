﻿
using DO;

namespace Dal;

/// <summary>
/// The class represents a temporary data-source.
/// </summary>
internal static class DataSource
{

    #region config_class
    /// <summary>
    /// A config class that manages the indexes & id's of the entities list. 
    /// </summary>
    internal static class Config
    {

        //lists next id's
        private static int orderLastId = 1000;
        private static int orderItemLastId = 1000;

        /// <summary>
        /// A get property for the orderLastId field.
        /// </summary>
        public static int OrderLastId { get { return ++orderLastId; } }

        /// <summary>
        /// A get property for the orderItemLastId field.
        /// </summary>
        public static int OrderItemLastId { get { return ++orderItemLastId; } }

    }

    #endregion

    private static readonly Random rn = new Random();

    //entities lists
    internal static List<Product?> productsList = new List<Product?>();
    internal static List<Order?> ordersList = new List<Order?>();
    internal static List<OrderItem?> orderItemsList = new List<OrderItem?>();

    #region ctor
    /// <summary>
    /// A static ctor, call the initializer method.
    /// </summary>
    static DataSource()
    {
        s_Initialize();
    }
    #endregion


    #region Initializing_Methods



    /// <summary>
    /// Initialize the etities lists. 
    /// </summary>
    private static void s_Initialize()
    {
        initializeProducts();
        initializeOrders();
        initializeOrderItems();
    }


    /// <summary>
    /// Initialize the products list. 
    /// </summary>
    private static void initializeProducts()
    {
        string[] productNames = new string[10] { "MacBook pro", "ipad 10 mini", "iphone 14", " apple watch mini", "air pods 2", "ahr pods 3", "ipad Air", "MacBook", "iphone 14 mini", "Apple Watch SE" };
        Enums.Category[] productsCategories = new Enums.Category[10] { Enums.Category.Mac, Enums.Category.ipad, Enums.Category.iphone, Enums.Category.watch, Enums.Category.Accessories, Enums.Category.Accessories, Enums.Category.ipad, Enums.Category.Mac, Enums.Category.iphone, Enums.Category.watch };


        for (int j = 0; j < 10; j++)
        {
            Product p = new Product();
            p.ID = rn.Next(100000, 999999);
            p.Name = productNames[j];
            p.Category = productsCategories[j];
            p.Price = rn.Next(100, 500);
            p.InStock = rn.Next(0, 30);

            if (j <= 2) p.InStock = 0;
            productsList.Add(p);
        }

    }


    /// <summary>
    /// Initialize the orders list. 
    /// </summary>
    private static void initializeOrders()
    {
        string[] customerNames = new string[10] { "Yehudit", "Chaya", "Sara", "David", "Moshe", "Tovi", "Adina", "Avi", "Dan", "Miri" };
        string[] customerEmails = new string[10] { "yt9074547@gmail.com", "Chaya9@gmail.com", "Sara@gmail.com", "David@gmail.com", "Moshe@gmail.com", "Tovi@gmail.com", "Adina@gmail.com", "Avi@gmail.com", "Dan@gmail.com", "Miri@gmail.com" };
        string[] customerAdress = new string[10] { "Rdak 4 Elad", "Haperach 5 Petach Tikva", "Rdak 4 Tel Aviv", "Dror 4 Bnei Brak", "Hatamar 67 Beit Shemesh", "Rdak 5 Elad", "Hateena 2 Elad", "Givataaim", "Derech Hashalom 11 Tel Aviv", "Kalanit 10 Eilat" };


        for (int j = 0; j < 20; j++)
        {
            TimeSpan tms;

            Order order = new Order();
            order.ID = Config.OrderLastId;

            order.CustomerName = customerNames[j % 10];
            order.CustomerEmail = customerEmails[j % 10];
            order.CustomerAdress = customerAdress[j % 10];

            order.OrderDate = new(rn.Next(1999, 2023), rn.Next(1, 13), rn.Next(1, 28));
            tms = new TimeSpan(rn.Next(1, 10), rn.Next(0, 24), rn.Next(0, 60), rn.Next(0, 60));

            if (j < 16)
            {
                order.ShipDate = order.OrderDate + tms;
            }

            if (j < 10)
            {
                tms.Add(new TimeSpan(rn.Next(1, 10), rn.Next(0, 24), rn.Next(0, 60), rn.Next(0, 60)));
                order.DeliveryDate = order.ShipDate + tms;
            }

            ordersList.Add(order);
        }
    }


    /// <summary>
    /// Initialize the orderItems list. 
    /// </summary>
    private static void initializeOrderItems()
    {
        Random rn = new Random();

        for (int j = 0; j < 40; j++)
        {
            int x = rn.Next(10);
            OrderItem item = new OrderItem();
            item.ID = Config.OrderItemLastId;
            item.OrderId = (int)ordersList[j % 20]?.ID!;
            item.ProductId = (int)productsList[x]?.ID!;
            item.Price = (int)productsList[x]?.Price!;
            item.Amount = rn.Next(1, 5);

            orderItemsList.Add(item);

        }
    }

    #endregion




}
