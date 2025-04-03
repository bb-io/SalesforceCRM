using Apps.Salesforce.Crm.Constants;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Invocation;
using Microsoft.AspNetCore.WebUtilities;

namespace Apps.Salesforce.Crm.Auth.OAuth2;

public class OAuth2AuthorizeService(InvocationContext invocationContext) : BaseInvocable(invocationContext), IOAuth2AuthorizeService
{
    public string GetAuthorizationUrl(Dictionary<string, string> values)
    {
        var domainName = values[CredNames.DomainName];
        var oauthUrl = $"https://{domainName}.my.salesforce.com/services/oauth2/authorize";
        var bridgeOauthUrl = $"{invocationContext.UriInfo.BridgeServiceUrl.ToString().TrimEnd('/')}/oauth";

        var parameters = new Dictionary<string, string>
        {
            { "client_id", values[CredNames.ClientId] },
            { "redirect_uri", $"{invocationContext.UriInfo.BridgeServiceUrl.ToString().TrimEnd('/')}/AuthorizationCode" },
            { "actual_redirect_uri", invocationContext.UriInfo.AuthorizationCodeRedirectUri.ToString() },
            { "response_type", "code"},
            { "state", values["state"] },
            { "authorization_url", oauthUrl}
        };

        return QueryHelpers.AddQueryString(bridgeOauthUrl, parameters!);
    }
}