﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models.ViewModels
{
    public class WinesVM
    {
        public AppUser AppUser { get; set; }
        public Wine Wine { get; set; }
        public Source Source { get; set; }
        public WineListWine WineListWine { get; set; }
        public WineList WineList { get; set; }
        public List<Source> ListSources { get; set; }

        //Lista de vinos aplicando los filtros adaptados a cada usuario
        public List<Wine> ListWineTaste { get; set; }

        //Lista de vinos segun su puntuación
        public List<Wine> ListWineUserScore { get; set; }

        //Lista de las tablas Tastes según el usuario
        public List<ColourTaste> ListUserColourTaste { get; set; }
        public List<SourceTaste> ListUserSourceTaste { get; set; }
        public List<SweetnessTaste> ListUserSweetnessTaste { get; set; }

        //Lista de vinos Tastes in View Details--> WinesController 
        public List<Wine> ListWinesTastesDetails { get; set; }

        //Lista de vinos Tastes in View Sources--> WinesController 
        public List<Wine> ListWinesTastesSources { get; set; }

        public List<WineList> ListWinesLists { get; set; }
        public List<WineListWine> ListWinesListWines { get; set; }
        public bool WinelistsWineExit { get; set; }

        //Lista de vinos patrocinados
        public List<Wine> SponsoredWines { get; set; }


    }
}
