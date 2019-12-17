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
        public Task<WineList> GetWineListByIdAsync(int id);
        public Task<WineList> GetWineListByNameListAsync(string nameList);
        public Task<WineListWine> GetWineListWineByIdAsync(int wineListWineId);
        public Task DeleteWineListWineAsync(WineListWine wineListWine);
        public Task<List<WineList>> GetWineListsByUserIdAsync(string userId);
        public Task<List<WineListWine>> GetWineListWineAsync();
        public Task<List<WineListWine>> GetWineListsWinesByWineLisIdAsync(int wineListId);
        public bool Exit(int? wineId, string userId);
        public Task CreateWineListAsync(WineList wineList);
        public Task DeleteWineListAsync(WineList wineList);
        public Task AddWineInWineListAsync(WineListWine wineListWine);
        public Task<WineListWine> IfExitWineListWine(int? wineId, string userId);
        public Task MoveWine(WineListWine wineListWine);
        public Task UpUpdate(WineList wineList);

    }
}
