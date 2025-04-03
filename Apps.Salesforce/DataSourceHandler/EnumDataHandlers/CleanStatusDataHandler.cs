using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Salesforce.Crm.DataSourceHandler.EnumDataHandlers;

public class CleanStatusDataHandler : IStaticDataSourceItemHandler
{
    public IEnumerable<DataSourceItem> GetData()
    {
        return new Dictionary<string, string>
        {
            { "Matched", "Matched" },
            { "Different", "Different" },
            { "Acknowledged", "Acknowledged" },
            { "NotFound", "NotFound" },
            { "Inactive", "Inactive" },
            { "Pending", "Pending" },
            { "SelectMatch", "SelectMatch" },
            { "Skipped", "Skipped" }
        }.Select(x => new DataSourceItem(x.Key, x.Value));
    }
}