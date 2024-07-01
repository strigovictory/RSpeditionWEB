namespace RSpeditionWEB.Models.ID;

public class WebUser
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public bool IsAuthenticated { get; set; }

    public bool IsAdmin { get; set; }

    public IEnumerable<string> Roles { get; set; }

    public Dictionary<string, string> Claims { get; set; }
}
