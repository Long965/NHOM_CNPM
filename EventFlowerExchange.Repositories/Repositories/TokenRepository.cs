using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowerExchange.Repositories.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly EventFlowerExchangeContext _context;
        public TokenRepository(EventFlowerExchangeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Token> GetTokenByValueAsync(string token)
        {
            return await _context.Tokens.FirstOrDefaultAsync(t => t.Token1 == token && !t.IsRevoked);
        }

        public async Task UpdateTokenAsync(Token token)
        {
            _context.Tokens.Update(token);
            await _context.SaveChangesAsync();
        }
        public async Task AddTokenAsync(Token token)
        {
        _context.Tokens.Add(token);
        await _context.SaveChangesAsync();
        }

    }
}
