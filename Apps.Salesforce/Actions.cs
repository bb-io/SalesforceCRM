using RestSharp;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Salesforce
{
    [ActionList]
    public class Actions
    {
        [Action]
        public AccountName GetAccountName(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var domainName = authenticationCredentialsProviders.First(v => v.KeyName == "domainName").Value;

            var client = new SalesforceClient(authenticationCredentialsProviders: authenticationCredentialsProviders);
            var request = new RestRequest("services/data/v57.0/query?q=SELECT+name+from+Account", Method.Get);

            request.AddHeader("Authorization", authenticationCredentialsProviders.First(p => p.KeyName == "Authorization").Value);

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

