

using System.Xml.Serialization;

internal class Program
{
    static DalApi.IDal dal = DalApi.Factory.Get();

    //entities lists
    internal static List<DO.Product?> productsList = dal.Product.GetAll().ToList();
    internal static List<DO.Order?> ordersList = dal.Order.GetAll().ToList();
    internal static List<DO.OrderItem?> orderItemsList = dal.OrderItem.GetAll().ToList();

    private static void Main(string[] args)
    {
        SaveListToXMLSerializer(productsList, "products");
        SaveListToXMLSerializer(ordersList, "orders");
        SaveListToXMLSerializer(orderItemsList, "orderItems");
    }

    public static void SaveListToXMLSerializer<T>(List<T?> list, string entity) where T : struct
    {
        string filePath = $"../../../xml/{entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            //using XmlWriter writer = XmlWriter.Create(file, new XmlWriterSettings() { Indent = true });

            XmlSerializer serializer = new(typeof(List<T?>));
            //if (s_writing)
            //    serializer.Serialize(writer, list);
            //else
            serializer.Serialize(file, list);
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex); 
            throw new Exception($"fail to create xml file: {filePath}", ex);
        }
    }


}