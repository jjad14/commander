namespace Commander.helpers
{
    public class CommandParams
    {
        private const int MaxPageSize = 20;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 2;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int? PlatformId { get; set; }
    }
}
