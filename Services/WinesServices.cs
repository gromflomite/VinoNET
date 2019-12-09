using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wineapp.Data;
using Wineapp.Models;

namespace Wineapp.Services
{
    public class WinesServices : IWines
    {
        private readonly ApplicationDbContext _context;

        public WinesServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateWineAsync(Wine wine)
        {
            await _context.AddAsync(wine);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteWineAsync(Wine wine)
        {
            _context.Wines.Remove(wine);
            await _context.SaveChangesAsync();
        }

        public async Task<Wine> GetWineByIdAsync(int? id)
        {
            return await _context.Wines.FindAsync(id);
        }

        public async Task<List<Wine>> GetWinesAsync()
        {
            return await _context.Wines.Include(x => x.Colour).Include(x => x.Source).Include(x => x.Sweetnes).ToListAsync();
        }

        public async Task UpdateWineAsync(Wine wine)
        {
            _context.Update(wine);
            await _context.SaveChangesAsync();
        }

        public bool WineExists(int? id)
        {
            return _context.Wines.Any(e => e.Id == id);
        }
    }
}
