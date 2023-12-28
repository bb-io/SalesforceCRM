using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Salesforce.Crm.Models.Requests;

public class UploadFileRequest
{
    public FileReference File { get; set; }

    public string? Filename { get; set; }

    public string Title { get; set; }
}