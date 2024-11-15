using EventFlowerExchange.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowerExchange.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        Task<Token> GetTokenByValueAsync(string token);
        Task UpdateTokenAsync(Token token);
        Task AddTokenAsync(Token token);
    }
}
