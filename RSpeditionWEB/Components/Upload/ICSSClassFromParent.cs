namespace RSpeditionWEB.Components.Upload
{
    public abstract class CSSClassFromParent : ComponentBaseClass
    {
        [CascadingParameter]
        public ICSSClassFromParent ICSSClassFromParent { get; set; }
    }

    public interface ICSSClassFromParent
    {
        string ClassForUploadForm { get; }
    }
}
