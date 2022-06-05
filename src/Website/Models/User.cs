using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace BrickAtHeart.Communities.Models
{
    public class User : IdentityUser<long>
    {
        public string? City { get; set; }

        public string? Country { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string DisplayName { get; set; }

        public string? GivenName { get; set; }

        public bool IsActive { get; set; }

        public bool IsApproved { get; set; }

        public string? PostalCode { get; set; }

        public string? Region { get; set; }

        public string? StreetAddressLine1 { get; set; }

        public string? StreetAddressLine2 { get; set; }

        public string? SurName { get; set; }

        public IList<Claim> Claims { get; set; } = new List<Claim>();

        public User(string displayName)
        {
            DisplayName = displayName;
        }
    }
}