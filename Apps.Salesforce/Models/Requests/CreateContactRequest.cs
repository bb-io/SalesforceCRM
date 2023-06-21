using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Salesforce.Models.Requests
{
    public class CreateContactRequest
    {
        [Display("Last name")]
        public string LastName { get; set; }

        public string Salutation { get; set; }
    }
}
