using Newtonsoft.Json;

namespace Apps.Salesforce.Crm.Models.Responses
{
    public class ListOpportunityStagesResponse
    {
        [JsonProperty("records")]
        public List<OpportunityStageDto> Records { get; set; } = new();
    }

    public class OpportunityStageDto
    {
        [JsonProperty("MasterLabel")]
        public string MasterLabel { get; set; } = default!;

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("IsClosed")]
        public bool IsClosed { get; set; }

        [JsonProperty("IsWon")]
        public bool IsWon { get; set; }

        [JsonProperty("DefaultProbability")]
        public double? DefaultProbability { get; set; }

        [JsonProperty("SortOrder")]
        public int SortOrder { get; set; }
    }
}
