
using DO;

namespace Dal;

/// <summary>
/// The class represents a temporary data-source.
/// </summary>
internal static class DataSource
{

    #region config_class
    /// <summary>
    /// A config class that manages the indexes & id's of the entities array. 
    /// </summary>
    internal static class Config
    {
        //arrays next available index
        internal static int orderIndex = 0;
        internal static int orderItemIndex = 0;
        internal static int productIndex = 0;

        //arrays next id's
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

    //entities arrays
    internal static Product[] productsArr = new Product[50];
    internal static Order[] ordersArr = new Order[100];
    internal static OrderItem[] orderItemsArr = new OrderItem[200];

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
    /// Initialize the etities arrays. 
    /// </summary>
    private static void s_Initialize()
    {
        initializeProducts();
        initializeOrders();
        initializeOrderItems();
    }


    /// <summary>
    /// Initialize the products array. 
    /// </summary>
    private static void initializeProducts()
    {
        string[] productNames = new string[10] { "great ring", "golden necklace", "rose bracelet", "goldfild bracelet", "small silver earrings", "kids earrings", "personal design silver ring", "Wedding ring", "Long decorated earring", "Personalized designer necklace" };
        Enums.Category[] productsCategories = new Enums.Category[10] { Enums.Category.ring, Enums.Category.necklace, Enums.Category.bracelet, Enums.Category.bracelet, Enums.Category.earrings, Enums.Category.earrings, Enums.Category.ring, Enums.Category.ring, Enums.Category.earrings, Enums.Category.necklace };


        for (int i = 0; i < 10; i++)
        {
            Product p = new Product();
            p.ID = rn.Next(100000, 999999);
            p.Name = productNames[i];
            p.Category = productsCategories[i];
            p.Price = rn.Next(100, 500);
            p.InStock = rn.Next(0, 30);

            if (i == 0) p.InStock = 0;

            addProduct(p);
        }


    }


    /// <summary>
    /// Initialize the orders array. 
    /// </summary>
    private static void initializeOrders()
    {
        string[] customerNames = new string[10] { "Yehudit", "Chaya", "Sara", "David", "Moshe", "Tovi", "Adina", "Avi", "Dan", "Miri" };
        string[] customerEmails = new string[10] { "yt9074547@gmail.com", "Chaya9@gmail.com", "Sara@gmail.com", "David@gmail.com", "Moshe@gmail.com", "Tovi@gmail.com", "Adina@gmail.com", "Avi@gmail.com", "Dan@gmail.com", "Miri@gmail.com" };
        string[] customerAdress = new string[10] { "Rdak 4 Elad", "Haperach 5 Petach Tikva", "Rdak 4 Tel Aviv", "Dror 4 Bnei Brak", "Hatamar 67 Beit Shemesh", "Rdak 5 Elad", "Hateena 2 Elad", "Givataaim", "Derech Hashalom 11 Tel Aviv", "Kalanit 10 Eilat" };

        for (int i = 0; i < 20; i++)
        {
            Order order = new Order();
            order.ID = Config.OrderLastId;
            order.CustomerName = customerNames[i % 10];
            order.CustomerEmail = customerEmails[i % 10];
            order.CustomerAdress = customerAdress[i % 10];
            order.OrderDate = DateTime.MinValue;
            TimeSpan tms = new TimeSpan(rn.Next(1, 10), rn.Next(0, 24), rn.Next(0, 60), rn.Next(0, 60));
            order.ShipDate = DateTime.MinValue + tms;
            tms.Add(new TimeSpan(rn.Next(1, 10), rn.Next(0, 24), rn.Next(0, 60), rn.Next(0, 60)));
            order.DeliveryDate = order.ShipDate + tms;

            addOrder(order);
        }
    }


    /// <summary>
    /// Initialize the orderItems array. 
    /// </summary>
    private static void initializeOrderItems()
    {
        for (int i = 0; i < 40; i++)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.ID = Config.OrderItemLastId;
            orderItem.OrderId = ordersArr[i % 20].ID;
            orderItem.ProductId = productsArr[i % 10].ID;
            orderItem.Price = productsArr[i % 10].Price;
            orderItem.Amount = rn.Next(1, 5);

            addOrderItem(orderItem);
        }
    }

    #endregion


    #region Adding_Methods


    /// <summary>
    /// Add a product to the products array.
    /// </summary>
    /// <param name="product">The product to be added</param>
    internal static void addProduct(Product product)
    {
        if (!validateProductId(product.ID))
            throw new Exception("The product is already exist");

        int i = Config.productIndex;
        productsArr[i] = product;
        Config.productIndex++;
    }


    /// <summary>
    /// Add an order to the orders array.
    /// </summary>
    /// <param name="order">The order to be added</param>
    internal static void addOrder(Order order)
    {
        int i = Config.orderIndex;
        ordersArr[i] = order;
        Config.orderIndex++;
    }


    /// <summary>
    /// Add an orderItem to the orderItems array.
    /// </summary>
    /// <param name="orderItem">The orderItem to be added</param>
    internal static void addOrderItem(OrderItem orderItem)
    {
        int i = Config.orderItemIndex;
        orderItemsArr[i] = orderItem;
        Config.orderItemIndex++;
    }


    #endregion


    #region validation
    /// <summary>
    /// Validate an id of a new product.
    /// </summary>
    /// <param name="id">The id to be validated</param>
    /// <returns>A boolean variable that determines whether the id is ok</returns>
    internal static bool validateProductId(int id)
    {
        for(int i = 0; i < Config.productIndex; i++)
        {
            if (productsArr[i].ID == id)
                return false;
        }
        return true;
    }

    #endregion

}
