using System;
using System.ComponentModel.DataAnnotations;

namespace BrickAtHeart.Communities.Areas.User.PageModels
{
    public class RegistrationPageModel
    {
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        public RegistrationPageModel()
        {
            DateOfBirth = DateTime.MinValue;
            Email = string.Empty;
            DisplayName = string.Empty;
        }
    }
}