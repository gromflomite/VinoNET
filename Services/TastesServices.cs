using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wineapp.Data;
using Wineapp.Models;

namespace Wineapp.Services
{
    public class TastesServices : ITastes
    {
        private readonly ApplicationDbContext _context;

        public TastesServices(ApplicationDbContext context)
        {
            _context = context;
        }

        
        //COLOUR TASTE
        public bool ColourTasteExists(int? id)
        {
            return _context.ColourTastes.Any(e => e.Id == id);
        }
        public async Task CreateColourTasteAsync(ColourTaste colourTaste)//
        {
            await _context.AddAsync(colourTaste);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteColourTasteAsync(ColourTaste colourTaste)
        {
            _context.ColourTastes.Remove(colourTaste);
            await _context.SaveChangesAsync();
        }       
        public async Task<ColourTaste> GetColourTasteByIdAsync(int? id)
        {
            return await _context.ColourTastes.FindAsync(id);
        }
        public async Task<List<ColourTaste>> GetColourTasteesByUserIdAsync(string userId)
        {
            return await _context.ColourTastes.Where(x => x.AppUserId == userId).ToListAsync();
        }
        public async Task<List<ColourTaste>> GetColourTastesAsync()
        {
            return await _context.ColourTastes.Include(x=>x.Colour).Include(x=>x.AppUser).ToListAsync();
        }
        public async Task UpdateColourTasteAsync(ColourTaste colourTaste)
        {
            _context.Update(colourTaste);
            await _context.SaveChangesAsync();
        }


        //SOURCE TASTE
        public async Task CreateSourceTasteAsync(SourceTaste sourceTaste)
        {
            await _context.AddAsync(sourceTaste);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteSourceTasteAsync(SourceTaste sourceTaste)
        {
            _context.SourceTastes.Remove(sourceTaste);
            await _context.SaveChangesAsync();
        }
        public async Task<SourceTaste> GetSourceTasteByIdAsync(int? id)
        {
            return await _context.SourceTastes.FindAsync(id);
        }
        public async Task<List<SourceTaste>> GetSourceTasteesByUserIdAsync(string userId)
        {
            return await _context.SourceTastes.Where(x => x.AppUserId == userId).ToListAsync(); 
        }
        public async Task<List<SourceTaste>> GetSourceTastesAsync()
        {
            return await _context.SourceTastes.Include(x=>x.Source).Include(x=>x.AppUser).ToListAsync();
        }
        public bool SourceTasteExists(int? id)
        {
            return _context.SourceTastes.Any(e => e.Id == id);
        }
        public async Task UpdateSourceTasteAsync(SourceTaste sourceTaste)
        {
            _context.Update(sourceTaste);
            await _context.SaveChangesAsync();
        }

        
        //SWEETNESS TASTE
        public async Task CreateSweetnessTasteAsync(SweetnessTaste sweetnessTaste)
        {
            await _context.AddAsync(sweetnessTaste);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteSweetnessTasteAsync(SweetnessTaste sweetnessTaste)
        {
            _context.SweetnessTastes.Remove(sweetnessTaste);
            await _context.SaveChangesAsync();
        }
        public async Task<SweetnessTaste> GetSweetnessTasteByIdAsync(int? id)
        {
            return await _context.SweetnessTastes.FindAsync(id);
        }
        public async Task<List<SweetnessTaste>> GetSweetnessTasteesByUserIdAsync(string userId)
        {
            return await _context.SweetnessTastes.Where(x => x.AppUserId == userId).ToListAsync();
        }
        public async Task<List<SweetnessTaste>> GetSweetnessTastesAsync()
        {
            return await _context.SweetnessTastes.Include(x=>x.Sweetnes).Include(x=>x.AppUser).ToListAsync();
        }
        public bool SweetnessTasteExists(int? id)
        {
            return _context.SweetnessTastes.Any(e => e.Id == id);
        }
        public async Task UpdateSweetnessTasteAsync(SweetnessTaste sweetnessTaste)
        {
            _context.Update(sweetnessTaste);
            await _context.SaveChangesAsync();
        }

    }
}
