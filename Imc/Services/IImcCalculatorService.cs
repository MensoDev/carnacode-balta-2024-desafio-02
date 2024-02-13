using Imc.Domain;

namespace Imc.Services;

public interface IImcCalculatorService
{
    ImcResult Calculate(decimal weight, decimal height, bool have65YearOrMore, Sex sex);
}

public class ImcCalculatorService : IImcCalculatorService
{
    public ImcResult Calculate(decimal weight, decimal height, bool have65YearOrMore, Sex sex)
    {
        var imc =  weight/ (height * height);
        var classification = GetClassification(imc, have65YearOrMore, sex);
        return new ImcResult
        {
            Imc = imc,
            Classification = classification,
            Description = GetDescription(classification),
            Title = GetTitle(classification)
        };
    }

    private static string GetTitle(ImcClassification classification)
    {
        return classification switch 
        {
            ImcClassification.AbaixoDoPeso => "Abaixo do peso",
            ImcClassification.PesoNormal => "Peso normal",
            ImcClassification.Sobrepeso => "Sobrepeso",
            ImcClassification.ObesidadeGrau1 => "Obesidade grau 1",
            ImcClassification.ObesidadeGrau2 => "Obesidade grau 2",
            ImcClassification.ObesidadeGrau3 => "Obesidade grau 3",
            _ => string.Empty
        };
    }

    private static string GetDescription(ImcClassification classification)
    {
        return classification switch
        {
            ImcClassification.AbaixoDoPeso => "Indica que a pessoa está abaixo do peso considerado saudável para sua altura e idade. Isso pode ser resultado de uma dieta inadequada, problemas de saúde ou outras razões.",
            ImcClassification.PesoNormal => "Reflete um IMC dentro da faixa considerada saudável para a maioria das pessoas. Indica que o peso corporal está em equilíbrio com a altura, o que geralmente sugere boa saúde.",
            ImcClassification.Sobrepeso => "Indica que o indivíduo tem um peso superior ao considerado saudável para sua altura e idade. Isso pode aumentar o risco de desenvolver condições de saúde como diabetes tipo 2, pressão alta e doenças cardíacas.",
            ImcClassification.ObesidadeGrau1 => "Indica um nível inicial de obesidade, onde o peso está significativamente acima do considerado saudável. Isso aumenta consideravelmente o risco de desenvolver doenças crônicas como diabetes, hipertensão e doenças cardíacas.",
            ImcClassification.ObesidadeGrau2 => "Indica um nível mais elevado de obesidade, com um peso consideravelmente acima do saudável. Isso aumenta significativamente o risco de problemas de saúde graves, como doenças cardíacas, acidente vascular cerebral e certos tipos de câncer.",
            ImcClassification.ObesidadeGrau3 => "Indica obesidade mórbida, onde o excesso de peso é extremamente elevado e representa um sério risco à saúde. Isso aumenta drasticamente o risco de doenças crônicas graves e reduz a expectativa de vida.",
            _ => string.Empty
        };
    }

    private static ImcClassification GetClassification(decimal imc, bool have65YearOrMore, Sex sex)
    {
        return have65YearOrMore switch
        {
            true when sex is Sex.Female => imc switch
            {
                < 21.9m => ImcClassification.AbaixoDoPeso,
                >= 21.9m and < 33.1m => ImcClassification.PesoNormal,
                >= 33.1m and < 38.1m => ImcClassification.Sobrepeso,
                >= 38.1m and < 41.1m => ImcClassification.ObesidadeGrau1,
                >= 41.1m and < 45.1m => ImcClassification.ObesidadeGrau2,
                _ => ImcClassification.ObesidadeGrau3
            },
            true when sex is Sex.Male => imc switch
            {
                < 22m => ImcClassification.AbaixoDoPeso,
                >= 22m and < 34m => ImcClassification.PesoNormal,
                >= 34m and < 39m => ImcClassification.Sobrepeso,
                >= 39m and < 43m => ImcClassification.ObesidadeGrau1,
                >= 43m and < 48m => ImcClassification.ObesidadeGrau2,
                _ => ImcClassification.ObesidadeGrau3
            },
            _ => imc switch
            {
                < 18.5m => ImcClassification.AbaixoDoPeso,
                >= 18.5m and < 24.9m => ImcClassification.PesoNormal,
                >= 24.9m and < 29.9m => ImcClassification.Sobrepeso,
                >= 29.9m and < 34.9m => ImcClassification.ObesidadeGrau1,
                >= 34.9m and < 39.9m => ImcClassification.ObesidadeGrau2,
                _ => ImcClassification.ObesidadeGrau3
            }
        };
    }
}

public record ImcResult
{
    public required decimal Imc { get; init; }
    public required ImcClassification Classification { get; init; }
    public required string Description { get; init; }
    public required string Title { get; init; }
}