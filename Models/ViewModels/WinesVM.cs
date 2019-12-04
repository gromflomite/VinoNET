using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models.ViewModels
{
    public class WinesVM
    {
        public Wine Wine { get; set; }
        public Source Source { get; set; }
        public List<Wine> ListWineTaste { get; set; }
        public List<Wine> ListWineUserScore { get; set; }    

        public List< ColourTaste> ListUserColourTaste { get; set; }
        public List<SourceTaste> ListUserSourceTaste { get; set; }
        public List<SweetnessTaste> ListUserSweetnessTaste { get; set; }

        public List<Wine> ListWinesTastesDetails { get; set; }
    }
}
