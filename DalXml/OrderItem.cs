
using DalApi;
using DO;
namespace Dal;

internal class OrderItem : IOrderItem
{
    const string s_orderItems = @"OrderItems"; //XML Serializer

    public int Add(DO.OrderItem entity)
    {
        List<DO.OrderItem?> listItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

        entity.ID = XMLTools.getNextOrderItemID();

        listItems.Add(entity);

        XMLTools.SaveListToXMLSerializer(listItems, s_orderItems);

        return entity.ID;
    }

    public void Delete(int id)
    {
        List<DO.OrderItem?> listItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

        if (listItems.RemoveAll(item => item?.ID == id) == 0)
            throw new NotFoundException("missing id");

        XMLTools.SaveListToXMLSerializer(listItems, s_orderItems);
    }

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? predicate = null)
    {

        List<DO.OrderItem?> listItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

        if (predicate == null)
            return listItems.Select(item => item).OrderBy(item => item?.ID);
        else
            return listItems.Where(predicate).OrderBy(item => item?.ID);
    }

    public DO.OrderItem? getByCondition(Func<DO.OrderItem?, bool>? predicate)
    {
        List<DO.OrderItem?> listItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

        return listItems.FirstOrDefault(predicate ??
            throw new InvalidInputException("condition cannot be null"), null) ??
            throw new NotFoundException("cannot find this order item.");
    }

    public DO.OrderItem GetById(int id)
    {
        List<DO.OrderItem?> listItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

        return listItems.FirstOrDefault(item => item?.ID == id) ??
            throw new DO.NotFoundException("missing id");
    }

    public IEnumerable<DO.OrderItem?> GetByOrder(int orderId)
    {
        List<DO.OrderItem?> listItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        return from item in listItems
               where item?.OrderId == orderId
               select item;
    }

    public DO.OrderItem GetByOrderAndProduct(int orderId, int productId)
    {
        List<DO.OrderItem?> listItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        return (from item in listItems
               where item?.OrderId == orderId&&item?.ProductId == productId
               select item).FirstOrDefault()
               ??throw new NotFoundException("couldnt find such product in this order");
    }

    public void Update(DO.OrderItem entity)
    {
        List<DO.OrderItem?> listItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

        if (listItems.RemoveAll(item => item?.ID == entity.ID) == 0)
            throw new NotFoundException("missing id");
        listItems.Add(entity);

        XMLTools.SaveListToXMLSerializer(listItems, s_orderItems);
    }
}
