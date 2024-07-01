namespace RSpeditionWEB.Models.ViewModels.ReflectionModels
{
    public class TableItem : IIdString
    {
        public string Id { get; set; }
        public string DbName { get; set; }
        public string ClassName { get; set; }
        public string DBSchemaName { get; set; }
    }
}
