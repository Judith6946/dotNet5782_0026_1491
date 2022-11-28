


using BlApi;
using BlImplementation;
using DalApi;

namespace BlTest;


internal class Program
{
    static IBl bl = new BL();
    private enum Menue { EXIT, PRODUCT, ORDER, CART };

    private enum ProductMenu { GET_PRODUCTS,MANAGER_SEATCH, UPDATE,DELETE,ADD,CUSTOMER_SEARCH}

    static void Main(string[] args)
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
                    case Menue.CART:
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

    private static void orderItemMenu()
    {
        throw new NotImplementedException();
    }

    private static void orderMenu()
    {
        throw new NotImplementedException();
    }

    private static void productMenu()
    {
        ProductMenu c = getProductChoice();
        try
        {
            switch (c)
            {
                case ProductMenu.GET_PRODUCTS:
                    getAllProducts();
                    break;
                case ProductMenu.MANAGER_SEATCH:
                    productManagerSearch();
                    break;
                case ProductMenu.UPDATE:
                    updateProduct();
                    break;
                case ProductMenu.DELETE:
                    break;
                case ProductMenu.ADD:
                    break;
                case ProductMenu.CUSTOMER_SEARCH:
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

    private static BO.Product getProductFromUser(int id)
    {
        BO.Product p = new BO.Product();
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

    private static void productManagerSearch()
    {
        Console.WriteLine("Insert id:");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id))
            throw new Exception();
        Console.WriteLine(bl.Product.GetProduct(id));
    }

    private static void getAllProducts()
    {
        foreach (var item in bl.Product.GetProducts())
        {
            Console.WriteLine(item);
        }
    }


    /// <summary>
    /// Get a menu option chosen by user.
    /// </summary>
    /// <returns>The chosen menu.</returns>
    private static Menue getMenu()
    {
        Console.WriteLine("Choose a menu: \n press 1 to the product menu. \n press 2 to the order menu. \n press 3 to the cart menu. \n press 0 to exit.");
        Menue menue = (Menue)Enum.Parse(typeof(Menue), Console.ReadLine());
        return menue;
    }

    /// <summary>
    /// Get a product-menu option chosen by user.
    /// </summary>
    /// <returns>The chosen option.</returns>
    private static ProductMenu getProductChoice()
    {
        Console.WriteLine("\n press 0 to get all products. \n press 1 to search product by id, for manager.\n press 2 to update a product.\n press 3 to delete a product. \n press 4 to add a product.\n \n press 5 to search a product for customer\n");
        ProductMenu c = (ProductMenu)Enum.Parse(typeof(ProductMenu), Console.ReadLine());
        return c;
    }
}