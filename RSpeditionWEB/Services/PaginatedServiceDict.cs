namespace RSpeditionWEB.Services
{
    public class PaginatedServiceDict<T, V> : PaginatedServiceBase<Dictionary<T, V>>
    {
        public PaginatedServiceDict(Dictionary<T, V> source, int size) : base(source, size)
        {
            if((this.Source?.Keys?.Count ?? 1) <= size)
            {
                this.TotalPages = 1;
            }
            else
            {
                var numItems = (this.Source?.Keys?.Count ?? 1);
                this.TotalPages = (numItems - (numItems % this.PageSize)) / this.PageSize;
                if(numItems % this.PageSize > 0)
                {
                    this.TotalPages++;
                }
            }
            this.PageIndex = 1;
            this.RowNum = 1;
        }
        public override int TotalPages { get; set; }
        public override int PageIndex { get; set; }
        public override int RowNum { get; set; }

        public override Dictionary<T, V> GetDataPerPage(int pageNum)
        {
            if (pageNum > 0 && pageNum <= this.TotalPages)
            {
                this.PageIndex = pageNum;
                this.RowNum = (this.PageIndex - 1) * this.PageSize + 1;
                return this.Source?
                       .Skip((this.PageIndex - 1) * this.PageSize)?
                       .Take(this.PageSize)?
                       .ToDictionary(_ => _.Key, _ => _.Value)
                       ;
            }
            else return new();
        }
    }
}
