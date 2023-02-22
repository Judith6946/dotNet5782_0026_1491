
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;


internal static class XMLTools
{

    const string s_dir = @"..\xml\";

    static XMLTools()
    {
        if (!Directory.Exists(s_dir))
            Directory.CreateDirectory(s_dir);
    }

    #region Extension Fuctions
    public static T? ToEnumNullable<T>(this XElement element, string name) where T : struct, Enum =>
        Enum.TryParse<T>((string?)element.Element(name), out var result) ? (T?)result : null;

    public static DateTime? ToDateTimeNullable(this XElement element, string name) =>
        DateTime.TryParse((string?)element.Element(name), out var result) ? (DateTime?)result : null;

    public static double? ToDoubleNullable(this XElement element, string name) =>
        double.TryParse((string?)element.Element(name), out var result) ? (double?)result : null;

    public static int? ToIntNullable(this XElement element, string name) =>
        int.TryParse((string?)element.Element(name), out var result) ? (int?)result : null;
    #endregion

    #region SaveLoadWithXElement

    /// <summary>
    /// Save XElement on xml file
    /// </summary>
    /// <param name="rootElem">XElement to be saved.</param>
    /// <param name="entity">Name of the xml file.</param>
    /// <exception cref="DO.XMLFileLoadCreateException">Thrown when file could not be saved</exception>
    public static void SaveListToXMLElement(XElement rootElem, string entity)
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            rootElem.Save(filePath);
        }
        catch (Exception ex)
        {
            throw new DO.XMLFileLoadCreateException($"fail to create xml file: {filePath}", ex);
        }
    }

    /// <summary>
    /// Get XElement from xml file.
    /// </summary>
    /// <param name="entity">File to be loaded.</param>
    /// <returns></returns>
    /// <exception cref="DO.XMLFileLoadCreateException">Thrown when file could not be loaded</exception>
    public static XElement LoadListFromXMLElement(string entity)
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            if (File.Exists(filePath))
                return XElement.Load(filePath);
            XElement rootElem = new(entity);
            rootElem.Save(filePath);
            return rootElem;
        }
        catch (Exception ex)
        {
            throw new DO.XMLFileLoadCreateException($"fail to load xml file: {filePath}", ex);
        }
    }
    #endregion

    #region SaveLoadWithXMLSerializer

    /// <summary>
    /// Save list on xml files using Serialize.
    /// </summary>
    /// <typeparam name="T">List type</typeparam>
    /// <param name="list">List to be saved</param>
    /// <param name="entity">File name</param>
    /// <exception cref="DO.XMLFileLoadCreateException">Thrown when file could not be saved.</exception>
    public static void SaveListToXMLSerializer<T>(List<T?> list, string entity) where T : struct
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);

            XmlSerializer serializer = new(typeof(List<T?>));
            serializer.Serialize(file, list);
        }
        catch (Exception ex)
        {
            throw new DO.XMLFileLoadCreateException($"fail to create xml file: {filePath}", ex);
        }
    }

    /// <summary>
    /// Get list from xml files.
    /// </summary>
    /// <typeparam name="T">List type.</typeparam>
    /// <param name="entity">File name to be loaded.</param>
    /// <returns>List of T objects from the xml files.</returns>
    /// <exception cref="DO.XMLFileLoadCreateException">Thrown when file could not be loaded.</exception>
    public static List<T?> LoadListFromXMLSerializer<T>(string entity) where T : struct
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            if (!File.Exists(filePath)) return new();
            using FileStream file = new(filePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T?>));
            return x.Deserialize(file) as List<T?> ?? new();
        }
        catch (Exception ex)
        {
            throw new DO.XMLFileLoadCreateException($"fail to load xml file: {filePath}", ex);
        }
    }

    #endregion

    #region ID config
    public static int getNextOrderID() => getNextID("OrderNextID");
    public static int getNextOrderItemID() => getNextID("OrderItemNextID");

    /// <summary>
    /// Get the next ID of element.
    /// </summary>
    /// <param name="element">Name of element.</param>
    /// <returns></returns>
    /// <exception cref="DO.XMLFileLoadCreateException">Thrown when xml-config file could not be loaded.</exception>
    /// <exception cref="DO.DalXmlFormatException">Thrown when xml format was invalid</exception>
    private static int getNextID(string element)
    {
        string filePath = $"{s_dir}xml-config.xml";

        if (!File.Exists(filePath))
            throw new DO.XMLFileLoadCreateException($"fail to load xml file: {filePath}");


        XElement rootElem = XElement.Load(filePath);
        XElement elem = rootElem.Element(element) ?? throw new DO.DalXmlFormatException($"could not find {element} element on {filePath}");
        int id;
        if (!int.TryParse(elem.Value, out id))
            throw new DO.DalXmlFormatException($"{element} value was invalid");
        elem.Value = (id + 1).ToString();

        try
        {
            rootElem.Save(filePath);
        }
        catch(Exception ex)
        {
            throw new DO.XMLFileLoadCreateException($"fail to save xml file: {filePath}",ex);
        }

        return id;
    }

    #endregion


}
