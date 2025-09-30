using Apps.Salesforce.Crm.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using RestSharp;

namespace Apps.Salesforce.Crm;

public class SalesforceRequest : BlackBirdRestRequest
{
    public SalesforceRequest(string endpoint, Method method, IEnumerable<AuthenticationCredentialsProvider> creds) : base(endpoint, method, creds)
    {
    }

    protected override void AddAuth(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var auth = creds.First(p => p.KeyName == CredNames.Authorization).Value;
        this.AddHeader("Authorization", auth);
    }
}