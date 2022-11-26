using DO;

namespace BO
{
    public class OrderTracking
    {
        /// <summary>
        ///  Unique ID of order.
        /// </summary>
        public int ID { get; set; }

        // <summary>
        /// status of order.
        /// </summary>
        public string StatusOrder { get; set; }


        // <summary>
        /// עוד לא צריך להתיחס לזה, אבל אח"כ אמור להיות שם תאריך וסטטוס הזמנה.
        /// </summary>
        public IEnumerable<Tuple<DateTime, string>> Tracking { get; set; }



    }
}
