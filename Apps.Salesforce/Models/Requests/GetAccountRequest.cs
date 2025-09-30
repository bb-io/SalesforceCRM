using Apps.Salesforce.Crm.DataSourceHandler;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Salesforce.Crm.Models.Requests;

public class GetAccountRequest
{
    [Display("Account ID")]
    [DataSource(typeof(AccountDataHandler))]
    public string Id { get; set; }
}