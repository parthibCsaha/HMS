using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        bool ValidateRefreshToken(string refreshToken);
    }
}
