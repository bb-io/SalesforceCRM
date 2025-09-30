using Apps.Salesforce.Crm.DataSourceHandler;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Tests.Salesforce.Base;

namespace Tests.Salesforce;

[TestClass]
public class DataHandlerTests : TestBase
{
    [TestMethod]
    public async Task AccountDataHandler_IssSuccess()
    {
        var handler = new AccountDataHandler(InvocationContext);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        foreach(var item in result)
        {
            Console.WriteLine($"{item.DisplayName} - {item.Value}");
        }

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task ContactDataHandler_IssSuccess()
    {
        var handler = new ContactDataHandler(InvocationContext);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        foreach (var item in result)
        {
            Console.WriteLine($"{item.DisplayName} - {item.Value}");
        }

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task FiletDataHandler_IssSuccess()
    {
        var handler = new FileDataHandler(InvocationContext);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        foreach (var item in result)
        {
            Console.WriteLine($"{item.DisplayName} - {item.Value}");
        }

        Assert.IsNotNull(result);
    }


    [TestMethod]
    public async Task OpportunityStageDataHandler_IssSuccess()
    {
        var handler = new OpportunityStageDataHandler(InvocationContext);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        foreach (var item in result)
        {
            Console.WriteLine($"{item.DisplayName} - {item.Value}");
        }

        Assert.IsNotNull(result);
    }
}
