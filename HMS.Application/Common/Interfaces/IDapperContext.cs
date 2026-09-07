using System.Data;

namespace HMS.Application.Common.Interfaces;

public interface IDapperContext
{
    IDbConnection CreateConnection();
}
