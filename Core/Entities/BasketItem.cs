namespace Core.Entities
{
    public class BasketItem
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}