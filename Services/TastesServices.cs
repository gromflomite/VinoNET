using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;

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
            return await _context.ColourTastes.Where(x => x.AppUserId == userId).Include(x=>x.Colour).ToListAsync();
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
            return await _context.SourceTastes.Where(x => x.AppUserId == userId).Include(x=>x.Source).ToListAsync();
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
            return await _context.SweetnessTastes.Where(x => x.AppUserId == userId).Include(x=>x.Sweetnes).ToListAsync();
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



        //Inserta los valores en las tablas Tastes cuando el usuario hace click, like,favorito
        //el valor base es +1,+3,+4, respectivamente
        //el valor que se reduce de manera exponencial cuando la puntuación total supera los 10 puntos,
        public async Task InsertClickValues(int? colourId, int? sourceId, int? sweetId, int value, string userId)
        {
            
            List<ColourTaste> colourTastes = await GetColourTasteesByUserIdAsync(userId);
            List<SourceTaste> sourceTastes = await GetSourceTasteesByUserIdAsync(userId);
            List<SweetnessTaste> sweetnessTastes = await GetSweetnessTasteesByUserIdAsync(userId);

            if (colourId != 0)
            {
                ColourTaste colourTaste = colourTastes.FirstOrDefault(x => x.ColourId == colourId);
                if (colourTaste.Score < 10)
                {
                    colourTaste.Score += value;
                    await UpdateColourTasteAsync(colourTaste);
                }
                else if (colourTaste.Score >= 10)
                {
                    double root = Convert.ToDouble(colourTaste.Score.ToString().Substring(0, 1)) + 1;
                    double addScore = Math.Sqrt(value / (Math.Pow(root, root)));
                    colourTaste.Score += addScore;
                }
            }

            if (sourceId != 0)
            {
                SourceTaste sourceTaste = sourceTastes.FirstOrDefault(x => x.SourceId == sourceId);

                if (sourceTaste.Score < 10)
                {
                    sourceTaste.Score += value;
                    await UpdateSourceTasteAsync(sourceTaste);
                }
                else if (sourceTaste.Score >= 10)
                {
                    double root = Convert.ToDouble(sourceTaste.Score.ToString().Substring(0, 1)) + 1;
                    double addScore = Math.Sqrt(value / (Math.Pow(root, root)));
                    sourceTaste.Score += addScore;
                    await UpdateSourceTasteAsync(sourceTaste);
                }
            }

            if (sweetId != 0)
            {
                SweetnessTaste sweetnessTaste = sweetnessTastes.FirstOrDefault(x => x.SweetnesId == sweetId);

                if (sweetnessTaste.Score < 10)
                {
                    sweetnessTaste.Score += value;
                    await UpdateSweetnessTasteAsync(sweetnessTaste);
                }
                else if (sweetnessTaste.Score >= 10)
                {
                    double root = Convert.ToDouble(sweetnessTaste.Score.ToString().Substring(0, 1)) + 1;
                    double addScore = Math.Sqrt(value / (Math.Pow(root, root)));
                    sweetnessTaste.Score += addScore;
                    await UpdateSweetnessTasteAsync(sweetnessTaste);
                }
            }
          
        }

        public async Task DelateLikeValues(int? colourId, int? sourceId, int? sweetId, string userId)
        {
           
            List<ColourTaste> colourTastes = await GetColourTasteesByUserIdAsync(userId);
            List<SourceTaste> sourceTastes = await GetSourceTasteesByUserIdAsync(userId);
            List<SweetnessTaste> sweetnessTastes = await GetSweetnessTasteesByUserIdAsync(userId);

            if (colourId != 0)
            {
                ColourTaste colourTaste = colourTastes.FirstOrDefault(x => x.ColourId == colourId);
                if (colourTaste.Score < 10)
                {
                    colourTaste.Score -= 3;
                    await UpdateColourTasteAsync(colourTaste);
                }
                else if (colourTaste.Score >= 10)
                {
                    double root = Convert.ToDouble(colourTaste.Score.ToString().Substring(0, 1)) + 1;
                    double addScore = Math.Sqrt(3 / (Math.Pow(root, root)));
                    colourTaste.Score -= addScore;
                }
            }

            if (sourceId != 0)
            {
                SourceTaste sourceTaste = sourceTastes.FirstOrDefault(x => x.SourceId == sourceId);

                if (sourceTaste.Score < 10)
                {
                    sourceTaste.Score -= 3;
                    await UpdateSourceTasteAsync(sourceTaste);
                }
                else if (sourceTaste.Score >= 10)
                {
                    double root = Convert.ToDouble(sourceTaste.Score.ToString().Substring(0, 1)) + 1;
                    double addScore = Math.Sqrt(3 / (Math.Pow(root, root)));
                    sourceTaste.Score -= addScore;
                    await UpdateSourceTasteAsync(sourceTaste);
                }
            }

            if (sweetId != 0)
            {
                SweetnessTaste sweetnessTaste = sweetnessTastes.FirstOrDefault(x => x.SweetnesId == sweetId);

                if (sweetnessTaste.Score < 10)
                {
                    sweetnessTaste.Score -= 3;
                    await UpdateSweetnessTasteAsync(sweetnessTaste);
                }
                else if (sweetnessTaste.Score >= 10)
                {
                    double root = Convert.ToDouble(sweetnessTaste.Score.ToString().Substring(0, 1)) + 1;
                    double addScore = Math.Sqrt(3 / (Math.Pow(root, root)));
                    sweetnessTaste.Score -= addScore;
                    await UpdateSweetnessTasteAsync(sweetnessTaste);
                }
            }

        }





    }
}
