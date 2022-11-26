
using BlApi;

using Dal;
using DalApi;

namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        private IDal Dal = new DalList();
        public BO.OrderTracking FollowOrder(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("JJJJ");
                DO.Order order = Dal.Order.GetAll().FirstOrDefault(x => x.ID == id, new DO.Order() { ID = 0 });

                if (order.ID == 0)
                    throw new Exception();

                BO.OrderTracking orderTracking = new BO.OrderTracking()
                {
                    OrderId = order.ID,
                    StatusOrder = order.DeliveryDate != DateTime.MinValue ? BO.Enums.OrderStatus.delivered : order.ShipDate != DateTime.MinValue ? BO.Enums.OrderStatus.sent : BO.Enums.OrderStatus.approved,
                    Tracking = new List<Tuple<DateTime, string>>()
                };
                orderTracking.Tracking.ToList().Add(new Tuple<DateTime, string>(order.OrderDate, "Order approved"));
                if (order.ShipDate != DateTime.MinValue)
                    orderTracking.Tracking.ToList().Add(new Tuple<DateTime, string>(order.ShipDate, "Order sent"));
                if (order.DeliveryDate != DateTime.MinValue)
                    orderTracking.Tracking.ToList().Add(new Tuple<DateTime, string>(order.DeliveryDate, "Order delivered"));
                return orderTracking;
            }
            catch(Exception e)
            {
                throw new Exception();
            }
        }

        public BO.Order GetOrder(int id)
        {
            try
            {
                if (id < 0)
                    throw new Exception();
                DO.Order o = Dal.Order.GetById(id);
                BO.Order order=new BO.Order() { ID = o.ID, CustomerName = o.CustomerName, CustomerEmail = o.CustomerEmail, CustomerAdress = o.CustomerAdress, OrderDate = o.OrderDate, ShipDate = o.ShipDate, DeliveryDate = o.DeliveryDate,
                    TotalPrice = Dal.OrderItem.GetByOrder(o.ID).Sum(x => x.Price),
                    Status = o.DeliveryDate != DateTime.MinValue ? BO.Enums.OrderStatus.delivered : o.ShipDate != DateTime.MinValue ? BO.Enums.OrderStatus.sent : BO.Enums.OrderStatus.approved
                };

                foreach (var item in Dal.OrderItem.GetByOrder(id))
                {
                    order.ItemsList.ToList().Add(new BO.OrderItem()
                    {
                        ID = item.ID,
                        Amount = item.Amount,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        ProductName = Dal.Product.GetById(item.ProductId).Name,
                        TotalPrice = item.Amount * item.Price
                    });
                }

                return order;
            }
            catch (Exception e)
            {
                throw new Exception("Do something!!");
            }
        }

        public IEnumerable<BO.OrderForList> GetOrders()
        {
            List<BO.OrderForList> orders = new List<BO.OrderForList>();
            foreach (DO.Order o in Dal.Order.GetAll())
            {
                orders.Add(new BO.OrderForList()
                {
                    ID = o.ID,
                    CustomerName = o.CustomerName,
                    TotalPrice = Dal.OrderItem.GetByOrder(o.ID).Sum(x => x.Price),
                    AmountOfItems = Dal.OrderItem.GetByOrder(o.ID).Sum(x => x.Amount),
                    StatusOrder = o.DeliveryDate != DateTime.MinValue ? BO.Enums.OrderStatus.delivered : o.ShipDate != DateTime.MinValue ? BO.Enums.OrderStatus.sent : BO.Enums.OrderStatus.approved
                }) ; 
            };
            return orders;
        }

        public BO.Order UpdateOrderDelivery(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("JJJJ");
                DO.Order order = Dal.Order.GetAll().FirstOrDefault(x => x.ID == id, new DO.Order() { ID = 0 });

                if (order.ID == 0 || Dal.Order.GetById(id).DeliveryDate != DateTime.MinValue)
                    throw new Exception();

                order.DeliveryDate = DateTime.Now;

                BO.Order order2 = new BO.Order()
                {
                    ID = order.ID,
                    CustomerAdress = order.CustomerAdress,
                    CustomerEmail = order.CustomerEmail,
                    CustomerName = order.CustomerName,
                    DeliveryDate = order.DeliveryDate,
                    OrderDate = order.OrderDate,
                    ShipDate = order.ShipDate,
                    Status = BO.Enums.OrderStatus.delivered,
                    TotalPrice = Dal.OrderItem.GetByOrder(id).Sum(x => x.Price)
                };

                foreach (var item in Dal.OrderItem.GetByOrder(id))
                {
                    order2.ItemsList.ToList().Add(new BO.OrderItem()
                    {
                        ID = item.ID,
                        Amount = item.Amount,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        ProductName = Dal.Product.GetById(item.ProductId).Name,
                        TotalPrice = item.Amount * item.Price
                    });
                }

                Dal.Order.Update(order);
                return order2;

            }
            catch (Exception e)
            {
                throw new Exception("chaya");
            }
        }

        public BO.Order UpdateOrderShipping(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("JJJJ");
                DO.Order order = Dal.Order.GetAll().FirstOrDefault(x => x.ID == id, new DO.Order() { ID = 0 });
               
                if (order.ID == 0 || Dal.Order.GetById(id).ShipDate != DateTime.MinValue)
                    throw new Exception();
                 
                order.ShipDate = DateTime.Now;

                BO.Order order2 = new BO.Order()
                {
                    ID = order.ID,
                    CustomerAdress = order.CustomerAdress,
                    CustomerEmail = order.CustomerEmail,
                    CustomerName = order.CustomerName,
                    DeliveryDate = order.DeliveryDate,
                    OrderDate = order.OrderDate,
                    ShipDate = order.ShipDate,
                    Status = BO.Enums.OrderStatus.sent,
                    TotalPrice = Dal.OrderItem.GetByOrder(id).Sum(x => x.Price)
                };

                foreach (var item in Dal.OrderItem.GetByOrder(id))
                {
                    order2.ItemsList.ToList().Add(new BO.OrderItem()
                    {
                        ID = item.ID,
                        Amount = item.Amount,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        ProductName = Dal.Product.GetById(item.ProductId).Name,
                        TotalPrice = item.Amount * item.Price
                    });
                }

                Dal.Order.Update(order);
                return order2;

            }
            catch (Exception e)
            {
                throw new Exception("chaya");
            }
        }
    }
}
