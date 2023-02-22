using DalApi;
using System;
using DO;
namespace Dal;

internal class Order : IOrder
{
    const string s_orders = @"orders"; //XML Serializer

    public int Add(DO.Order entity)
    {
        List<DO.Order?> listOrdres = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        entity.ID = XMLTools.getNextOrderID();
        listOrdres.Add(entity);

        XMLTools.SaveListToXMLSerializer(listOrdres, s_orders);

        return entity.ID;
    }

    public void Delete(int id)
    {
        List<DO.Order?> listOrdres = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (listOrdres.RemoveAll(order => order?.ID == id) == 0)
            throw new NotFoundException("missing id"); 

        XMLTools.SaveListToXMLSerializer(listOrdres, s_orders);
    }

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? predicate = null)
    {
        List<DO.Order?> listOrdres = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (predicate == null)
            return listOrdres.Select(order => order).OrderBy(order => order?.ID);
        else
            return listOrdres.Where(predicate).OrderBy(order => order?.ID);
    }

    public DO.Order? getByCondition(Func<DO.Order?, bool>? predicate)
    {
        List<DO.Order?> listOrdres = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        return listOrdres.FirstOrDefault(predicate ??
            throw new InvalidInputException("condition cannot be null"), null) ??
            throw new NotFoundException("cannot find this order.");
    }

    public DO.Order GetById(int id)
    {
        List<DO.Order?> listOrdres = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        return listOrdres.FirstOrDefault(order => order?.ID == id) ??
            throw new DO.NotFoundException("missing id");
    }

    public void Update(DO.Order entity)
    {
        List<DO.Order?> listOrdres = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (listOrdres.RemoveAll(order => order?.ID == entity.ID) == 0)
            throw new NotFoundException("missing id");
        listOrdres.Add(entity);

        XMLTools.SaveListToXMLSerializer(listOrdres, s_orders);
    }
}
