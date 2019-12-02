using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models
{
    public class WineListWine
    {
        public int Id { get; set; }
        public int WineListId { get; set; }
        public WineList WineList{ get; set; }
        public int WineId { get; set; }
        public Wine Wine { get; set; }

    }
}
