


namespace BlTest;


internal class Program
{
    private enum Menue { EXIT, PRODUCT, ORDER, CART };

    private enum ProductMenu { GET_PRODUCTS,SEATCH, }

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
        throw new NotImplementedException();
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
}