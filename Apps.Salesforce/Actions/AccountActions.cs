using System.Dynamic;
using Apps.Salesforce.Crm.Dtos;
using Apps.Salesforce.Crm.Models.Requests;
using Apps.Salesforce.Crm.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.Salesforce.Crm.Actions;

[ActionList("Account")]
public class AccountActions(InvocationContext invocationContext): Invocable(invocationContext)
{
    [Action("Search all accounts", Description = "List all accounts")]
    public ListAllAccountsResponse ListAllAccounts()
    {
        var query = "SELECT FIELDS(ALL) FROM Account LIMIT 200";
        var client = new SalesforceClient(Creds);
        var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, Creds);
        return client.Get<ListAllAccountsResponse>(request);
    }

    [Action("Get account", Description = "Get account by id")]
    public AccountDto? GetAccount([ActionParameter] GetAccountRequest input)
    {
        var query = $"SELECT FIELDS(ALL) FROM Account WHERE Id = '{input.Id}'";
        var client = new SalesforceClient(Creds);
        var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, Creds);
        return client.Get<ListAllAccountsResponse>(request).Records.FirstOrDefault();
    }

    [Action("Create account", Description = "Create account")]
    public RecordIdDto? CreateAccount([ActionParameter] CreateAccountRequest input)
    {
        var client = new SalesforceClient(Creds);
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/Account", Method.Post, Creds);
        request.AddJsonBody(input);
        return client.Execute<RecordIdDto>(request).Data;
    }

    [Action("Update account field", Description = "Update account field")]
    public void UpdateAccount(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] UpdateContactRequest input)
    {
        var client = new SalesforceClient(Creds);
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/Account/{input.Id}", Method.Patch, Creds);
        var payload = new ExpandoObject();
        payload.TryAdd(input.FieldName, input.FieldValue);
        request.AddJsonBody(payload);
        client.Execute(request);
    }

    [Action("Delete account", Description = "Delete account by id")]
    public void DeleteAccount(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetAccountRequest input)
    {
        var client = new SalesforceClient(Creds);
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/Account/{input.Id}", Method.Delete, Creds);
        client.Execute(request);
    }

    [Action("DEBUG: Get auth data", Description = "Can be used only for debugging purposes.")]
    public IEnumerable<AuthenticationCredentialsProvider> GetAuthenticationCredentialsProviders()
    {
        return Creds;
    }
}