namespace Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrdered itemOrdered, int quantity, decimal price)
        {
            ItemOrdered = itemOrdered;
            Quantity = quantity;
            Price = price;
        }

        public ProductItemOrdered ItemOrdered { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}