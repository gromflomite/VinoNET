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
        public async Task<WineList> GetWineListByNameListAsync(string nameList,string userId)
        {
            return await _context.WineLists.FirstOrDefaultAsync(x => x.ListName == nameList && x.AppUserId == userId);
        }
        public async Task<WineList> GetWineListByIdAsync(int id)
        {
            return await _context.WineLists.FindAsync(id);
        }

        //wine list wine
        public async Task<List <WineListWine>> GetWineListWineAsync()
        {
            return await _context.WineListWines.ToListAsync();
        }
        public async Task<WineListWine> GetWineListWineByIdAsync(int wineListWineId)
        {
            return await _context.WineListWines.FindAsync(wineListWineId); ;
        }

        public async Task DeleteWineListWineAsync(WineListWine wineListWine)
        {           
            _context.WineListWines.Remove(wineListWine);
            await _context.SaveChangesAsync();
        }        

        public async Task AddWineInWineListAsync(WineListWine wineListWine)
        {
            await _context.AddAsync(wineListWine);

            await _context.SaveChangesAsync();
        }

        public bool Exit(int? wineId,string userId)
        {
            List<WineList> listsWineLists = _context.WineLists.Where(x => x.AppUserId == userId).ToList();
            foreach (WineList wineLists in listsWineLists)
            {
                if (_context.WineListWines.Any(x => x.WineListId == wineLists.Id && x.WineId == wineId))
                {
                    return true;
                }
            }

            return false;
        }
        public async Task<WineListWine> IfExitWineListWine(int? wineId, string userId)
        {
            List<WineList> listsWineLists = _context.WineLists.Where(x => x.AppUserId == userId).ToList();
            foreach (WineList wineLists in listsWineLists)
            {
                if (_context.WineListWines.Any(x => x.WineListId == wineLists.Id && x.WineId == wineId))
                {
                    return await _context.WineListWines.FirstOrDefaultAsync(x=>x.WineListId == wineLists.Id && x.WineId == wineId);
                }
            }
            return null;
        }
        public async Task MoveWine(WineListWine wineListWine)
        {
            _context.Update(wineListWine);
            await _context.SaveChangesAsync();           
        }

        //
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

        public async Task<List<WineListWine>> GetWineListsWinesByWineLisIdAsync(int wineListId)
        {
            return await _context.WineListWines.Where(x=>x.WineListId == wineListId).Include(x=>x.Wine).ThenInclude(x=>x.Colour)
                .Include(x=>x.Wine).ThenInclude(x=>x.Source)
                .Include(x=>x.Wine).ThenInclude(x=>x.Sweetnes)
                .ToListAsync();
        }
        public async Task UpUpdate(WineList wineList)
        {
            _context.Update(wineList);
            await _context.SaveChangesAsync();
        }

    }
}
