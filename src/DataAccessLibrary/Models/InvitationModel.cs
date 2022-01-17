using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class InvitationModel
    {
        public int Id { get; set; }
        public string SentByUserId { get; set; }
        public string SentToUserId { get; set; }
        public EventModel Event { get; set; }
        public bool Accepted { get; set; }
        public DateTime DateSent { get; set; }
        public bool IsActive { get; set; }
        public string Message { get; set; }
        public bool Responded { get; set; }

    }
}
