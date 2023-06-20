using RestSharp;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common;
using Apps.Salesforce.Models.Responses;

namespace Apps.Salesforce.Actions
{
    [ActionList]
    public class Actions
    {
        [Action("Get account name", Description = "Get account name")]
        public AccountName GetAccountName(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var client = new SalesforceClient(authenticationCredentialsProviders);
            var request = new SalesforceRequest("services/data/v57.0/query?q=SELECT+name+from+Account", Method.Get, authenticationCredentialsProviders);

            var response = client.Execute<QueryResult>(request);
            if (response.Data == null)
            {
                throw new Exception("response data was null");
            }
            if (response.Data.Records.Count < 1)
            {
                throw new Exception("no accounts were found");
            }
            return new AccountName(response.Data.Records[0].Name);
        }
    }
}

