namespace RSpeditionWEB.Components.ComponentsBase
{
    public abstract class UploadFilesBaseParent<T, V, L> : UploadFilesBase<T> where T : class, ICloneable, IUploadedContent, new() where V : class where L : class
    {
        [CascadingParameter]
        protected ScaffoldJCMainBase ScaffoldJCMainBaseCascadingParameter { get; set; }
        public List<V> SuccessItems { get; set; } = new();
        public List<V> SecondarySuccessItems { get; set; } = new();
        public List<NotSuccessResponseItemDetailed<L>> NotSuccessItems { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            ReturnUrl = ScaffoldJCMainBaseCascadingParameter?.ReturnUrl ?? "/";
            await UpdateCollectionsForFiltersFromDB();
        }

        private async Task UpdateCollectionsForFiltersFromDB()
        {
            await InitializeCollections();
            InitButtons();
            IsRender = true;
        }

        protected virtual List<(int, string, string)> GetTupleNotSuccess()
        {
            List<(int, string, string)> res = new();
            for (var i = 0; i < NotSuccessItems.Count; i++)
            {
                var item = NotSuccessItems[i];
                var name = item.NotSuccessItem.ToString();
                var reason = item.Reason;
                res.Add((i+1, name, reason));
            }
            return res;
        }

        protected virtual List<(int, string, string)> GetTupleSuccess()
        {
            List<(int, string, string)> res = new();
            for (var i = 0; i < SuccessItems.Count; i++)
            {
                var item = SuccessItems[i];
                var name = item.ToString();
                res.Add((i+1, name, string.Empty));
            }
            return res;
        }

        protected virtual List<(int, string, string)> GetTupleSecondarySuccess()
        {
            List<(int, string, string)> res = new();
            for (var i = 0; i < this.SecondarySuccessItems.Count; i++)
            {
                var item = this.SecondarySuccessItems[i];
                var name = item.ToString();
                res.Add((i+1, name, string.Empty));
            }
            return res;
        }
    }
}
