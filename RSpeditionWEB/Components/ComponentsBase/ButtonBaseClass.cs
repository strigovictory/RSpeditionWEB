namespace RSpeditionWEB.Components.ComponentsBase
{
    public class ButtonBaseClass : ComponentBase
    {
        public Action CallbackToParent { get; set; }

        public string LabelForButton { get; set; }

        // Если свойство равно истина - кнопка активна
        public bool IsActive { get; set; }

        public string TitleIfIsNonActive { get; set; }

        public string TitleIfIsActive { get; set; }
    }
}
