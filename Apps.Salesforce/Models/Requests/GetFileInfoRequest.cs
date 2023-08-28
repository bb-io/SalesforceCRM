using Blackbird.Applications.Sdk.Common;

namespace Apps.Salesforce.Crm.Models.Requests;

public class GetFileInfoRequest
{
    [Display("File ID")]
    public string FileId { get; set; }
}