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
        public async Task<Colour> GetColourTasteByIdAsync(int? id)
        {
            return await _context.Colours.FindAsync(id);
        }

        public async Task<List<Colour>> GetColourTastesAsync()
        {
            return await _context.Colours.ToListAsync(); 
        }

        public async Task<Source> GetSourceTasteByIdAsync(int? id)
        {
            return await _context.Sources.FindAsync(id);
        }

        public async Task<List<Source>> GetSourceTastesAsync()
        {
            return await _context.Sources.ToListAsync();
        }

        public async Task<Sweetnes> GetSweetnesTasteByIdAsync(int? id)
        {
            return await _context.Sweetness.FindAsync(id);
        }

        public async Task<List<Sweetnes>> GetSweetnesTastesAsync()
        {
            return await _context.Sweetness.ToListAsync();
        }
    }
}
