
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Cart : ICart
{

    private IDal Dal = new DalList();
    public BO.Cart AddItem(BO.Cart cart, int id)
    {
        try
        {
            BO.OrderItem item = cart.ItemsList.FirstOrDefault(x => x.ID == id, null);
            if (item != null)
            {
                if (Dal.Product.GetById(id).InStock <= item.Amount)
                    throw new Exception();
                item.Amount++;
                item.TotalPrice += item.Price;
                cart.TotalPrice += item.Price;
            }
            else
            {
               DO.Product p=Dal.Product.GetAll().FirstOrDefault(x => x.ID == id,new DO.Product() { ID=0});
               if(p.ID==0||p.InStock<=0)
                    throw new Exception();
                ((List<BO.OrderItem>)cart.ItemsList).Add(new BO.OrderItem()
                {
                    ID = 0,
                    Amount = 1,
                    Price = p.Price,
                    ProductId = p.ID,
                    ProductName = p.Name,
                    TotalPrice = p.Price
                });
                cart.TotalPrice+=p.Price;
            }
            return cart;
        }
        catch (Exception e)
        {
            throw new Exception();
        }

    }

    public void MakeOrder(BO.Cart cart)
    {
        try
        {
            //add בדיקת תקינות
            DO.Order order = new DO.Order()
            {
                CustomerAdress=cart.CustomerAdress,
                CustomerEmail=cart.CustomerEmail,
                CustomerName=cart.CustomerName, 
                OrderDate=DateTime.Now,
                DeliveryDate=DateTime.MinValue,
                ShipDate=DateTime.MinValue,
            };
            int id = Dal.Order.Add(order);

            foreach (var item in cart.ItemsList)
            {

                DO.Product p = Dal.Product.GetById(item.ProductId);
                p.InStock -= item.Amount;
                if (p.InStock < 0)
                    throw new Exception();
                Dal.Product.Update(p);

                Dal.OrderItem.Add(new DO.OrderItem()
                {
                    Amount = item.Amount,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    OrderId = id
                });
                
               
            }

        }
        catch(Exception e)
        {

        }
    }

    public BO.Cart UpdateAmount(BO.Cart cart, int amount, int id)
    {
        try
        {
            BO.OrderItem item = cart.ItemsList.FirstOrDefault(x => x.ProductId == id, null);
            if (item == null)
                throw new Exception();
            if(item.Amount > amount)
            {
                if (Dal.Product.GetById(id).InStock <= item.Amount)
                    throw new Exception();
                item.Amount++;
                item.TotalPrice += item.Price;
                cart.TotalPrice += item.Price;
            }
            else
            {
                item.Amount--;
                item.TotalPrice -= item.Price;
                cart.TotalPrice -= item.Price;
                if(item.Amount == 0)
                {
                    cart.ItemsList.ToList().Remove(item);
                }
            }

            return cart;

        }
        catch(Exception e)
        {
            throw new Exception();
        }
    }

}
