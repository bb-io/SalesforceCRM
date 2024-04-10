using Apps.Salesforce.Crm.DataSourceHandler;
using Apps.Salesforce.Crm.DataSourceHandler.EnumDataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Salesforce.Crm.Models.Requests;

public class CreateContactRequest
{
    [Display("Last name")] public string LastName { get; set; }

    public string? Salutation { get; set; }

    public string? Description { get; set; }
    
    public string? Title { get; set; }

    public string? Department { get; set; }
    
    public string? Email { get; set; }

    public DateTime? Birthdate { get; set; }

    [Display("Account ID")]
    [DataSource(typeof(AccountDataHandler))]
    public string? AccountId { get; set; }

    [Display("Assistant name")] public string? AssistantName { get; set; }

    [Display("Assistant phone")] public string? AssistantPhone { get; set; }

    [Display("Can allow portal self-register")]
    public bool? CanAllowPortalSelfReg { get; set; }

    [Display("Do not call")] public bool? DoNotCall { get; set; }

    [Display("Clean status")]
    [StaticDataSource(typeof(CleanStatusDataHandler))]
    public string? CleanStatus { get; set; }

    [Display("Connection received ID")] public string? ConnectionReceivedId { get; set; }

    [Display("Connection sent ID")] public string? ConnectionSentId { get; set; }
}