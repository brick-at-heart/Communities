using BrickAtHeart.Communities.Models.Events;
using System;
using System.ComponentModel.DataAnnotations;

namespace BrickAtHeart.Communities.Areas.Community.PageModels
{
    public class EventPageModel
    {
        public string CommunityDisplayName { get; set; }

        public long CommunityId { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public double? EndTime { get; set; }

        public long EventId { get; set; }

        public long EventScheduleId { get; set; }

        [Required]
        [Display(Name ="Name")]
        [MaxLength(256)]
        public string EventName { get; set; }

        [MaxLength(512)]
        public string Location { get; set; }

        public long Owner { get; set;}

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public double? StartTime { get; set; } 

        public EventStatus Status { get; set; }
    }
}
