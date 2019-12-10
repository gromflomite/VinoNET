using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wineapp.Data;
using Wineapp.Models;

namespace Wineapp.Services
{
    public class WineListsServices : IWineLists
    {
        private readonly ApplicationDbContext _context;
        

        public WineListsServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddWineInWineListAsync(WineListWine wineListWine)
        {
            await _context.AddAsync(wineListWine);

            await _context.SaveChangesAsync();
        }

        public async Task CreateWineListAsync(WineList wineList)
        {
            await _context.AddAsync(wineList);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteWineListAsync(WineList wineList)
        {
            foreach (WineListWine wineListWine in await _context.WineListWines.Where(x => x.WineListId == wineList.Id).ToListAsync())
            {
                 _context.WineListWines.Remove(wineListWine);
            }

            _context.WineLists.Remove(wineList);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WineList>> GetWineListsAsync()
        {
            return await _context.WineLists.ToListAsync();
        }

        public async Task<List<WineList>> GetWineListsByUserIdAsync(string userId)
        {
            return await _context.WineLists.Where(x=>x.AppUserId == userId).ToListAsync();
        }

        public async Task<List<WineListWine>> GetWineListsWinesByUserIdAsync(int? WineListId)
        {
            return await _context.WineListWines.ToListAsync();
        }
    }
}
