using Apps.Salesforce.Crm.Dtos;

namespace Apps.Salesforce.Crm.Models.Responses;

public class ListAllContactsResponse
{
    public IEnumerable<ContactDto> Records { get; set; }
}