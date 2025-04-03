using Apps.Salesforce.Crm.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Salesforce.Crm;

public class SalesforceRequest : RestRequest
{
    public SalesforceRequest(string endpoint, 
        Method method, 
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) : base(endpoint, method)
    {
        var token = authenticationCredentialsProviders.First(p => p.KeyName == CredNames.Authorization).Value;
        this.AddHeader("Authorization", $"{token}");
    }
}