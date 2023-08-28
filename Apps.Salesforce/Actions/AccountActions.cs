using System.Dynamic;
using Apps.Salesforce.Crm.Dtos;
using Apps.Salesforce.Crm.Models.Requests;
using Apps.Salesforce.Crm.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Salesforce.Crm.Actions;

[ActionList]
public class AccountActions
{
    [Action("List all accounts", Description = "List all accounts")]
    public ListAllAccountsResponse ListAllAccounts(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var query = "SELECT FIELDS(ALL) FROM Account LIMIT 200";
        var client = new SalesforceClient(authenticationCredentialsProviders);
        var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, authenticationCredentialsProviders);
        return client.Get<ListAllAccountsResponse>(request);
    }

    [Action("Get account", Description = "Get account by id")]
    public AccountDto? GetAccount(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetAccountRequest input)
    {
        var query = $"SELECT FIELDS(ALL) FROM Account WHERE Id = '{input.Id}'";
        var client = new SalesforceClient(authenticationCredentialsProviders);
        var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, authenticationCredentialsProviders);
        return client.Get<ListAllAccountsResponse>(request).Records.FirstOrDefault();
    }

    [Action("Create account", Description = "Create account")]
    public RecordIdDto? CreateAccount(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] CreateAccountRequest input)
    {
        var client = new SalesforceClient(authenticationCredentialsProviders);
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/Account", Method.Post, authenticationCredentialsProviders);
        request.AddJsonBody(input);
        return client.Execute<RecordIdDto>(request).Data;
    }

    [Action("Update account field", Description = "Update account field")]
    public void UpdateAccount(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] UpdateContactRequest input)
    {
        var client = new SalesforceClient(authenticationCredentialsProviders);
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/Account/{input.Id}", Method.Patch, authenticationCredentialsProviders);
        var payload = new ExpandoObject();
        payload.TryAdd(input.FieldName, input.FieldValue);
        request.AddJsonBody(payload);
        client.Execute(request);
    }

    [Action("Delete account", Description = "Delete account by id")]
    public void DeleteAccount(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] GetAccountRequest input)
    {
        var client = new SalesforceClient(authenticationCredentialsProviders);
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/Account/{input.Id}", Method.Delete, authenticationCredentialsProviders);
        client.Execute(request);
    }
}