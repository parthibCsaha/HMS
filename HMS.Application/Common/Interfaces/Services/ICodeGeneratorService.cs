namespace HMS.Application.Common.Interfaces.Services;

public interface ICodeGeneratorService
{
    Task<string> GenerateCodeAsync(string prefix, CancellationToken ct = default);
}
