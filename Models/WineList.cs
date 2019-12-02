﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models
{
    public class WineList
    {
        public int Id { get; set; }
        public string ListName { get; set; }
        public DateTime? ListDate { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<WineListWine>  WineListWines{ get; set; }

    }
}
