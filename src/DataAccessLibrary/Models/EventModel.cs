using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PlaceModel Place { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public decimal EntranceFee { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
        public string Url { get; set; }
        public bool AllowRequests { get; set; }
    }
}
