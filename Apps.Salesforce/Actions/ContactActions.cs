using Apps.Salesforce.Dtos;
using Apps.Salesforce.Models.Requests;
using Apps.Salesforce.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Salesforce.Actions
{
    [ActionList]
    public class ContactActions
    {
        [Action("List all contacts", Description = "List all contacts")]
        public ListAllContactsResponse ListAllContacts(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var query = "SELECT FIELDS(ALL) FROM Contact LIMIT 200";
            var client = new SalesforceClient(authenticationCredentialsProviders);
            var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, authenticationCredentialsProviders);
            return client.Get<ListAllContactsResponse>(request);
        }

        [Action("Get contact", Description = "Get contact by id")]
        public ContactDto? GetContact(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] GetContactRequest input)
        {
            var query = $"SELECT FIELDS(ALL) FROM Contact WHERE Id = '{input.Id}'";
            var client = new SalesforceClient(authenticationCredentialsProviders);
            var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, authenticationCredentialsProviders);
            return client.Get<ListAllContactsResponse>(request).Records.FirstOrDefault();
        }

        [Action("Create contact", Description = "Create contact")]
        public RecordIdDto? CreateContact(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] CreateContactRequest input)
        {
            var client = new SalesforceClient(authenticationCredentialsProviders);
            var request = new SalesforceRequest($"services/data/v57.0/sobjects/Contact", Method.Get, authenticationCredentialsProviders);
            request.AddJsonBody(input);
            return client.Execute<RecordIdDto>(request).Data;
        }

        [Action("Update contact field", Description = "Update contact field")]
        public void UpdateContact(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] UpdateContactRequest input)
        {
            var client = new SalesforceClient(authenticationCredentialsProviders);
            var request = new SalesforceRequest($"services/data/v57.0/sobjects/Contact/{input.Id}", Method.Patch, authenticationCredentialsProviders);
            var payload = new ExpandoObject();
            payload.TryAdd(input.FieldName, input.FieldValue);
            request.AddJsonBody(payload);
            client.Execute(request);
        }

        [Action("Delete contact", Description = "Delete contact by id")]
        public void DeleteContact(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] GetContactRequest input)
        {
            var client = new SalesforceClient(authenticationCredentialsProviders);
            var request = new SalesforceRequest($"services/data/v57.0/sobjects/Contact/{input.Id}", Method.Delete, authenticationCredentialsProviders);
            client.Execute(request);
        }
    }
}
