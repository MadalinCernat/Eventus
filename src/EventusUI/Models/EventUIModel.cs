using EventusUI.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace EventusUI.Models
{
    public class EventUIModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please choose a place for your event.")]
        public int PlaceId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [FromNow("Start Date Time must be past now.")]
        public DateTime StartDateTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [FromNow("End Date Time must be past now.")]
        public DateTime EndDateTime { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal EntranceFee { get; set; }

        [Url]
        public string Url { get; set; }

        public bool AllowRequests { get; set; } = false;
    }
}
