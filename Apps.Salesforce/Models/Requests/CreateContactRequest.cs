using Blackbird.Applications.Sdk.Common;

namespace Apps.Salesforce.Crm.Models.Requests;

public class CreateContactRequest
{
    [Display("Last name")]
    public string LastName { get; set; }

    public string Salutation { get; set; }
}