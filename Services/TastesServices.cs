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
        public bool ColourTasteExists(int? id)
        {
            return _context.ColourTastes.Any(e => e.Id == id);
        }

        public async Task CreateColourTasteAsync(ColourTaste colourTaste)
        {
            await _context.AddAsync(colourTaste);

            await _context.SaveChangesAsync();
        }

        public async Task CreateSourceTasteAsync(SourceTaste sourceTaste)
        {
            await _context.AddAsync(sourceTaste);

            await _context.SaveChangesAsync();
        }

        public async Task CreateSweetnessTasteAsync(SweetnessTaste sweetnessTaste)
        {
            await _context.AddAsync(sweetnessTaste);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteColourTasteAsync(ColourTaste colourTaste)
        {
            _context.ColourTastes.Remove(colourTaste);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSourceTasteAsync(SourceTaste sourceTaste)
        {
            _context.SourceTastes.Remove(sourceTaste);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSweetnessTasteAsync(SweetnessTaste sweetnessTaste)
        {
            _context.SweetnessTastes.Remove(sweetnessTaste);
            await _context.SaveChangesAsync();
        }

        public async Task<ColourTaste> GetColourTasteByIdAsync(int? id)
        {
            return await _context.ColourTastes.FindAsync(id);
        }

        public Task<List<ColourTaste>> GetColourTasteesByUserIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ColourTaste>> GetColourTastesAsync()
        {
            return await _context.ColourTastes.ToListAsync();
        }

        public Task<SourceTaste> GetSourceTasteByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SourceTaste>> GetSourceTasteesByUserIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<SourceTaste>> GetSourceTastesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SweetnessTaste> GetSweetnessTasteByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SweetnessTaste>> GetSweetnessTasteesByUserIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<SweetnessTaste>> GetSweetnessTastesAsync()
        {
            throw new NotImplementedException();
        }

        public bool SourceTasteExists(int? id)
        {
            throw new NotImplementedException();
        }

        public bool SweetnessTasteExists(int? id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateColourTasteAsync(ColourTaste colourTaste)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSourceTasteAsync(SourceTaste sourceTaste)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSweetnessTasteAsync(SweetnessTaste sweetnessTaste)
        {
            throw new NotImplementedException();
        }
    }
}
