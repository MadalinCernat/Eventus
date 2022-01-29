using System.ComponentModel.DataAnnotations;

namespace EventusUI.Models
{
    public class EventUIModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string PlaceId { get; set; }
        public string PlaceName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDateTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDateTime { get; set; }

        [Required]
        public decimal EntranceFee { get; set; }
        public string CreatedByUserId { get; set; }

        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
        public bool IsOver { get; set; }

        [Url]
        public string? Url { get; set; }
    }
}
