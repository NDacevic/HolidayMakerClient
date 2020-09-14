using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMakerClient.Dto
{
    class SearchParameterDto
    {

        public SearchParameterDto(string location, DateTime startDate, DateTime endDate, int numberOfGuests )
        {
            Location = location;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfGuests = numberOfGuests;
        }

        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
