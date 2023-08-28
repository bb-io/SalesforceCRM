using Apps.Salesforce.Crm.Dtos;

namespace Apps.Salesforce.Crm.Models.Responses;

public class ListAllAccountsResponse
{
    public IEnumerable<AccountDto> Records { get; set; }
}