using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BrickAtHeart.Communities.Areas.User.ViewModels
{
    public class ProfilePageModel
    {
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        [ReadOnly(true)]
        public string Email { get; set; }

        [Display(Name = "Family Name")]
        public string SurName { get; set; }

        [Display(Name = "Given Name")]
        public string GivenName { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "State")]
        public string Region { get; set; }

        [Display(Name = "Address")]
        public string StreetAddressLine1 { get; set; }

        [Display(Name = "Address 2")]
        public string StreetAddressLine2 { get; set; }
    }
}