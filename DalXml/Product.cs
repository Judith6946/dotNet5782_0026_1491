
using DalApi;
using DO;
using System;
using System.Xml.Linq;

namespace Dal;

internal class Product : IProduct
{
    const string s_products = @"products"; //Linq to XML

    static DO.Product? createProductfromXElement(XElement s)
    {
        return new DO.Product()
        {
            ID = s.ToIntNullable("ID") ?? throw new DO.DalXmlFormatException("Data was invalid"),
            Name = (string?)s.Element("Name"),
            Category = s.ToEnumNullable<Enums.Category>("Category"),
            InStock = s.ToIntNullable("InStock") ?? throw new DO.DalXmlFormatException("Data was invalid"),
            Price = s.ToIntNullable("Price") ?? throw new DO.DalXmlFormatException("Data was invalid")
        };
    }

    public int Add(DO.Product entity)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);

        XElement? pr = (from p in productsRootElem.Elements()
                          where p.ToIntNullable("ID") == entity.ID 
                          select p).FirstOrDefault();
        if (pr != null)
            throw new AlreadyExistException("id already exist"); 

        XElement productElem = new XElement("Product",
                                   new XElement("ID", entity.ID),
                                   new XElement("Name", entity.Name),
                                   new XElement("Price", entity.Price),
                                   new XElement("InStock", entity.InStock),
                                   new XElement("Category", entity.Category)
                                   );

        productsRootElem.Add(productElem);

        XMLTools.SaveListToXMLElement(productsRootElem, s_products);

        return entity.ID; ;
    }

    public void Delete(int id)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);

        XElement? pr = (from p in productsRootElem.Elements()
                          where (int?)p.Element("ID") == id
                          select p).FirstOrDefault() ?? throw new NotFoundException("missing id"); 

        pr.Remove(); 

        XMLTools.SaveListToXMLElement(productsRootElem, s_products);
    }

    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? predicate = null)
    {
        XElement? productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        if (predicate != null)
            return productsRootElem.Elements().Select(s => createProductfromXElement(s)).Where(predicate);
        return productsRootElem.Elements().Select(s => createProductfromXElement(s));
    }

    public DO.Product? getByCondition(Func<DO.Product?, bool>? predicate)
    {
        XElement? productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        return productsRootElem.Elements().Select(s => createProductfromXElement(s))
            .FirstOrDefault(predicate ?? throw new InvalidInputException("predicate cannot be null"));
    }

    public DO.Product GetById(int id)
    {
        XElement? productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        return (from elem in productsRootElem.Elements()
               let p = createProductfromXElement(elem)
               where p?.ID == id
               select p).FirstOrDefault()??throw new NotFoundException("No such product.");       
    }

    public void Update(DO.Product entity)
    {
        Delete(entity.ID);
        Add(entity);
    }

}
