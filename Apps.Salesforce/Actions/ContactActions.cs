using Apps.Salesforce.Crm.Dtos;
using Apps.Salesforce.Crm.Models.Requests;
using Apps.Salesforce.Crm.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using System.Dynamic;

namespace Apps.Salesforce.Crm.Actions;

[ActionList("Contact")]
public class ContactActions(InvocationContext invocationContext) : Invocable(invocationContext)
{
    [Action("Search all contacts", Description = "Search all contacts")]
    public async Task<ListAllContactsResponse> ListAllContacts()
    {
        var query = "SELECT FIELDS(ALL) FROM Contact LIMIT 200";
        var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, Creds);
        return await Client.ExecuteWithErrorHandling<ListAllContactsResponse>(request);
    }

    [Action("Get contact", Description = "Get contact by id")]
    public async Task<ContactDto> GetContact([ActionParameter] GetContactRequest input)
    {
        var query = $"SELECT FIELDS(ALL) FROM Contact WHERE Id = '{input.Id}'";
        var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<ListAllContactsResponse>(request);

        return new() { Id = response.Records.FirstOrDefault().Id, Name = response.Records.FirstOrDefault().Name };
    }

    [Action("Create contact", Description = "Create contact")]
    public async Task<RecordIdDto> CreateContact([ActionParameter] CreateContactRequest input)
    {
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/Contact", Method.Post, Creds);
        request.AddJsonBody(input);
        return await Client.ExecuteWithErrorHandling<RecordIdDto>(request);
    }

    [Action("Update contact field", Description = "Update contact field")]
    public async Task UpdateContact([ActionParameter] UpdateContactRequest input)
    {
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/Contact/{input.Id}", Method.Patch, Creds);
        var payload = new ExpandoObject();
        payload.TryAdd(input.FieldName, input.FieldValue);
        request.AddJsonBody(payload);
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Delete contact", Description = "Delete contact by id")]
    public async Task DeleteContact([ActionParameter] GetContactRequest input)
    {
        var request = new SalesforceRequest($"services/data/v57.0/sobjects/Contact/{input.Id}", Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }
}