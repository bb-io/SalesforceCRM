
using Blackbird.Applications.Sdk.Common;

namespace Apps.Salesforce.Crm.Dtos;

public class ContactDto
{
    [Display("Contact ID")]
    public string Id { get; set; }
    public string Name { get; set; }

    [Display("First name")]
    public string FirstName { get; set; }

    [Display("Last name")]
    public string LastName { get; set; }
}