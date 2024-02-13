using Blazored.LocalStorage;
using Imc.Domain;
using Imc.Models;

namespace Imc.Services;

public interface IDiagnosticService
{
    Task RegisterDraftAsync();
    Task<Diagnostic?> GetAsync(Guid id);
    Task<IEnumerable<Diagnostic>> GetAllAsync(Guid userId);
    Task<IEnumerable<Diagnostic>> SearchAsync(Guid userId, string term);

    Task RemoveDraftAsync();
    Task<Diagnostic?> GetDraftAsync();
    Task CreateDraftAsync(CreateDiagnosticCommand command, Guid userId);
}

public class DiagnosticService(ILocalStorageService localStorage, IImcCalculatorService imcCalculator)
    : IDiagnosticService
{
    private List<Diagnostic> _diagnostics = [];
    private const string DiagnosticsKey = "diagnostics";
    private const string DraftDiagnosticKey = "draftDiagnostic";

    public async Task RegisterDraftAsync()
    {
        await ReadAsync();
        var diagnostic = await GetDraftAsync();
        if (diagnostic is null) return;
        _diagnostics.Add(diagnostic);
        await RemoveDraftAsync();
        await WriteAsync();
    }

    public async Task<Diagnostic?> GetAsync(Guid id)
    {
        await ReadAsync();
        return _diagnostics.Find(diagnostic => diagnostic.Id == id);
    }

    public async Task<IEnumerable<Diagnostic>> GetAllAsync(Guid userId)
    {
        await ReadAsync();
        return _diagnostics
            .Where(diagnostic => diagnostic.UserId == userId)
            .OrderByDescending(diagnostic => diagnostic.CreatedAt)
            .ToList();
    }

    public async Task<IEnumerable<Diagnostic>> SearchAsync(Guid userId, string term)
    {
        await ReadAsync();

        return _diagnostics
            .Where(diagnostic => diagnostic.UserId == userId &&
                                 (diagnostic.Title.Contains(term) || diagnostic.Description.Contains(term)))
            .OrderByDescending(diagnostic => diagnostic.CreatedAt)
            .ToList();
    }

    public async Task RemoveDraftAsync()
    {
        await localStorage.RemoveItemAsync(DraftDiagnosticKey);
    }

    public async Task<Diagnostic?> GetDraftAsync()
    {
        return await localStorage.GetItemAsync<Diagnostic>(DraftDiagnosticKey);
    }

    public async Task CreateDraftAsync(CreateDiagnosticCommand command, Guid userId)
    {
        var imcResult = imcCalculator.Calculate(
            command.Peso!.Value,
            command.Altura!.Value,
            command.Have65YearOrMore,
            command.Sex!.Value);

        var imc = new ImcVo(
            imcResult.Imc,
            command.Peso.Value,
            command.Altura.Value,
            command.Sex.Value,
            command.Have65YearOrMore,
            imcResult.Classification);

        var diagnostic = new Diagnostic(userId, imcResult.Title, imcResult.Description, imc);
        await localStorage.SetItemAsync(DraftDiagnosticKey, diagnostic);
    }

    private async Task WriteAsync()
    {
        await localStorage.SetItemAsync(DiagnosticsKey, _diagnostics);
    }

    private async Task ReadAsync()
    {
        _diagnostics = await localStorage.GetItemAsync<List<Diagnostic>>(DiagnosticsKey) ?? [];
    }
}