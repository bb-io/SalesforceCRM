using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Salesforce.Models.Requests
{
    public class UpdateContactRequest
    {
        public string Id { get; set; }

        [Display("Field name")]
        public string FieldName { get; set; }

        [Display("Field value")]
        public string FieldValue { get; set; }
    }
}
