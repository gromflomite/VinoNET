using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wineapp.Models;

namespace Wineapp.Services
{
    public interface IFilters
    {
        public Task<List<Colour>> GetColourAsync();
        public Task<Colour> GetColourByIdAsync(int? id);

        public Task<List<Source>> GetSourceAsync();
        public Task<Source> GetSourceByIdAsync(int? id);

        public Task<List<Sweetnes>> GetSweetnesAsync();
        public Task<Sweetnes> GetSweetnesByIdAsync(int? id);
    }
}
