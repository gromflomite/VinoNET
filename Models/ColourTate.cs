using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models
{
    public class ColourTate
    {
        public int Id { get; set; }
        public int ColourId { get; set; }
        public Colour Colour { get; set; }
    }
}
