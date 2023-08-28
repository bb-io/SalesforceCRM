using Blackbird.Applications.Sdk.Common;

namespace Apps.Salesforce.Crm.Models.Requests;

public class UpdateContactRequest
{
    public string Id { get; set; }

    [Display("Field name")]
    public string FieldName { get; set; }

    [Display("Field value")]
    public string FieldValue { get; set; }
}