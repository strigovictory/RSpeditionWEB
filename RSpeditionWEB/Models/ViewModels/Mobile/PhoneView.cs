namespace RSpeditionWEB.Models.ViewModels.Mobile;

[Display(Name = "Телефонный аппарат")]
public class PhoneView : IId
{
    public PhoneView() { }

    [CustomDisplayAttribute("Идентификационный номер", "Укажите идентификационный номер")]
    public int Id { get; set; }

    [CustomDisplayAttribute("Серийный номер", "Укажите серийный номер")]
    public string SerialNumber { get; set; }

    [CustomDisplayAttribute("Стоимость в евро", "Укажите стоимость в евро")]
    public decimal? CostEur { get; set; }

    [CustomDisplayAttribute("Текущий статус", "Укажите текущий статус")]
    public int? ActualStatus { get; set; }

    [CustomDisplayAttribute("Номера телефонов", "Укажите номера телефонов")]
    public string Telephones { get; set; }

    [CustomDisplayAttribute("Не работающий", "Укажите, если телефон не работающий")]
    public bool IsDead { get; set; }

    [CustomDisplayAttribute("Используется в офисе", "Укажите, если телефон используется в офисе")]
    public bool Foffice { get; set; }

    [CustomDisplayAttribute("Комментарий", "Укажите комментарий")]
    public string Comment { get; set; }

    [CustomDisplayAttribute("Счет на телефон", "Выберите счет на телефон")]
    [CustomForeignAttribute(typeof(PhoneInvoiceView), "InvoiceNumber")]
    public Guid? InvoiceGuid { get; set; }

    [CustomDisplayAttribute("Эл.почта google", "Укажите эл.почту google")]
    public string GoogleEmail { get; set; }

    [CustomDisplayAttribute("Пароль от эл.почты google", "Укажите пароль от эл.почты google")]
    public string GooglePassword { get; set; }

    [CustomForeignAttribute(typeof(PhoneModelView), "ModelName")]
    public int FkPhoneModelId { get; set; }

    [CustomDisplayAttribute("Модель телефонного аппарата", "Выберите модель телефонного аппарата")]
    public string PhoneModelName { get; set; }

    [CustomForeignAttribute(typeof(DivisionResponse), "Name")]
    public int? FkDivisionId { get; set; }

    [CustomDisplayAttribute("Подразделение", "Выберите подразделение")]
    public string DivisionName{ get; set; }

    [CustomForeignAttribute(typeof(TruckResponse), "CarNumber")]
    public int? FkTruckId { get; set; }

    [CustomDisplayAttribute("Тягач", "Выберите тягач")]
    public string TruckName{ get; set; }

    [CustomForeignAttribute(typeof(EmployeeView), "LastName")]
    public int? FkEmployeeId { get; set; }

    [CustomDisplayAttribute("Работник", "Выберите работника")]
    public string EmployeeName { get; set; }

    [CustomForeignAttribute(typeof(SimCardView), "SimCardNumber")]
    public int? FkSimCardId { get; set; }

    [CustomDisplayAttribute("Sim-карта", "Выберите sim-карту")]
    public string SimCardNum { get; set; }

    [CustomForeignAttribute(typeof(DivisionResponse), "Name")]
    public int? FkCurrentDivisionId { get; set; }

    [CustomDisplayAttribute("Текущее подразделение", "Выберите текущее подразделение")]
    public string CurrentDivisionName{ get; set; }
}
