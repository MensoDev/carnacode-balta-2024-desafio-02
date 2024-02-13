using System.ComponentModel.DataAnnotations;

namespace Imc.Models;

public class LoginCommand
{
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail é inválido.")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string? Password { get; set; }
    
    [Required(ErrorMessage = "O campo de verificação é obrigatório.")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "Você deve ser um humano.")]
    public bool IsHuman { get; set; }
    
}