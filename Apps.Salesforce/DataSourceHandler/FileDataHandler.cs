using Apps.Salesforce.Crm.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Salesforce.Crm.DataSourceHandler
{
    public class FileDataHandler(InvocationContext invocationContext) : BaseInvocable(invocationContext), IAsyncDataSourceItemHandler
    {
        private AuthenticationCredentialsProvider[] Creds
            => InvocationContext.AuthenticationCredentialsProviders.ToArray();

        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            var client = new SalesforceClient(Creds);

            var query = "SELECT FIELDS(ALL) FROM ContentDocument LIMIT 200";
            var request = new SalesforceRequest($"services/data/v57.0/query?q={query}", Method.Get, Creds);

            var response = await client.ExecuteWithErrorHandling<ListAllAccountsResponse>(request);
            return response!.Records
                .Where(x => context.SearchString is null ||
                            x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Select(x => new DataSourceItem(x.Id, x.Id));
        }
    }
}
