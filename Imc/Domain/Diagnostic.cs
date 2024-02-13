using System.Text.Json.Serialization;

namespace Imc.Domain;

public class Diagnostic : Entity
{
    public Diagnostic(Guid userId, string title, string description, ImcVo imc)
    {
        UserId = userId;
        Title = title;
        Description = description;
        Imc = imc;
        CreatedAt = DateTime.Now;
    }

    [JsonInclude]
    public Guid UserId { get; private set; }
    
    [JsonInclude]
    public string Title { get; private set; }
    
    [JsonInclude]
    public string Description { get; private set; }
    
    [JsonInclude]
    public DateTime CreatedAt { get; private set; }
    
    [JsonInclude]
    public ImcVo Imc { get; private set; }
    
    public string GetIcon()
    {
        return Imc.Classification switch
        {
            ImcClassification.AbaixoDoPeso => "⚠️",
            ImcClassification.PesoNormal => "✅",
            ImcClassification.Sobrepeso => "⚠️",
            ImcClassification.ObesidadeGrau1 => "❗",
            ImcClassification.ObesidadeGrau2 => "❌",
            ImcClassification.ObesidadeGrau3 => "❌",
            _ => "❓"    
        };
    }
    
}