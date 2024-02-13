using System.ComponentModel.DataAnnotations;

namespace Imc.Models;

public class CreateUserAccountCommand
{
    [Required(ErrorMessage = "O Nome é obrigatório.")]
    public string? Name { get; set; }
    
    [Required(ErrorMessage = "O Email é obrigatório.")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "A Senha é obrigatória.")]
    public string? Password { get; set; }
    
    [Required(ErrorMessage = "A Confirmação de Senha é obrigatória.")]
    [Compare(nameof(Password), ErrorMessage = "A Confirmação de Senha não confere com a Senha.")]
    public string? ConfirmPassword { get; set; }
    
    [Required(ErrorMessage = "Você é um humano?")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "Você deve ser um humano.")]
    public bool IsHuman { get; set; }
}