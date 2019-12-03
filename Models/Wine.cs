using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public List<WineListWine>  WineListWines{ get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string Name { get; set; }
        public double? Price { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string Company { get; set; }
        public bool Sparkling { get; set; }

        [MaxLength(4)]
        public int Year { get; set; }
        public bool Publish { get; set; }

    }
}
