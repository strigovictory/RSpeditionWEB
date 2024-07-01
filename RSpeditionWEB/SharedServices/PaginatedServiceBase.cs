namespace RSpeditionWEB.Abstract
{
    // Базовый класс для реализации пагинации
    public abstract class PaginatedServiceBase<T>
    {
        protected readonly T Source;
        protected readonly int PageSize;
        public abstract int PageIndex { get; set; }
        public abstract int TotalPages { get; set; }
        public abstract int RowNum { get; set; }
        public abstract T GetDataPerPage(int pageNum);
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedServiceBase(T source, int size)
        {
            this.Source = source;
            if(size > 0)
            {
                this.PageSize = size;
            }
            else
            {
                this.PageSize = 1;
            }
        }
    }
}
