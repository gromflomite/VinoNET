using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models
{
    public class AppUser: IdentityUser
    {

        [DataType(DataType.Text)]
        [MaxLength(25)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string Surname { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(60)]
        public string Address { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(35)]
        public string Company { get; set; }

    }
}
