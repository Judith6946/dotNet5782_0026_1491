
using DO;

namespace BO
{
    public class OrderForList
    {
        /// <summary>
        ///  Unique ID of order.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of customer.
        /// </summary>
        public string CustomerName { get; set; }

        // <summary>
        /// status of order.
        /// </summary>
        public Enums.OrderStatus StatusOrder { get; set; }

        // <summary>
        /// amount of products in order.
        /// </summary>
        public int AmountOfItems { get; set; }

        // <summary>
        /// Total price of order.
        /// </summary>
        public double TotalPrice { get; set; }
    }
}
