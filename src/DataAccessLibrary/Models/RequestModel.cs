using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class RequestModel
    {
        public int Id { get; set; }
        public string SentByUserId { get; set; }
        public EventModel Event { get; set; }
        public string RequestMessage { get; set; }
        public DateTime Date { get; set; }
        public bool Accepted { get; set; }
    }
}
