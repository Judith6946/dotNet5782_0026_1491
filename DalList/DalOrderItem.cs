using DO;

namespace Dal;

/// <summary>
/// Access order item data. 
/// </summary>
public class DalOrderItem
{

   
    /// <summary>
    /// Add item to order. 
    /// </summary>
    /// <param name="o">item to be added to the order</param>
    /// <returns>Id of the new product.</returns>
    public int Add(OrderItem o)
    {
        //check if the array is not full.
        if (DataSource.Config.orderItemIndex >= DataSource.orderItemsArr.Length)
            throw new Exception("No place for the new order item.");

        //Adding product.
        o.ID = DataSource.Config.OrderItemLastId;
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
    public OrderItem GetById(int id)
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



    /// <summary>
    /// Search an order item by order id and product id.
    /// </summary>
    /// <param name="orderId">Id of order</param>
    /// <param name="productId">Id of product</param>
    /// <returns>The found order item object.</returns>
    /// <exception cref="Exception">Thrown when no such item was found.</exception>
    public OrderItem GetByOrderAndProduct(int orderId, int productId)
    {
        for (int i = 0; i < DataSource.Config.orderItemIndex; i++)
        {
            if (DataSource.orderItemsArr[i].ProductId == productId && DataSource.orderItemsArr[i].OrderId == orderId)
            {
                return DataSource.orderItemsArr[i];
            }
        }

        throw new Exception("cannot find this OrderItem");
    }



    /// <summary>
    /// Get all items on a given order. 
    /// </summary>
    /// <param name="orderId">The order id.</param>
    /// <returns>All order items of the given order.</returns>
    /// /// <exception cref="Exception">Thrown when the order has no items.</exception>
    public OrderItem[] GetByOrder(int orderId)
    {
        OrderItem[] arr=new OrderItem[DataSource.Config.orderItemIndex];
        int k = 0;

        for (int i = 0; i < DataSource.Config.orderItemIndex; i++)
        {
            if (DataSource.orderItemsArr[i].OrderId == orderId)
            {
                arr[k++] = DataSource.orderItemsArr[i]; 
            }
        }
        if (k == 0)
            throw new Exception("No items on this order.");
        OrderItem[] arr2 = new OrderItem[k];
        Array.Copy(arr, arr2, k);
        return arr2;
    }


}
