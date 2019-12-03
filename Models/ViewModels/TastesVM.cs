using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models.ViewModels
{
    public class TastesVM
    {
        public ColourTaste ColourTaste { get; set; }
        public SourceTaste SourceTaste { get; set; }
        public SweetnessTaste SweetnessTaste { get; set; }
        public List<Colour> ListColours { get; set; }
        public List<Source> ListSources { get; set; }
        public List<Sweetnes> ListSweetness { get; set; }
        public List<ColourTaste> ColourTastes { get; set; }
        public List<SourceTaste> SourceTastes { get; set; }
        public List<SweetnessTaste> SweetnesTastes { get; set; }



    }
}
