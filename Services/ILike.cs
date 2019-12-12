using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wineapp.Models;

namespace Wineapp.Services
{
    public interface ILike
    {
        public Task<List<UserScore>> GetUserScoreAsync();
        public Task Create(UserScore userScore);
        public Task<UserScore> Edit(int? idUserScore);
        public Task Delete(int idWine, string idUserScore);
        public bool Exit(string usuarioId, int wineId);

    }
}
