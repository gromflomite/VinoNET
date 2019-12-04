using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models.ViewModels
{
    public class WineVM
    {
        public Wine Wine { get; set; }
        public List<Wine> ListWineTaste { get; set; }
        public List<Wine> ListWineUserScore { get; set; }

    }
}
