using Apps.Salesforce.Crm.Dtos;
using Apps.Salesforce.Crm.Models.Requests;
using Apps.Salesforce.Crm.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using System.Dynamic;

namespace Apps.Salesforce.Crm.Actions;

[ActionList("Account")]
public class AccountActions(InvocationContext invocationContext) : Invocable(invocationContext)
{
    [Action("Search all accounts", Description = "Search all accounts")]
    public async Task<ListAllAccountsResponse> ListAllAccounts()
    {
        var query = "SELECT FIELDS(ALL) FROM Account LIMIT 200";
        var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, Creds);
        return await Client.ExecuteWithErrorHandling<ListAllAccountsResponse>(request);
    }

    [Action("Get account", Description = "Get account by id")]
    public async Task<AccountDto> GetAccount([ActionParameter] GetAccountRequest input)
    {
        var query = $"SELECT FIELDS(ALL) FROM Account WHERE Id = '{input.Id}'";
        var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<ListAllAccountsResponse>(request);

        return new() { Id = response.Records.FirstOrDefault().Id, Name = response.Records.FirstOrDefault().Name };
    }

    [Action("Create account", Description = "Create account")]
    public async Task<RecordIdDto> CreateAccount([ActionParameter] CreateAccountRequest input)
    {
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/Account", Method.Post, Creds);
        request.AddJsonBody(input);
        var response = await Client.ExecuteWithErrorHandling<RecordIdDto>(request);

        return new RecordIdDto { Id = response.Id };
    }

    [Action("Update account field", Description = "Update account field")]
    public async Task UpdateAccount([ActionParameter] UpdateAccountRequest input)
    {
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/Account/{input.Id}", Method.Patch, Creds);
        var payload = new ExpandoObject();
        payload.TryAdd(input.FieldName, input.FieldValue);
        request.AddJsonBody(payload);
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Delete account", Description = "Delete account by id")]
    public async Task DeleteAccount([ActionParameter] GetAccountRequest input)
    {
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/Account/{input.Id}", Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("DEBUG: Get auth data", Description = "Can be used only for debugging purposes.")]
    public IEnumerable<AuthenticationCredentialsProvider> GetAuthenticationCredentialsProviders()
    {
        return Creds;
    }
}