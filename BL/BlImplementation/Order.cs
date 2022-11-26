
using BlApi;

using Dal;
using DalApi;


namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        private IDal Dal = new DalList();
        public OrderTracking FollowOrder(int id)
        {
            throw new NotImplementedException();
        }

        public BO.Order GetOrder(int id)
        {
            try
            {
                if (id < 0)
                    throw new Exception();
                DO.Order o = Dal.Order.GetById(id);
                return new BO.Order() { ID = o.ID, CustomerName = o.CustomerName, CustomerEmail = o.CustomerEmail, CustomerAdress = o.CustomerAdress, OrderDate = o.OrderDate, ShipDate = o.ShipDate, DeliveryDate = o.DeliveryDate) };

            }
            catch (Exception e)
            {
                throw new Exception("Do something!!");
            }
        }

        public IEnumerable<OrderForList> GetOrders()
        {
            List<BO.OrderForList> orders = new List<BO.OrderForList>();
            foreach (DO.Order o in Dal.Order.GetAll())
            {
                orders.Add(new BO.OrderForList()
                {
                    ID = o.ID,
                    CustomerName = o.CustomerName,
                //add another something?

                }); ;
            };
            return orders;
        }

        public BO.Order UpdateOrderDelivery(int id)
        {
            throw new NotImplementedException();
        }

        public BO.Order UpdateOrderShipping(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("JJJJ");
                if (Dal.Order.GetAll().Any(x => x.ID == id)&&(Dal.Order.GetById(id).ShipDate!=DateTime.MinValue()))
                  
              Dal.Order.Update( new DO.Order() { ShipDate = Dal.Order.ShipDate });


            }
            catch (Exception e)
            {
                throw new Exception("chaya");
            }
        }
    }
}
