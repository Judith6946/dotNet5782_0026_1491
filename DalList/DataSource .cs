
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
        int i;

        for (int j = 0; j < 10; j++)
        {
            i = Config.productIndex;
            productsArr[i] = new Product();
            productsArr[i].ID = rn.Next(100000, 999999);
            productsArr[i].Name = productNames[i];
            productsArr[i].Category = productsCategories[i];
            productsArr[i].Price = rn.Next(100, 500);
            productsArr[i].InStock = rn.Next(0, 30);

            if (i == 0) productsArr[i].InStock = 0;
            Config.productIndex++;
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
        int i;

        for (int j = 0; j < 20; j++)
        {
            i = Config.orderIndex;
            ordersArr[i] = new Order();
            ordersArr[i].ID = Config.OrderLastId;
            ordersArr[i].CustomerName = customerNames[i % 10];
            ordersArr[i].CustomerEmail = customerEmails[i % 10];
            ordersArr[i].CustomerAdress = customerAdress[i % 10];
            ordersArr[i].OrderDate = DateTime.MinValue;
            TimeSpan tms = new TimeSpan(rn.Next(1, 10), rn.Next(0, 24), rn.Next(0, 60), rn.Next(0, 60));
            ordersArr[i].ShipDate = DateTime.MinValue + tms;
            tms.Add(new TimeSpan(rn.Next(1, 10), rn.Next(0, 24), rn.Next(0, 60), rn.Next(0, 60)));
            ordersArr[i].DeliveryDate = ordersArr[i].ShipDate + tms;
          
            Config.orderIndex++;
        }
    }


    /// <summary>
    /// Initialize the orderItems array. 
    /// </summary>
    private static void initializeOrderItems()
    {
        int i;
        for (int j= 0; j < 40; j++)
        {
            i = Config.orderItemIndex;
            orderItemsArr[i] = new OrderItem();
            orderItemsArr[i].ID = Config.OrderItemLastId;
            orderItemsArr[i].OrderId = ordersArr[i % 20].ID;
            orderItemsArr[i].ProductId = productsArr[i % 10].ID;
            orderItemsArr[i].Price = productsArr[i % 10].Price;
            orderItemsArr[i].Amount = rn.Next(1, 5);
           
            Config.orderItemIndex++;
            
        }
    }

    #endregion


   

}
