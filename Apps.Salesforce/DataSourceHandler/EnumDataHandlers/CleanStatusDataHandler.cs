using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Salesforce.Crm.DataSourceHandler.EnumDataHandlers;

public class CleanStatusDataHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData() => new()
    {
        { "Matched", "Matched" },
        { "Different", "Different" },
        { "Acknowledged", "Acknowledged" },
        { "NotFound", "NotFound" },
        { "Inactive", "Inactive" },
        { "Pending", "Pending" },
        { "SelectMatch", "SelectMatch" },
        { "Skipped", "Skipped" },
    };
}