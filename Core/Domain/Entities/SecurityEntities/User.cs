global using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.SecurityEntities
{
    public class User : IdentityUser // kda kda be inhert mn identity user ely el TKey beta3ha string (3shan el Id Guid) 
    {
        public string DisplayName { get; set; } // Full Name

        public Address Address { get; set; }
    }
}
