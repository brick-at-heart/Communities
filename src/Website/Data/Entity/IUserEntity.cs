using System;

namespace BrickAtHeart.Communities.Data.Entity
{
    public interface IUserEntity
    {
        string City { get; set; }

        string Country { get; set; }

        DateTime DateOfBirth { get; set; }

        string DisplayName {get; set;}

        string Email { get; set; }

        bool EmailConfirmed { get; set; }

        string GivenName { get; set; }

        long Id { get; set; }

        bool IsActive { get; set; }

        bool IsApproved {get; set; }

        string NormalizedEmail { get; set; }

        string PhoneNumber { get; set; }

        bool PhoneNumberConfirmed { get; set; }

        string PostalCode { get; set; }

        string Region { get; set; }

        string StreetAddressLine1 { get; set; }

        string StreetAddressLine2 { get; set; }

        string SurName { get; set; }
    }
}
