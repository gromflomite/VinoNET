using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models
{
    public class ColourTaste
    {
        public int Id { get; set; }

        public int Score { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ColourId { get; set; }
        public Colour Colour { get; set; }
    }
}
