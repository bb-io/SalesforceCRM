using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Salesforce.Crm
{
    public class Invocable : BaseInvocable
    {
        protected AuthenticationCredentialsProvider[] Creds =>
            InvocationContext.AuthenticationCredentialsProviders.ToArray();

        protected SalesforceClient Client { get; }
        public Invocable(InvocationContext invocationContext) : base(invocationContext)
        {
            Client = new SalesforceClient(invocationContext.AuthenticationCredentialsProviders);
        }
    }
}
