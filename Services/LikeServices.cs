using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wineapp.Data;
using Wineapp.Models;

namespace Wineapp.Services
{
    public class LikeServices : ILike
    {
        private readonly ApplicationDbContext _context;

        public LikeServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(UserScore userScore)
        {
            await _context.AddAsync(userScore);

            await _context.SaveChangesAsync();
           
        }

        public async Task Delete(int idWine, string idUserScore)
        {
            UserScore userScore =  _context.UserScores.FirstOrDefault(x => x.AppUserId == idUserScore && x.WineId == idWine);
            _context.Remove(userScore);
            await _context.SaveChangesAsync();
        }

        public Task<UserScore> Edit(int? idUserScore)
        {
            throw new NotImplementedException();
        }

        public bool Exit(string usuarioId, int wineId)
        {          
            return _context.UserScores.Where(x => x.AppUserId == usuarioId).Any(e => e.WineId == wineId);
        }

        public Task<List<UserScore>> GetColourAsync()
        {
            throw new NotImplementedException();
        }
    }
}
