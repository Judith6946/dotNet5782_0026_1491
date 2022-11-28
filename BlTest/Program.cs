

using BO;
using BlApi;
using BlImplementation;
using DalApi;

namespace BlTest;


internal class Program
{
    private static IBl bl = new BL();

    private static List<Cart> carts = new List<Cart>();

    #region ENUMS
    private enum Menue { EXIT, PRODUCT, ORDER, CART };

    private enum ProductMenu { GET_PRODUCTS,MANAGER_SEATCH, UPDATE,DELETE,ADD,CUSTOMER_SEARCH}

    private enum CartMenu { ADD_ITEM,UPDATE_AMOUNT,MAKE_ORDER }

    #endregion

    private enum OrderMenu { GET_ORDERS, GET_ORDER, UPDATE_ORDER_SHIPING, UPDATE_ORDER_DELIVERY, FOLLOW_ORDER}

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
                        OrderMenu();
                        break;
                    case Menue.CART:
                        cartMenu();
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


    #region Menues

    private static void cartMenu()
    {
        CartMenu c = getCartChoice();
        try
        {
            switch (c)
            {
                case CartMenu.ADD_ITEM:
                    addItemForCart();
                    break;
                case CartMenu.UPDATE_AMOUNT:
                    updateAmountOnCart();
                    break;
                case CartMenu.MAKE_ORDER:
                    makeOrder();
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    private static OrderMenu GetOrdertChoice()
    {
        Console.WriteLine("\n press 0 to get all products. \n press 1 to get all the orders, for manager.\n press 2 to get a order.\n press 3 to update ship date of order. \n press 4 to update a delivery date of order. \n press 5 to follow an order.\n ");
        OrderMenu c = (OrderMenu)Enum.Parse(typeof(OrderMenu), Console.ReadLine());
        return c;
    }

    private static void orderMenu()
    {
        OrderMenu c = GetOrdertChoice();
        try
        {
            switch (c)
            {
                case OrderMenu.GET_ORDERS:
                    GetOrders();
                    break;
                case OrderMenu.GET_ORDER:
                    GetOrder();
                    break;
                case OrderMenu.UPDATE_ORDER_SHIPING:
                    UpdateOrderShipping();
                    break;
                case OrderMenu.UPDATE_ORDER_DELIVERY:
                    UpdateOrderDelivery();
                    break;
                case OrderMenu.FOLLOW_ORDER:
                    FollowOrder();
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

    private static void FollowOrder()
    {
        throw new NotImplementedException();
    }

   

    private static void orderMenu()
    private static void UpdateOrderDelivery()
    {
        throw new NotImplementedException();
    }

    private static void UpdateOrderShipping()
    {
        Console.WriteLine("Insert id:");
        string s = Console.ReadLine();
        int id;
        if (int.TryParse(s, out id))
        {
            Console.WriteLine(bl.Order.GetOrder(id));

            BO.Order.UpdateOrderShipping(o);
            Console.WriteLine("Updating succeeded");
        }
    }

    private static void GetOrder()
    {
        Console.WriteLine("Insert id:");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id))
            throw new Exception();
        Console.WriteLine(bl.Order.GetOrder(id));
    }

    private static void GetOrders()
    {
        foreach (var item in bl.Order.GetOrders())
        {
            Console.WriteLine(item);
        }
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
                    deleteProduct();
                    break;
                case ProductMenu.ADD:
                    addProduct();
                    break;
                case ProductMenu.CUSTOMER_SEARCH:
                    productCustomerSearch();
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


    #region Cart CRUD

    private static void makeOrder()
    {
        Cart cart = getCart();
        Console.WriteLine("Insert Your Name:");
        string name = Console.ReadLine();
        Console.WriteLine("Insert Your Adress:");
        string adress = Console.ReadLine();
        cart.CustomerName = name;
        cart.CustomerAdress = adress;
        bl.Cart.MakeOrder(cart);
    }

    private static void updateAmountOnCart()
    {
        Cart cart = getCart();
        Console.WriteLine("Insert product id:");
        string s1 = Console.ReadLine();
        Console.WriteLine("Insert new Amount:");
        string s2 = Console.ReadLine();
        int id,amount;
        if (int.TryParse(s1, out id)&& int.TryParse(s2, out amount))
        {
            cart=bl.Cart.UpdateAmount(cart,amount,id);
            updateCart(cart);
            Console.WriteLine("Updating succeeded");
        }
    }

    private static Cart getCart()
    {
        Console.WriteLine("Insert your email:");
        string email = Console.ReadLine();
        Cart cart = carts.FirstOrDefault(x => x.CustomerEmail == email, null);
        if (cart == null)
        {
            cart = new Cart() { CustomerEmail = email, ItemsList = new List<OrderItem>(), TotalPrice = 0 };
            carts.Add(cart);
        }
        return cart;
    }

    private static void updateCart(Cart cart)
    {
        int index = carts.FindIndex(x=>x.CustomerEmail==cart.CustomerEmail);

        if (index != -1)
            carts[index] = cart;
    }

    private static void addItemForCart()
    {
        Cart cart = getCart();
        Console.WriteLine(cart);
        Console.WriteLine("Insert product id:");
        string s = Console.ReadLine();
        int id;
        if (int.TryParse(s, out id))
        {
            cart=bl.Cart.AddItem(cart,id);
            updateCart(cart);
            Console.WriteLine("Adding succeeded");
        }
    }

    #endregion


    #region Product CRUD
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

    private static void updateProduct()
    {
        Console.WriteLine("please enter product id:");
        string s = Console.ReadLine();
        int id;
        if (int.TryParse(s, out id))
        {
            Console.WriteLine(bl.Product.GetProduct(id));
            Product p = getProductFromUser(id);
            bl.Product.UpdateProduct(p);
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


    private static void productCustomerSearch()
    {
        //which cart???
    }

    private static void addProduct()
    {
        Console.WriteLine("please enter product id:");
        string s = Console.ReadLine();
        int id;
        if (int.TryParse(s, out id))
        {
            Product p = getProductFromUser(id);
            bl.Product.AddProduct(p);
            Console.WriteLine("Adding succeeded");
        }
    }

    private static void deleteProduct()
    {
        Console.WriteLine("please enter product id:");
        int id;
        if (int.TryParse(Console.ReadLine(), out id))
        {
            bl.Product.DeleteProduct(id);
            Console.WriteLine("Deleting succeeded");
        }
    }

    #endregion


    #region User Choices

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

    /// <summary>
    /// Get a cart-menu option chosen by user.
    /// </summary>
    /// <returns>The chosen option.</returns>
    private static CartMenu getCartChoice()
    {
        Console.WriteLine("\n press 0 to add item to your cart. \n press 1 to update amount for a product.\n press 2 to make an order.\n");
        CartMenu c = (CartMenu)Enum.Parse(typeof(CartMenu), Console.ReadLine());
        return c;
    }

    #endregion


}