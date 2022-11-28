using DO;

namespace BO
{
    public class ProductForList
    {
        /// <summary>
        ///  Unique ID of product.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///  Name of product-for-list.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  Price of product-for-list.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        ///  Category of product-for-list.
        /// </summary>
        public Enums.Category Category { get; set; }

        public override string ToString() => $@"
        Product ID={ID}: {Name}, 
        category - {Category}
    	Price: {Price}
        ";
    }
}
