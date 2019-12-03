using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wineapp.Models;

namespace Wineapp.Services
{
    public interface IFilters
    {
        public Task<List<Colour>> GetColourTastesAsync();
        public Task<Colour> GetColourTasteByIdAsync(int? id);

        public Task<List<Source>> GetSourceTastesAsync();
        public Task<Source> GetSourceTasteByIdAsync(int? id);

        public Task<List<Sweetnes>> GetSweetnesTastesAsync();
        public Task<Sweetnes> GetSweetnesTasteByIdAsync(int? id);
    }
}
