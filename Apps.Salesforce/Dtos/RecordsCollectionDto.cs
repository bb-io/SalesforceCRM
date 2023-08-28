namespace Apps.Salesforce.Crm.Dtos;

public class RecordsCollectionDto<T>
{
    public IEnumerable<T> Records { get; set; }
}