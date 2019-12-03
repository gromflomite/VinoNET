using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models
{
    public class WineList
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string ListName { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(140)]
        public string Description { get; set; }
        public DateTime? ListDate { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<WineListWine>  WineListWines { get; set; }

    }
}
