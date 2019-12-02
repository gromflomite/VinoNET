﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models
{
    public class Wine
    {
        public int Id { get; set; }

        public int? ColourId { get; set; }
        public Colour Colour { get; set; }
        public int? SweetnesId { get; set; }
        public Sweetnes Sweetnes { get; set; }
        public int? SourceId { get; set; }
        public Source Source { get; set; }
        public List<WineListWine>  wineListWines{ get; set; }

    }
}
