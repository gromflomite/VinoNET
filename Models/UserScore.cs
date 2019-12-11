using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models
{
    public class UserScore
    {
        public int Id { get; set; }
        public int VoteValue { get; set; }
        public DateTime VoteDate { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int WineId { get; set; }
        public Wine Wine { get; set; }


    }
}
