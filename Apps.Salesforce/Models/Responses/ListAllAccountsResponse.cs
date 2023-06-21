using Apps.Salesforce.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Salesforce.Models.Responses
{
    public class ListAllAccountsResponse
    {
        public IEnumerable<AccountDto> Records { get; set; }
    }
}
