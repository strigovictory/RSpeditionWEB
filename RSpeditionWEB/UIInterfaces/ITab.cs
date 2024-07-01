namespace RSpeditionWEB.UIInterfaces
{
    // Интерфейс для реализации вкладок
    public interface ITab
    {
        RenderFragment ChildContent { get; }
    }
}
