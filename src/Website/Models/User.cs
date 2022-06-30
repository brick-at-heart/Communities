using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace BrickAtHeart.Communities.Models
{
    public class User : IdentityUser<long>
    {
        [PersonalData]
        public string City { get; set; }

        [PersonalData]
        public string Country { get; set; }

        [PersonalData]
        public DateTime DateOfBirth { get; set; }

        [PersonalData]
        public string DisplayName { get; set; }

        [PersonalData]
        public string GivenName { get; set; }

        public bool IsActive { get; set; }

        public bool IsApproved { get; set; }

        [PersonalData]
        public string PostalCode { get; set; }

        [PersonalData]
        public string Region { get; set; }

        [PersonalData]
        public string StreetAddressLine1 { get; set; }

        [PersonalData]
        public string StreetAddressLine2 { get; set; }

        [PersonalData]
        public string SurName { get; set; }

        public IList<Claim> Claims { get; set; } = new List<Claim>();

        public User(string displayName)
        {
            DisplayName = displayName;
        }
    }
}