using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class PlaceModel
    {
        public int Id { get; set; }
        public CityModel City { get; set; }
    }
}
