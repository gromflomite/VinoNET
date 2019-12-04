using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wineapp.Models;

namespace Wineapp.Services
{
    public interface IWines
    {
        public Task<List<Wine>> GetWinesAsync();
        public Task<Wine> GetWineByIdAsync(int? id);
        public Task CreateWineAsync(Wine wine);
        public Task UpdateWineAsync(Wine wine);
        public Task DeleteWineAsync(Wine wine);
        public bool WineExists(int? id);
    }
}
