
using DO;
using System.ComponentModel;

namespace Dal;

internal static class DataSource
{

    internal static class Config
    {
        internal static int orderIndex = 0;
        internal static int orderItemIndex = 0;
        internal static int productIndex = 0;

        private static int orderLastId = 0;
        private static int orderItemLastId = 0;


        public static int OrderLastId { get {  return ++orderLastId; } }

        public static int OrderItemLastId { get { return ++orderItemLastId; } }

    }

    private static readonly Random rn = new Random();

    internal static Product[] productsArr = new Product[50];
    internal static Order[] ordersArr = new Order[100];
    internal static OrderItem[] orderItemsArr = new OrderItem[200];

    static DataSource()
    {
        s_Initialize();
    }

    private static void s_Initialize()
    {
        addProducts();
        addOrders();
        addOrderItems();
    }

    private static void addProducts()
    {
        string[] productNames = new string[10]
        {"great ring", "golden necklace", "rose bracelet", "goldfild bracelet",
        "small silver earrings", "kids earrings", "personal design silver ring",
        "Wedding ring", "Long decorated earring", "Personalized designer necklace" };

        Enums.Category[] productsCategories = new Enums.Category[10]
        {Enums.Category.ring,Enums.Category.necklace,Enums.Category.bracelet,Enums.Category.bracelet,
        Enums.Category.earrings,Enums.Category.earrings,Enums.Category.ring,Enums.Category.ring,
        Enums.Category.earrings,Enums.Category.necklace};

        for (int i = 0; i < 10; i++)
        {
            productsArr[i] = new Product();
            productsArr[i].ID = rn.Next(100000, 999999);
            productsArr[i].Name = productNames[i];
            productsArr[i].Category = productsCategories[i];
            productsArr[i].Price = rn.Next(100, 500);
            productsArr[i].InStock = rn.Next(0, 30);

            if (i == 0) productsArr[i].InStock = 0;
        }

        
    }

    private static void addOrders()
    {
        string[] customerNames = new string[10] { "Yehudit", "Chaya", "Sara", "David", "Moshe", "Tovi", "Adina", "Avi", "Dan", "Miri" };
        string[] customerEmails = new string[10] { "yt9074547@gmail.com", "Chaya9@gmail.com", "Sara@gmail.com", "David@gmail.com", "Moshe@gmail.com", "Tovi@gmail.com", "Adina@gmail.com", "Avi@gmail.com", "Dan@gmail.com", "Miri@gmail.com" };
        string[] customerAdress = new string[10] { "Rdak 4 Elad", "Haperach 5 Petach Tikva", "Rdak 4 Tel Aviv", "Dror 4 Bnei Brak", "Hatamar 67 Beit Shemesh", "Rdak 5 Elad", "Hateena 2 Elad", "Givataaim", "Derech Hashalom 11 Tel Aviv", "Kalanit 10 Eilat" };

        for (int i = 0; i < 20; i++)
        {
            ordersArr[i].ID = Config.OrderLastId; 
            ordersArr[i].CustomerName = customerNames[i % 10];
            ordersArr[i].CustomerEmail = customerEmails[i % 10];
            ordersArr[i].CustomerAdress = customerAdress[i % 10];
            ordersArr[i].OrderDate = DateTime.MinValue;
            TimeSpan tms = new TimeSpan(rn.Next(1, 10), rn.Next(0, 24), rn.Next(0, 60), rn.Next(0, 60));
            ordersArr[i].ShipDate = DateTime.MinValue + tms;
            tms.Add(new TimeSpan(rn.Next(1, 10), rn.Next(0, 24), rn.Next(0, 60), rn.Next(0, 60)));
            ordersArr[i].DeliveryDate = ordersArr[i].ShipDate + tms;
        }
    }

    private static void addOrderItems()
    {
        for (int i = 0; i < 40; i++)
        {
            orderItemsArr[i] = new OrderItem();
            orderItemsArr[i].ID=Config.OrderItemLastId;
            orderItemsArr[i].OrderId = ordersArr[i % 20].ID;
            orderItemsArr[i].ProductId = productsArr[i % 10].ID;
            orderItemsArr[i].Price = productsArr[i % 10].Price;
            orderItemsArr[i].Amount = rn.Next(1, 5);

        }
    }

}
