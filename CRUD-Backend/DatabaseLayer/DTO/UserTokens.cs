using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DTO
{
    public class UserTokens
    {
        public string? RefreshTokens { get; set; } = string.Empty;

        public string? JwtTokens { get; set; } = string.Empty;
    }
}
