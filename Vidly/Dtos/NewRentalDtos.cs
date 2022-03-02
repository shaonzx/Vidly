using System.Collections.Generic;

namespace Vidly.Dtos
{
    public class NewRentalDtos
    {
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
    }
}