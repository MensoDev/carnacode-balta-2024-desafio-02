using System.ComponentModel.DataAnnotations;
using Imc.Domain;

namespace Imc.Models;

public class CreateDiagnosticCommand
{
    [Required(ErrorMessage = "O peso é obrigatório")]
    public decimal? Peso { get; set; }
    
    [Required(ErrorMessage = "A altura é obrigatória")]
    public decimal? Altura { get; set; }
    
    [Required(ErrorMessage = "O sexo é obrigatório")]
    public Sex? Sex { get;  set; }
    
    [Required(ErrorMessage = "confirme se você tem ou não 65 anos ou mais")]
    public bool Have65YearOrMore { get; set; }
}