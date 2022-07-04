using System;

namespace BrickAtHeart.Communities.Data.Entity
{
    public class UserEntity : IUserEntity
    {
        public string City { get; set; }

        public string Country { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string GivenName { get; set; }

        public long Id { get; set; }

        public bool IsActive { get; set; }

        public bool IsApproved { get; set;}

        public string NormalizedEmail { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string PostalCode { get; set; }

        public string Region { get; set; }

        public string StreetAddressLine1 { get; set; }

        public string StreetAddressLine2 { get; set; }

        public string SurName { get; set; }
    }
}
