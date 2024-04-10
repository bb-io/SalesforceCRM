using System.Net.Mime;
using RestSharp;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common;
using Apps.Salesforce.Crm.Dtos;
using Apps.Salesforce.Crm.Models.Requests;
using Apps.Salesforce.Crm.Models.Responses;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;

namespace Apps.Salesforce.Crm.Actions;

[ActionList]
public class FilesActions
{
    private readonly IFileManagementClient _fileManagementClient;

    public FilesActions(IFileManagementClient fileManagementClient)
    {
        _fileManagementClient = fileManagementClient;
    }

    [Action("List all files", Description = "List all files")]
    public ListAllFilesResponse ListAllFiles(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var query = "SELECT FIELDS(ALL) FROM ContentDocument LIMIT 200";
        var client = new SalesforceClient(authenticationCredentialsProviders);
        var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, authenticationCredentialsProviders);
        return client.Get<ListAllFilesResponse>(request);
    }

    [Action("Get file info", Description = "Get file info by id")]
    public FileInfoDto GetFileInfo(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetFileInfoRequest input)
    {
        var query = $"SELECT FIELDS(ALL) FROM ContentDocument WHERE Id = '{input.FileId}'";
        var client = new SalesforceClient(authenticationCredentialsProviders);
        var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, authenticationCredentialsProviders);
        return client.Get<ListAllFilesResponse>(request).Records.FirstOrDefault() ?? throw new("No file found with the provided ID");
    }

    [Action("Download file", Description = "Download file by id")]
    public async Task<DownloadFileResponse> DownloadFile(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] DownloadFileRequest input)
    {
        var client = new SalesforceClient(authenticationCredentialsProviders);
        var fileInfo = GetFileInfo(authenticationCredentialsProviders, new GetFileInfoRequest() { FileId = input.FileId });

        var contentVersionRequest = new SalesforceRequest($"services/data/v57.0/query?q=SELECT FIELDS(ALL) FROM ContentVersion WHERE Id = '{fileInfo.LatestPublishedVersionId}'", Method.Get, authenticationCredentialsProviders);
        var contentVersion = client.Get<RecordsCollectionDto<FileContentVersionDto>>(contentVersionRequest).Records.FirstOrDefault();

        var request = new SalesforceRequest($"services/data/v57.0/sobjects/ContentVersion/{fileInfo.LatestPublishedVersionId}/VersionData", Method.Get, authenticationCredentialsProviders);

        var response = client.Get(request);
        var fileContent = response.RawBytes!;

        using var stream = new MemoryStream(response.RawBytes!);
        var file = await _fileManagementClient.UploadAsync(stream, response.ContentType ?? MediaTypeNames.Application.Octet, contentVersion?.PathOnClient ?? string.Empty);
        return new() {
            File = file
        };
    }

    [Action("Upload file", Description = "Upload file")]
    public RecordIdDto UploadFile(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] UploadFileRequest input)
    {
        var client = new SalesforceClient(authenticationCredentialsProviders);
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/ContentVersion", Method.Post, authenticationCredentialsProviders);
        var fileBytes = _fileManagementClient.DownloadAsync(input.File).Result.GetByteData().Result;
        request.AddJsonBody(new
        {
            Title = input.Title,
            PathOnClient = input.Filename ?? input.File.Name,
            VersionData = Convert.ToBase64String(fileBytes)
        });
        return client.Execute<RecordIdDto>(request).Data;
    }

    [Action("Delete file", Description = "Delete file")]
    public void DeleteFile(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetFileInfoRequest input)
    {
        var client = new SalesforceClient(authenticationCredentialsProviders);
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/ContentDocument/{input.FileId}", Method.Delete, authenticationCredentialsProviders);
        client.Execute(request);
    }
}