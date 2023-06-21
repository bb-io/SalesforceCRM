using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Salesforce.Dtos;

namespace Apps.Salesforce.Models.Responses
{
    public class ListAllContactsResponse
    {
        public IEnumerable<ContactDto> Records { get; set; }
    }
}
