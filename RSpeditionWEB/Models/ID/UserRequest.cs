namespace RSpeditionWEB.Models.ID;

public class UserRequest
{
    public bool IsAuthenticated { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public Dictionary<string, string> Claims { get; set; }

    public IEnumerable<string> Roles { get; set; }
}
