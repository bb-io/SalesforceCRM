using Apps.Salesforce.Crm.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Salesforce.Crm.DataSourceHandler;

public class AccountDataHandler : BaseInvocable, IAsyncDataSourceHandler
{
    private AuthenticationCredentialsProvider[] Creds
        => InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public AccountDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var client = new SalesforceClient(Creds);

        var query = "SELECT FIELDS(ALL) FROM Account LIMIT 200";
        var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, Creds);

        var response = await client.GetAsync<ListAllAccountsResponse>(request);
        return response.Records
            .Where(x => context.SearchString is null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(30)
            .ToDictionary(x => x.Id, x => x.Name);
    }
}