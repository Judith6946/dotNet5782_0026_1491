
using BO;
using BlApi;
using BlImplementation;
namespace BlTest;

internal class Program
{

    private static IBl bl = new BL();
    private static Cart cart = new Cart() { ItemsList = new List<OrderItem?>(), TotalPrice = 0 };

    #region ENUMS

    /// <summary>
    /// Main Menu
    /// </summary>
    private enum Menue { EXIT, PRODUCT, ORDER, CART };

    /// <summary>
    /// Product Menu
    /// </summary>
    private enum ProductMenu { GET_PRODUCTS, MANAGER_SEATCH, UPDATE, DELETE, ADD, CUSTOMER_SEARCH }

    /// <summary>
    /// Cart Menu
    /// </summary>
    private enum CartMenu { ADD_ITEM, UPDATE_AMOUNT, MAKE_ORDER }

    /// <summary>
    /// Order Menu.
    /// </summary>
    private enum OrderMenu { GET_ORDERS, GET_ORDER, UPDATE_ORDER_SHIPING, UPDATE_ORDER_DELIVERY, FOLLOW_ORDER,UPDATE_ORDER }

    #endregion



    static void Main(string[] args)
    {

        Menue menue;
        menue = getMenu();

        while (menue != Menue.EXIT)
        {
            try
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
                        cartMenu();
                        break;
                    default:
                        Console.WriteLine("No such menu, please try again.");
                        break;
                }

                menue = getMenu();

            }
            catch (Exception e)
            {
                string str = e.GetType().ToString() + ": " + e.Message;
                if (e.InnerException != null)
                    str += "\ninner exception - " + e.InnerException.GetType().ToString() + ": " + e.InnerException.Message;
                Console.WriteLine(str);
            }
        }

    }


    #region Menues

    /// <summary>
    /// Handle cart menu.
    /// </summary>
    private static void cartMenu()
    {
        CartMenu c = getCartChoice();

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


    /// <summary>
    /// Handle order menu.
    /// </summary>
    private static void orderMenu()
    {
        OrderMenu c = GetOrdertChoice();

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
            case OrderMenu.UPDATE_ORDER:
                UpdateOrder();
                break;
            default:
                break;
        }

    }


    /// <summary>
    /// Handle product menu.
    /// </summary>
    private static void productMenu()
    {
        ProductMenu c = getProductChoice();

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


    #endregion



    #region Order CRUD

    /// <summary>
    /// Deliver an order.
    /// </summary>
    /// <exception cref="InvalidInputException">Thrown when order id is not valid.</exception>
    private static void UpdateOrderDelivery()
    {
        Console.WriteLine("Insert id:");
        string s = Console.ReadLine();
        int id;
        if (int.TryParse(s, out id))
        {
            bl.Order.UpdateOrderDelivery(id);
            Console.WriteLine("Updating succeeded");
        }
        else
            throw new InvalidInputException("Id was not valid");
    }

    /// <summary>
    /// Follow an order- print order state.
    /// </summary>
    /// <exception cref="InvalidInputException">Thrown when order id is not valid.</exception>
    private static void FollowOrder()
    {
        Console.WriteLine("Insert id:");
        string s = Console.ReadLine();
        int id;
        if (int.TryParse(s, out id))
        {
            OrderTracking tracking = bl.Order.FollowOrder(id);
            Console.WriteLine(tracking);
        }
        else
            throw new InvalidInputException("Id was not valid.");
    }

    /// <summary>
    /// Ship an order.
    /// </summary>
    /// <exception cref="InvalidInputException">Thrown when order id is not valid.</exception>
    private static void UpdateOrderShipping()
    {
        Console.WriteLine("Insert id:");
        string s = Console.ReadLine();
        int id;
        if (int.TryParse(s, out id))
        {
            bl.Order.UpdateOrderShipping(id);
            Console.WriteLine("Updating succeeded");
        }
        else
            throw new InvalidInputException("Id was not valid.");
    }

    /// <summary>
    /// Get an order - print order details.
    /// </summary>
    /// <exception cref="InvalidInputException">Thrown when order id is not valid.</exception>
    private static void GetOrder()
    {
        Console.WriteLine("Insert id:");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id))
            throw new InvalidInputException("Id was not valid.");
        Console.WriteLine(bl.Order.GetOrder(id));
    }

    /// <summary>
    /// Print all orders.
    /// </summary>
    private static void GetOrders()
    {
        foreach (var item in bl.Order.GetOrders())
        {
            Console.WriteLine(item);
        }
    }

    private static void UpdateOrder()
    {
        int id, productId, amount;
        string strId, strPID, strAmount;
        Console.WriteLine("insert id:");
        strId = Console.ReadLine();
        Console.WriteLine("insert product id");
        strPID = Console.ReadLine();
        Console.WriteLine("insert amount");
        strAmount = Console.ReadLine();

        if (int.TryParse(strId, out id) && int.TryParse(strPID, out productId) && int.TryParse(strAmount, out amount))
        {
            bl.Order.UpdateOrder(id, productId, amount);
            Console.WriteLine("updating succeeded");
        }
        else
            throw new InvalidInputException("Id and amount must be numbers");
    }

    #endregion



    #region Cart CRUD

    /// <summary>
    /// Make an order from cart.
    /// </summary>
    private static void makeOrder()
    {
        Console.WriteLine("Insert Your Email:");
        string email = Console.ReadLine();
        Console.WriteLine("Insert Your Name:");
        string name = Console.ReadLine();
        Console.WriteLine("Insert Your Adress:");
        string adress = Console.ReadLine();
        cart.CustomerName = name;
        cart.CustomerAdress = adress;
        cart.CustomerEmail = email;
        bl.Cart.MakeOrder(cart);
        cart = new Cart() { ItemsList = new List<OrderItem>(), TotalPrice = 0 };
    }

    /// <summary>
    /// Update amount of product on cart.
    /// </summary>
    /// <exception cref="InvalidInputException">Thrown when input was invalid.</exception>
    private static void updateAmountOnCart()
    {
        Console.WriteLine("Insert product id:");
        string s1 = Console.ReadLine();
        Console.WriteLine("Insert new Amount:");
        string s2 = Console.ReadLine();
        int id, amount;
        if (int.TryParse(s1, out id) && int.TryParse(s2, out amount))
        {
            cart = bl.Cart.UpdateAmount(cart, amount, id);
            Console.WriteLine("Updating succeeded");
        }
        else
            throw new InvalidInputException("id/amount is invalid.");
    }

    /// <summary>
    /// Add product to cart.
    /// </summary>
    /// <exception cref="InvalidInputException">Thrown when id was invalid.</exception>
    private static void addItemForCart()
    {
        Console.WriteLine("Insert product id:");
        string s = Console.ReadLine();
        int id;
        if (int.TryParse(s, out id))
        {
            cart = bl.Cart.AddItem(cart, id);
            Console.WriteLine("Adding succeeded");
        }
        else
            throw new InvalidInputException("id/amount is invalid.");
    }

    #endregion


    #region Product CRUD

    /// <summary>
    /// Get a product from user.
    /// </summary>
    /// <param name="id">Id of new product.</param>
    /// <returns>New product.</returns>
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
    /// Update a product.
    /// </summary>
    /// <exception cref="InvalidInputException">Thrown when id was invalid.</exception> 
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
        else
            throw new InvalidInputException("id/amount is invalid.");
    }

    /// <summary>
    /// Search a product (for manager)
    /// </summary>
    /// <exception cref="InvalidInputException">Thrown when id was invalid.</exception>
    private static void productManagerSearch()
    {
        Console.WriteLine("Insert id:");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id))
            throw new InvalidInputException("Id was not valid");
        Console.WriteLine(bl.Product.GetProduct(id));
    }

    /// <summary>
    /// Print all products
    /// </summary>
    private static void getAllProducts()
    {
        foreach (var item in bl.Product.GetProducts())
        {
            Console.WriteLine(item);
        }
    }

    /// <summary>
    /// Search a product (for customer)
    /// </summary>
    /// <exception cref="InvalidInputException">Thrown when id was invalid.</exception>
    private static void productCustomerSearch()
    {
        Console.WriteLine("Insert id:");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id))
            throw new InvalidInputException("Id was not valid");
        Console.WriteLine(bl.Product.GetProductItem(id, cart));
    }

    /// <summary>
    /// Add a product
    /// </summary>
    /// <exception cref="InvalidInputException">Thrown when id was invalid.</exception>
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
        else
            throw new InvalidInputException("Id was invalid.");
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    /// <exception cref="InvalidInputException">Thrown when id was invalid.</exception>
    private static void deleteProduct()
    {
        Console.WriteLine("please enter product id:");
        int id;
        if (int.TryParse(Console.ReadLine(), out id))
        {
            bl.Product.DeleteProduct(id);
            Console.WriteLine("Deleting succeeded");
        }
        else
            throw new InvalidInputException("Id was invalid.");
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
    /// Get an order action chosen by user.
    /// </summary>
    /// <returns>The chosen action.</returns>
    private static OrderMenu GetOrdertChoice()
    {
        Console.WriteLine("\n press 0 to get all the orders, for manager.\n press 1 to get an order.\n press 2 to update ship date of order. \n press 3 to update a delivery date of order. \n press 4 to follow an order.\n press 5 to update order.\n");
        OrderMenu c = (OrderMenu)Enum.Parse(typeof(OrderMenu), Console.ReadLine());
        return c;
    }

    /// <summary>
    /// Get a product-menu option chosen by user.
    /// </summary>
    /// <returns>The chosen option.</returns>
    private static ProductMenu getProductChoice()
    {
        Console.WriteLine("\n press 0 to get all products. \n press 1 to search product by id, for manager.\n press 2 to update a product.\n press 3 to delete a product. \n press 4 to add a product.\n press 5 to search a product for customer\n");
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