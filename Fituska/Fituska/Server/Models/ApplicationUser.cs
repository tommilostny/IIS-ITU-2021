using Microsoft.AspNetCore.Identity;
using System;

namespace Fituska.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string DiscordUserName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime LastLoginDate { get; set; }

        public string PhotoID { get; set; }
    }
}
