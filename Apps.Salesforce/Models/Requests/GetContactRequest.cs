using Apps.Salesforce.Crm.DataSourceHandler;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Salesforce.Crm.Models.Requests;

public class GetContactRequest
{
    [Display("Contact ID")]
    [DataSource(typeof(ContactDataHandler))]
    public string Id { get; set; }
}