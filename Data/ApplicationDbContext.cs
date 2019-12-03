using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wineapp.Models;

namespace Wineapp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Wineapp.Models.Wine> Wines { get; set; }
        public DbSet<Wineapp.Models.Colour> Colours { get; set; }
        public DbSet<Wineapp.Models.ColourTaste> ColourTastes { get; set; }
        public DbSet<Wineapp.Models.Source> Sources { get; set; }
        public DbSet<Wineapp.Models.SourceTaste> SourceTastes { get; set; }
        public DbSet<Wineapp.Models.Sweetnes> Sweetness { get; set; }
        public DbSet<Wineapp.Models.SweetnessTaste> SweetnessTastes { get; set; }
        public DbSet<Wineapp.Models.UserScore> UserScores { get; set; }
        public DbSet<Wineapp.Models.WineList> WineLists { get; set; }
        public DbSet<Wineapp.Models.WineListWine> WineListWines { get; set; }
    }
}
