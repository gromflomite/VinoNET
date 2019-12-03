using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models
{
    public class Source
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string SourceType { get; set; }
        public string Province { get; set; }
        public string Image { get; set; }
    }
}
