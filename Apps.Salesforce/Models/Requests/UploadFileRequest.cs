using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.Salesforce.Crm.Models.Requests;

public class UploadFileRequest
{
    public File File { get; set; }

    public string? Filename { get; set; }

    public string Title { get; set; }
}