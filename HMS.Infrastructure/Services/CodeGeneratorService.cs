using System.Data;
using Dapper;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Services;

namespace HMS.Infrastructure.Services;

public class CodeGeneratorService(IDapperContext dapperContext) : ICodeGeneratorService
{
    public async Task<string> GenerateCodeAsync(string prefix, CancellationToken ct = default)
    {
        using var connection = dapperContext.CreateConnection();

        var nextNumber = await connection.ExecuteScalarAsync<int>(
            "SELECT * FROM sp_get_next_code(@Prefix)",
            new { Prefix = prefix }
        );

        return $"{prefix}-{nextNumber:D5}";
    }
}
