using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Salesforce.Crm.Models.Requests
{
    public class GetFileInfoRequest
    {
        [Display("File ID")]
        public string FileId { get; set; }
    }
}
