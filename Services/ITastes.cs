using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wineapp.Models;

namespace Wineapp.Services
{
    public interface ITastes
    {
        //COLOUR TASTE
        public Task<List<ColourTaste>> GetColourTasteesByUserIdAsync();
        public Task<List<ColourTaste>> GetColourTastesAsync();
        public Task<ColourTaste> GetColourTasteByIdAsync(int? id);
        public Task CreateColourTasteAsync(ColourTaste colourTaste);
        public Task UpdateColourTasteAsync(ColourTaste colourTaste);
        public Task DeleteColourTasteAsync(ColourTaste colourTaste);
        public bool ColourTasteExists(int? id);

        //SOURCE TASTE
        public Task<List<SourceTaste>> GetSourceTasteesByUserIdAsync();
        public Task<List<SourceTaste>> GetSourceTastesAsync();
        public Task<SourceTaste> GetSourceTasteByIdAsync(int? id);
        public Task CreateSourceTasteAsync(SourceTaste sourceTaste);
        public Task UpdateSourceTasteAsync(SourceTaste sourceTaste);
        public Task DeleteSourceTasteAsync(SourceTaste sourceTaste);
        public bool SourceTasteExists(int? id);

        //SWEETNESS TASTE
        public Task<List<SweetnessTaste>> GetSweetnessTasteesByUserIdAsync();
        public Task<List<SweetnessTaste>> GetSweetnessTastesAsync();
        public Task<SweetnessTaste> GetSweetnessTasteByIdAsync(int? id);
        public Task CreateSweetnessTasteAsync(SweetnessTaste sweetnessTaste);
        public Task UpdateSweetnessTasteAsync(SweetnessTaste sweetnessTaste);
        public Task DeleteSweetnessTasteAsync(SweetnessTaste sweetnessTaste);
        public bool SweetnessTasteExists(int? id);

    }
}
