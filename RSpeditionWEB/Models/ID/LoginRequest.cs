using System.ComponentModel.DataAnnotations;

namespace RSpeditionWEB.Models.ID;

public class LoginRequest
{
    [Required]
    [Display(Name = "Логин")]
    public string UserName { get; set; }

    [Required]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
}
