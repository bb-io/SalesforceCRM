using Apps.Salesforce.Crm.Dtos;

namespace Apps.Salesforce.Crm.Models.Responses;

public class ListAllFilesResponse
{
    public IEnumerable<FileInfoDto> Records { get; set; }
}