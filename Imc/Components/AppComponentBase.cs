using Microsoft.AspNetCore.Components;

namespace Imc.Components;

public class AppComponentBase : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? UserAttributes { get; set; }
    
    [Parameter]
    public string? Class { get; set; }
    
    [Parameter]
    public string? Style { get; set; }
}