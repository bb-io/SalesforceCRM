using Apps.Salesforce.Crm.Dtos;
using Apps.Salesforce.Crm.Models.Requests;
using Apps.Salesforce.Crm.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;
using System.Net.Mime;

namespace Apps.Salesforce.Crm.Actions;

[ActionList("File")]
public class FilesActions(InvocationContext invocationContext, IFileManagementClient _fileManagementClient) : Invocable(invocationContext)
{
    [Action("Search all files", Description = "Search all files")]
    public async Task<ListAllFilesResponse> ListAllFiles()
    {
        var query = "SELECT FIELDS(ALL) FROM ContentDocument LIMIT 200";
        var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, Creds);
        return await Client.ExecuteWithErrorHandling<ListAllFilesResponse>(request);
    }

    [Action("Get file info", Description = "Get file info by id")]
    public async Task<FileInfoDto> GetFileInfo([ActionParameter] GetFileInfoRequest input)
    {
        var query = $"SELECT FIELDS(ALL) FROM ContentDocument WHERE Id = '{input.FileId}'";
        var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<ListAllFilesResponse>(request);
        var fileInfo = response.Records.FirstOrDefault();

        return fileInfo;
    }

    [Action("Download file", Description = "Download file by id")]
    public async Task<DownloadFileResponse> DownloadFile([ActionParameter] DownloadFileRequest input)
    {
        var fileInfo = await GetFileInfo(new GetFileInfoRequest() { FileId = input.FileId });

        var contentVersionRequest = new SalesforceRequest($"services/data/v57.0/query?q=SELECT FIELDS(ALL) FROM ContentVersion WHERE Id = '{fileInfo.LatestPublishedVersionId}'", Method.Get, Creds);
        var contentVersion = await Client.ExecuteWithErrorHandling<RecordsCollectionDto<FileContentVersionDto>>(contentVersionRequest);
        var content = contentVersion?.Records.FirstOrDefault();

        var request = new SalesforceRequest($"services/data/v57.0/sobjects/ContentVersion/{fileInfo.LatestPublishedVersionId}/VersionData", Method.Get, Creds);

        var response = await Client.ExecuteWithErrorHandling(request);
        var fileContent = response.RawBytes!;

        using var stream = new MemoryStream(response.RawBytes!);
        var file = await _fileManagementClient.UploadAsync(stream, response.ContentType ?? MediaTypeNames.Application.Octet, content?.PathOnClient ?? string.Empty);
        return new()
        {
            File = file
        };
    }

    [Action("Upload file", Description = "Upload file")]
    public async Task<RecordIdDto> UploadFile([ActionParameter] UploadFileRequest input)
    {
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/ContentVersion", Method.Post, Creds);
        var fileBytes = _fileManagementClient.DownloadAsync(input.File).Result.GetByteData().Result;
        request.AddJsonBody(new
        {
            Title = input.Title,
            PathOnClient = input.Filename ?? input.File.Name,
            VersionData = Convert.ToBase64String(fileBytes)
        });
        return await Client.ExecuteWithErrorHandling<RecordIdDto>(request);
    }

    [Action("Delete file", Description = "Delete file")]
    public async Task DeleteFile([ActionParameter] GetFileInfoRequest input)
    {
        var client = new SalesforceClient(Creds);
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/ContentDocument/{input.FileId}", Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }
}