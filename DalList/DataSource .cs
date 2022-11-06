
using  DO;
using System.ComponentModel;

namespace Dal;

internal static class DataSource
{
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

        for(int i = 0; i < productsArr.Length; i++)
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

    }

    private static void addOrderItems()
    {

    }

}
