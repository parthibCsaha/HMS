using System.Data;
using HMS.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace HMS.Infrastructure.Persistence;

public class DapperContext(IConfiguration configuration) : IDapperContext
{
    private readonly string _connectionString =
        configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}
