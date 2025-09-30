using Apps.Salesforce.Crm.DataSourceHandler;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Salesforce.Crm.Models.Requests
{
    public class UpdateAccountRequest
    {
        [Display("Account ID")]
        [DataSource(typeof(AccountDataHandler))]
        public string Id { get; set; }

        [Display("Field name")]
        public string FieldName { get; set; }

        [Display("Field value")]
        public string FieldValue { get; set; }
    }
}
