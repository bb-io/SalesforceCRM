using System.Text.Json;
using Apps.Salesforce.Crm.Constants;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Salesforce.Crm.Auth.OAuth2;

public class OAuth2TokenService(InvocationContext invocationContext) : BaseInvocable(invocationContext), IOAuth2TokenService
{
    private const string ExpiresAtKeyName = "expires_at";

    private static string TokenUrl = "";

    public async Task<Dictionary<string, string>> RequestToken(string state, string code, Dictionary<string, string> values, CancellationToken cancellationToken)
    {
        TokenUrl = $"https://{values[CredNames.DomainName]}.my.salesforce.com/services/oauth2/token";

        const string grant_type = "authorization_code";
        string redirectUri = $"{invocationContext.UriInfo.BridgeServiceUrl.ToString().TrimEnd('/')}/AuthorizationCode";
        var bodyParameters = new Dictionary<string, string>
        {
            { "grant_type", grant_type },
            { "client_id", values[CredNames.ClientId] },
            { "client_secret", values[CredNames.ClientSecret] },
            { "redirect_uri", redirectUri },
            { "code", code }
        };

        return await RequestToken(bodyParameters, cancellationToken);
    }

    private async Task<Dictionary<string, string>> RequestToken(Dictionary<string, string> bodyParameters, CancellationToken cancellationToken)
    {
        var utcNow = DateTime.UtcNow;
        using HttpClient httpClient = new HttpClient();
        using var httpContent = new FormUrlEncodedContent(bodyParameters);
        using var response = await httpClient.PostAsync(TokenUrl, httpContent, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync();
        var resultDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent)?.ToDictionary(r => r.Key, r => r.Value?.ToString())
                               ?? throw new InvalidOperationException($"Invalid response content: {responseContent}");

        var expiresAt = utcNow.AddHours(2);
        resultDictionary.Add(ExpiresAtKeyName, expiresAt.ToString());

        return resultDictionary;
    }

    public bool IsRefreshToken(Dictionary<string, string> values)
    {
        var expiresAt = DateTime.Parse(values[ExpiresAtKeyName]);
        return DateTime.UtcNow > expiresAt;
    }

    public async Task<Dictionary<string, string>> RefreshToken(Dictionary<string, string> values, CancellationToken cancellationToken)
    {
        const string grant_type = "refresh_token";
        TokenUrl = $"https://{values[CredNames.DomainName]}.my.salesforce.com/services/oauth2/token";

        var bodyParameters = new Dictionary<string, string>
        {
            { "grant_type", grant_type },
            { "client_id", values[CredNames.ClientId] },
            { "client_secret", values[CredNames.ClientSecret] },
            { "refresh_token", values["refresh_token"] },
        };
        return await RequestToken(bodyParameters, cancellationToken);
    }

    public Task RevokeToken(Dictionary<string, string> values)
    {
        throw new NotImplementedException();
    }
}
