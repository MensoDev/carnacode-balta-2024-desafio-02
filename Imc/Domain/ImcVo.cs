using System.Text.Json.Serialization;

namespace Imc.Domain;

public class ImcVo
{
    public ImcVo(decimal imc, decimal peso, decimal altura, Sex sex, bool have65YearOrMore, ImcClassification classification)
    {
        Imc = imc;
        Peso = peso;
        Altura = altura;
        Sex = sex;
        Have65YearOrMore = have65YearOrMore;
        Classification = classification;
    }

    [JsonInclude]
    public decimal Imc { get; private set; }
    
    [JsonInclude]
    public decimal Peso { get; private set; }
    
    [JsonInclude]
    public decimal Altura { get; private set; }
    
    [JsonInclude]
    public ImcClassification Classification { get; set; }
    
    [JsonInclude]
    public Sex Sex { get; private set; }
    
    [JsonInclude]
    public bool Have65YearOrMore { get; private set; }
}