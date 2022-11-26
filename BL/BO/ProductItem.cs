using DO;

namespace BO
{
    public class ProductItem
    {
        /// <summary>
        /// Unique ID of product.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of product-item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Price ID of product-item.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Category of product-item.
        /// </summary>
        public Enums.Category Category { get; set; }


        /// <summary>
        ///If item is Available.
        /// </summary>
        public bool Available { get; set; }

        /// <summary>
        /// Amount in the buyer's shopping cart.
        /// </summary>
        public int Amount { get; set; }
    }
}

