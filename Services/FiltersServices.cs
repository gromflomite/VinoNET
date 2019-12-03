using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wineapp.Data;
using Wineapp.Models;

namespace Wineapp.Services
{
    public class FiltersServices : IFilters
    {
        private readonly ApplicationDbContext _context;

        public FiltersServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Colour> GetColourByIdAsync(int? id)
        {
            return await _context.Colours.FindAsync(id);
        }

        public async Task<List<Colour>> GetColourAsync()
        {
            return await _context.Colours.ToListAsync(); 
        }

        public async Task<Source> GetSourceByIdAsync(int? id)
        {
            return await _context.Sources.FindAsync(id);
        }

        public async Task<List<Source>> GetSourceAsync()
        {
            return await _context.Sources.ToListAsync();
        }

        public async Task<Sweetnes> GetSweetnesByIdAsync(int? id)
        {
            return await _context.Sweetness.FindAsync(id);
        }

        public async Task<List<Sweetnes>> GetSweetnesAsync()
        {
            return await _context.Sweetness.ToListAsync();
        }
    }
}
