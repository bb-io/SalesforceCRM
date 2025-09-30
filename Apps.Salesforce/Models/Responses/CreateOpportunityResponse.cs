using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Salesforce.Crm.Models.Responses
{
    public class CreateOpportunityResponse
    {
        [Display("Record ID")]
        [JsonProperty("id")]
        public string Id { get; set; } = default!;

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("errors")]
        public List<string>? Errors { get; set; }
    }
}
