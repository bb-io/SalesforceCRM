using Apps.Salesforce.Crm.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Salesforce.Crm;

//public class SalesforceClient(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) 
//    : RestClient(new RestClientOptions() { ThrowOnAnyError = true, BaseUrl = GetUri(authenticationCredentialsProviders) })

public class SalesforceClient : BlackBirdRestClient
{
    public SalesforceClient(IEnumerable<AuthenticationCredentialsProvider> creds)
      : base(new RestClientOptions { ThrowOnAnyError = false, BaseUrl = GetBaseUrl(creds) }) { }

    private static Uri GetBaseUrl(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider)
    {
        var domainName = authenticationCredentialsProvider.First(v => v.KeyName == CredNames.DomainName).Value;
        return new Uri($"https://{domainName}.my.salesforce.com");
    }

    public override async Task<T> ExecuteWithErrorHandling<T>(RestRequest request)
    {
        var resp = await ExecuteWithErrorHandling(request);
        var content = resp.Content ?? string.Empty;
        var val = JsonConvert.DeserializeObject<T>(content, JsonSettings);
        if (val == null)
            throw new Exception($"Could not parse response to {typeof(T)}. Raw: {content}");
        return val;
    }

    public override async Task<RestResponse> ExecuteWithErrorHandling(RestRequest request)
    {
        var response = await ExecuteAsync(request);
        if (!response.IsSuccessStatusCode)
            throw ConfigureErrorException(response);
        return response;
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        var content = response.Content ?? "";
        return new PluginApplicationException($"Error : {content}");
    }
}