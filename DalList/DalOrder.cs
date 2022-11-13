using DO;

namespace Dal;


/// <summary>
/// Access order data. 
/// </summary>
public class DalOrder
{

    /// <summary>
    /// Add an order to the order array.
    /// </summary>
    /// <param name="o">Order object to be added.</param>
    /// <returns>New order id.</returns>
    /// <exception cref="Exception">Thrown when the order array is full.</exception>
    public int Add(Order o)
    {

        //check if the array is not full.
        if (DataSource.Config.orderIndex >= DataSource.ordersArr.Length)
            throw new Exception("No place for the new order.");

        //Adding order.
        o.ID = DataSource.Config.OrderLastId;
        DataSource.ordersArr[DataSource.Config.orderIndex] = o;
        DataSource.Config.orderIndex++;
        return o.ID;
    }


    /// <summary>
    /// Get a order by its id. 
    /// </summary>
    /// <param name="id">Id of order.</param>
    /// <returns>Order object.</returns>
    /// <exception cref="Exception">Thrown when the order cant be found.</exception>
    public Order Get(int id)
    {
        for (int i = 0; i < DataSource.Config.orderIndex; i++)
        {
            if (DataSource.ordersArr[i].ID == id)
                return DataSource.ordersArr[i];
        }
        throw new Exception("Cannot find this order.");
    }


    /// <summary>
    /// Get all of orders. 
    /// </summary>
    /// <returns>Orders array.</returns>
    public Order[] GetAll()
    {
        int size = DataSource.Config.orderIndex;
        Order[] orders = new Order[size];
        Array.Copy(DataSource.ordersArr, orders, size);
        return orders;
    }


    /// <summary>
    /// Delete a order by its id.
    /// </summary>
    /// <param name="id">Id of order to be deleted</param>
    public void Delete(int id)
    {

        int i = 0;
        bool found = false;

        //Search order.
        for (; i < DataSource.Config.orderIndex && !found; i++)
        {
            if (DataSource.ordersArr[i].ID == id)
                found = true;
        }

        //Move next orders.
        for (; i < DataSource.Config.orderItemIndex && found; i++)
        {
            DataSource.ordersArr[i - 1] = DataSource.ordersArr[i];
        }

        if (found)
            DataSource.Config.orderIndex--;
        else
            throw new Exception("Cannot find this product.");
    }


    /// <summary>
    /// Update a order.
    /// </summary>
    /// <param name="o">Updated order.</param>
    /// <exception cref="Exception">Thrown when order cant be found.</exception>
    public void Update(Order o)
    {
        bool found = false;
        for (int i = 0; i < DataSource.Config.orderIndex; i++)
        {
            if (DataSource.ordersArr[i].ID == o.ID)
            {
                found = true;
                DataSource.ordersArr[i] = o;
            }
        }

        if (!found)
            throw new Exception("cannot find this order");
    }


}
