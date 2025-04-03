using Apps.Salesforce.Crm.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Salesforce.Crm;

public class SalesforceClient(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) 
    : RestClient(new RestClientOptions() { ThrowOnAnyError = true, BaseUrl = GetUri(authenticationCredentialsProviders) })
{
    private static Uri GetUri(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider)
    {
        var domainName = authenticationCredentialsProvider.First(v => v.KeyName == CredNames.DomainName).Value;
        return new Uri($"https://{domainName}.my.salesforce.com");
    }
}