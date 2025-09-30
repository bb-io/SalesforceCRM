using Apps.Salesforce.Crm.DataSourceHandler;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.Salesforce.Crm.Models.Requests
{
    public class CreateOpportunityRequest
    {
        [Display("Name")]
        [JsonProperty("Name")]
        public string Name { get; set; } = default!;

        [Display("Stage name")]
        [JsonProperty("StageName")]
        [DataSource(typeof(OpportunityStageDataHandler))]
        public string StageName { get; set; } = default!;

        [Display("Close date")]
        [JsonProperty("CloseDate")]
        public DateTime CloseDate { get; set; }

        [Display("Account ID")]
        [JsonProperty("AccountId")]
        public string? AccountId { get; set; }

        [Display("Amount")]
        [JsonProperty("Amount")]
        public decimal? Amount { get; set; }

        [Display("Probability (%)")]
        [JsonProperty("Probability")]
        public double? Probability { get; set; }

        [Display("Type")]
        [JsonProperty("Type")]
        public string? Type { get; set; }

        [Display("Lead source")]
        [JsonProperty("LeadSource")]
        public string? LeadSource { get; set; }

        [Display("Campaign ID")]
        [JsonProperty("CampaignId")]
        public string? CampaignId { get; set; }

        [Display("Owner ID")]
        [JsonProperty("OwnerId")]
        public string? OwnerId { get; set; }

        [Display("Next step")]
        [JsonProperty("NextStep")]
        public string? NextStep { get; set; }

        [Display("Description")]
        [JsonProperty("Description")]
        public string? Description { get; set; }

        [Display("Currency")]
        [JsonProperty("CurrencyIsoCode")]
        public string? CurrencyIsoCode { get; set; }
    }
}
