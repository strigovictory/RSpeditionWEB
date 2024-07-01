namespace RSpeditionWEB.Validation
{
    internal class ValidateIEnumerable : ValidationAttribute
    {
        public string CollectionName { get; set; }
        public override bool IsValid(object value)
        {
            var res = false;
            if (value is ICollection collection)
            {
                res = collection.Count > 0;
            }
            if (value is IEnumerable enumerable)
            {
                res = enumerable.GetEnumerator().MoveNext();
            }

            this.ErrorMessage = res ? String.Empty : $"Коллекция {this.CollectionName ?? string.Empty} должна содержать не менее 1 элемента !";

            return res;
        }
    }
}
