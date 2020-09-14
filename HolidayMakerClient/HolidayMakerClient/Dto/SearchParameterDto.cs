using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMakerClient.Dto
{
    class SearchParameterDto
    {

        public SearchParameterDto(string location, DateTimeOffset startDate, DateTimeOffset endDate, int numberOfGuests )
        {
            Location = location;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfGuests = numberOfGuests;
        }

        public string Location { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
