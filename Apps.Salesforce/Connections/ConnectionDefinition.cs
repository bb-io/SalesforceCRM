using Apps.Salesforce.Crm.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.Salesforce.Crm.Connections;

public class OAuth2ConnectionDefinition : IConnectionDefinition
{
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups =>
    [
        new ConnectionPropertyGroup
        {
            Name = "OAuth2",
            AuthenticationType = ConnectionAuthenticationType.OAuth2,
            ConnectionProperties =
            [
                new(CredNames.DomainName) 
                {
                    DisplayName = "Domain name"
                },
                new(CredNames.ClientId)
                {
                    DisplayName = "Client ID"
                },
                new(CredNames.ClientSecret)
                {
                    DisplayName = "Client secret",
                    Sensitive = true
                }
            ]
        }
    ];

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(Dictionary<string, string> values)
    {
        var token = GetValueOrThrow(values, "access_token", "Access token not found");
        var domainName = GetValueOrThrow(values, CredNames.DomainName, "Domain name not found");
            
        yield return new AuthenticationCredentialsProvider(CredNames.Authorization, $"Bearer {token}");
        yield return new AuthenticationCredentialsProvider(CredNames.DomainName, domainName);
    }
    
    private string GetValueOrThrow(Dictionary<string, string> values, string key, string errorMessage)
    {
        return values.TryGetValue(key, out var value) 
            ? value 
            : throw new ArgumentException(errorMessage, nameof(key));
    }
}