using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wineapp.Models;

namespace Wineapp.Services
{
    public interface IWineLists
    {
        public Task<List<WineList>> GetWineListsAsync();
        public Task<List<WineList>> GetWineListsByUserIdAsync(string userId);
        public Task<List<WineListWine>> GetWineListsWinesByUserIdAsync(int? WineListId);
        public Task CreateWineListAsync(WineList wineList);
        public Task DeleteWineListAsync(WineList wineList);
        public Task AddWineInWineListAsync(WineListWine wineListWine);
    }
}
