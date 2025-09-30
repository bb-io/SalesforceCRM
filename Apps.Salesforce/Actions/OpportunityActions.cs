using Apps.Salesforce.Crm.Models.Requests;
using Apps.Salesforce.Crm.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using System.Dynamic;

namespace Apps.Salesforce.Crm.Actions
{
    [ActionList("Opportunity")]
    public class OpportunityActions(InvocationContext invocationContext) : Invocable(invocationContext)
    {
        [Action("Create opportunity", Description = "Create a new opportunity")]
        public async Task<CreateOpportunityResponse> CreateOpportunity([ActionParameter] CreateOpportunityRequest input)
        {
            var request = new SalesforceRequest($"services/data/v57.0/sobjects/Opportunity", Method.Post, Creds);

            var obj = new ExpandoObject();
            var payload = (IDictionary<string, object?>)obj;

            payload["Name"] = input.Name;
            payload["StageName"] = input.StageName;
            payload["CloseDate"] = input.CloseDate.ToString("yyyy-MM-dd");

            AddIfNotNullOrWhiteSpace(payload, "AccountId", input.AccountId);
            AddIfHasValue(payload, "Amount", input.Amount);
            AddIfHasValue(payload, "Probability", input.Probability);
            AddIfNotNullOrWhiteSpace(payload, "Type", input.Type);
            AddIfNotNullOrWhiteSpace(payload, "LeadSource", input.LeadSource);
            AddIfNotNullOrWhiteSpace(payload, "CampaignId", input.CampaignId);
            AddIfNotNullOrWhiteSpace(payload, "OwnerId", input.OwnerId);
            AddIfNotNullOrWhiteSpace(payload, "NextStep", input.NextStep);
            AddIfNotNullOrWhiteSpace(payload, "Description", input.Description);

            request.AddJsonBody(obj);
            return await Client.ExecuteWithErrorHandling<CreateOpportunityResponse>(request);
        }

        private static void AddIfNotNullOrWhiteSpace(IDictionary<string, object?> dict, string key, string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                dict[key] = value;
        }

        private static void AddIfHasValue<T>(IDictionary<string, object?> dict, string key, T? value) where T : struct
        {
            if (value.HasValue)
                dict[key] = value.Value;
        }
    }
}
