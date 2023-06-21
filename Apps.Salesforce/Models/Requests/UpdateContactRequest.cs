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

        public string FieldName { get; set; }

        public string FieldValue { get; set; }
    }
}
