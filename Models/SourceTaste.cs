using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models
{
    public class SourceTaste
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int SourceId { get; set; }
        public Source Source { get; set; }
    }
}
