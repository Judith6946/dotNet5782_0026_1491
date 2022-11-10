using DO;

namespace Dal;

public class DataOrderItem
{

    /// <summary>
    /// Add item to order. 
    /// </summary>
    /// <param name="o">item to be added to the order</param>
    /// <returns>Id of the new product.</returns>
    public int Add(OrderItem o)
    {
        //Check whether the id does not already exist.
        for (int i = 0; i < DataSource.Config.orderItemIndex; i++)
        {
            if (DataSource.orderItemsArr[i].ID == o.ID)
                throw new Exception("OrderItem id is aready exist.");
        }

        //Adding product.
        DataSource.orderItemsArr[DataSource.Config.orderItemIndex] = o;
        DataSource.Config.orderItemIndex++;
        return o.ID;
    }
    /// <summary>
    /// Get a item by its id. 
    /// </summary>
    /// <param name="id">Id of item .</param>
    /// <returns>OrderItem object.</returns>
    /// <exception cref="Exception">Thrown when the OrderItem cant be found.</exception>
    public OrderItem Get(int id)
    {
        for (int i = 0; i < DataSource.Config.orderItemIndex; i++)
        {
            if (DataSource.orderItemsArr[i].ID == id)
                return DataSource.orderItemsArr[i];
        }
        throw new Exception("Cannot find this OrderItem.");
    }


    /// <summary>
    /// Get all of products. 
    /// </summary>
    /// <returns>Products array.</returns>
    public OrderItem[] GetAll()
    {
        int size = DataSource.Config.orderItemIndex;
        OrderItem[] orderItems = new OrderItem[size];
        Array.Copy(DataSource.orderItemsArr, orderItems, size);
        return orderItems;
    }

    /// <summary>
    /// Delete a OrderItem by its id.
    /// </summary>
    /// <param name="id">Id of OrderItem to be deleted</param>
    public void Delete(int id)
    {

        int i = 0;
        bool found = false;

        //Search OrderItem.
        for (; i < DataSource.Config.orderItemIndex && !found; i++)
        {
            if (DataSource.orderItemsArr[i].ID == id)
                found = true;
        }

        //Move next OrderItem.
        for (; i < DataSource.Config.orderItemIndex && found; i++)
        {
            DataSource.orderItemsArr[i - 1] = DataSource.orderItemsArr[i];
        }

        if (found)
            DataSource.Config.orderItemIndex--;
        else
            throw new Exception("Cannot find this OrderItem.");
    }

    /// <summary>
    /// Update a orderItem.
    /// </summary>
    /// <param name="p">Updated orderItem.</param>
    /// <exception cref="Exception">Thrown when orderItem cant be found.</exception>
    public void Update(OrderItem o)
    {
        bool found = false;
        for (int i = 0; i < DataSource.Config.orderItemIndex; i++)
        {
            if (DataSource.orderItemsArr[i].ID == o.ID)
            {
                found = true;
                DataSource.orderItemsArr[i] = o;
            }
        }

        if (!found)
            throw new Exception("cannot find this OrderItem");
    }
}
