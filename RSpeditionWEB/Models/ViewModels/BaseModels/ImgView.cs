namespace RSpeditionWEB.Models.ViewModels.BaseModels
{
    public class ImgView : IComparable
    {
        public ImgView(string src)
        {
            this.ImgSrc = src;
        }
        public string ImgSrc { get; }

        public int CompareTo(object obj)
        => this.ImgSrc
            .Substring(this.ImgSrc.LastIndexOf('/'), this.ImgSrc.Length - this.ImgSrc.LastIndexOf('/'))
            .CompareTo((obj as ImgView).ImgSrc.Substring((obj as ImgView).ImgSrc.LastIndexOf('/'), (obj as ImgView).ImgSrc.Length - (obj as ImgView).ImgSrc.LastIndexOf('/')));

        public override string ToString()
        => this.ImgSrc;
    }
}
