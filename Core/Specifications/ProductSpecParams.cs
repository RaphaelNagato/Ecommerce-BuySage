namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 20;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 5;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize || value < 1) ? MaxPageSize : value;
        }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string Sort { get; set; }
        private string _search;
        public string Search {
            get => _search;
            set => _search = value.ToLower();
        }

    }
}