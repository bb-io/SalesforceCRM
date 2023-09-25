using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using RestSharp;

namespace Apps.Salesforce.Crm.Connections;

public class ConnectionValidator : IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
    {
        try
        {
            var client = new SalesforceClient(authProviders);

            var query = "SELECT FIELDS(ALL) FROM Contact LIMIT 200";
            var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, authProviders);

            await client.ExecuteAsync(request, cancellationToken);

            return new()
            {
                IsValid = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }
    }
}