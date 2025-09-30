using Blackbird.Applications.Sdk.Common;

namespace Apps.Salesforce.Crm.Dtos;

public class AccountDto
{
    [Display("Account ID")]
    public string Id { get; set; }
    public string Name { get; set; }
}