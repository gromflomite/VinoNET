using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models
{
    public class SweetnessTaste
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int SweetnesId { get; set; }
        public Sweetnes Sweetnes { get; set; }

    }
}
