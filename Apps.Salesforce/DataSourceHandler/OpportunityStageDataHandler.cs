using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Salesforce.Crm.Models.Responses;

namespace Apps.Salesforce.Crm.DataSourceHandler
{
    public class OpportunityStageDataHandler(InvocationContext invocationContext)
        : BaseInvocable(invocationContext), IAsyncDataSourceItemHandler
    {
        private AuthenticationCredentialsProvider[] Creds
            => InvocationContext.AuthenticationCredentialsProviders.ToArray();

        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(
            DataSourceContext context,
            CancellationToken cancellationToken)
        {
            var client = new SalesforceClient(Creds);

            var soql =
               "SELECT MasterLabel, IsActive, IsClosed, IsWon, DefaultProbability, SortOrder " +
               "FROM OpportunityStage " +
               "WHERE IsActive = true " +
               "ORDER BY SortOrder";

            var request = new SalesforceRequest($"services/data/v57.0/query", Method.Get, Creds)
                .AddQueryParameter("q", soql);

            var response = await client.ExecuteWithErrorHandling<ListOpportunityStagesResponse>(request);

            return response!.Records
                .Where(x =>
                    context.SearchString is null ||
                    x.MasterLabel.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Select(x =>
                {
                    var label = x.DefaultProbability.HasValue
                        ? $"{x.MasterLabel} ({x.DefaultProbability.Value}%)"
                        : x.MasterLabel;

                    return new DataSourceItem(x.MasterLabel, label);
                });
        }
    }
}
