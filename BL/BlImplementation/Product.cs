
using Dal;
using DalApi;
using AutoMapper;
namespace BlImplementation;

/// <summary>
/// Implementation of product methods (BL layer)
/// </summary>
internal class Product : BlApi.IProduct
{
    private IDal Dal = new DalList();
    IMapper mapper=new MapperConfiguration(cfg=>cfg.AddProfile(new BoProfile())).CreateMapper();

    public void AddProduct(BO.Product product)
    {
        try
        {
            if (product.ID <= 0 || product.Name == "" || product.InStock < 0 || product.Price < 0)
                throw new Exception("JJJJ");
            Dal.Product.Add(new DO.Product()
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                InStock = product.InStock,
                Category = (DO.Enums.Category)(BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), product.Category.ToString())
            });
        }
        catch(Exception e)
        {
            throw new Exception("nnn");
        }
    }

    public void DeleteProduct(int id)
    {
        try
        {
            if (Dal.OrderItem.GetAll().Any(x => x.ProductId == id))
                throw new Exception("");

            Dal.Product.Delete(id);
        }
       
        catch(Exception e)
        {
            throw new Exception();
        }

    }

    public BO.Product GetProduct(int id)
    {
        try
        {
            if (id < 0)
                throw new Exception();
            DO.Product p = Dal.Product.GetById(id);
            return new BO.Product() { ID = p.ID, Name = p.Name, Price = p.Price, InStock = p.InStock, Category = (BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), p.Category.ToString()) };

        }
        catch (Exception e)
        {
            throw new Exception("Do something!!!!!!!!!!");
        }
    }

    public BO.ProductItem GetProductItem(int id, BO.Cart cart)
    {
        try 
        {
            if (id < 0)
                throw new Exception();

            DO.Product p = Dal.Product.GetById(id);

            return new BO.ProductItem() { 
                ID = p.ID, 
                Name = p.Name, 
                Price = p.Price, 
                Category = (BO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), p.Category.ToString()),
                Available=p.InStock!=0,
                Amount=cart.ItemsList.First(x=>x.ProductId==id).Amount
            };

        }
        catch (Exception e)
        {
            throw new Exception("Do something!!!!!!!!!!");
        }
    }

    public IEnumerable<BO.ProductForList> GetProducts()
    {
        List<BO.ProductForList> products = new List<BO.ProductForList>();
        foreach (DO.Product p in Dal.Product.GetAll())
        {
            //products.Add(new BO.ProductForList()
            //{
            //    Category = (BO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), p.Category.ToString()),
            //    Price = p.Price,
            //    Name = p.Name,
            //    ID = p.ID
            //});
            //Console.WriteLine(Mappers.c);

            BO.ProductForList P2 = mapper.Map<DO.Product, BO.ProductForList>(p);
            products.Add(P2);
        };
        return products;
    }

    public void UpdateProduct(BO.Product product)
    {
        try
        {
            if (product.ID <= 0 || product.Name == "" || product.InStock < 0 || product.Price < 0)
                throw new Exception("JJJJ");
            Dal.Product.Update(new DO.Product()
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                InStock = product.InStock,
                Category = (DO.Enums.Category)(BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), product.Category.ToString())
            });
        }
        catch (Exception e)
        {
            throw new Exception("nnn");
        }
    }

}
