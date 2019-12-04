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
        public List<Source> ListSourceTaste { get; set; }

        public ColourTaste colourTaste { get; set; }
        public SourceTaste sourceTaste { get; set; }
        public SweetnessTaste sweetnessTaste { get; set; }
    }
}
