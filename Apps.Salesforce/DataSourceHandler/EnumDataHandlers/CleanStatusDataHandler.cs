using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.Salesforce.Crm.DataSourceHandler.EnumDataHandlers;

public class CleanStatusDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
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