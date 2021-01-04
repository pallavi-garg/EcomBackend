using Microsoft.AspNetCore.Identity;
using System;

namespace AuthServer.Infrastructure
{
    public class AppUser : IdentityUser
    {
        // Add additional profile data for application users by adding properties to this class
        public string Name { get; set; }

        public string LoginType { get; set; }
    }
}
