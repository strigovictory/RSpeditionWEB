namespace RSpeditionWEB.Models.ID;

public class UserInfo
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public DateTime HireDate { get; set; }

    public DateTime? FireDate { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DutyId { get; set; }

    public string EngFirstName { get; set; }

    public string EndLastName { get; set; }

    public string TelegramUsername { get; set; }

    public string WorkPhoneNumber { get; set; }

    public bool IsActive { get; set; }

    public string UserHash { get; set; }

    public byte[] UserPhoto { get; set; }

    public bool IsAdmin { get; set; }
    
}
