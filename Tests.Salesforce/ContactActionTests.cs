using Apps.Salesforce.Crm.Actions;
using Tests.Salesforce.Base;

namespace Tests.Salesforce
{
    [TestClass]
    public class ContactActionTests : TestBase
    {
        [TestMethod]
        public async Task SearchContacts_ActionIsSuccess()
        {
            var action = new ContactActions(InvocationContext);
            var result = await action.ListAllContacts();
            Console.WriteLine($"Count: {result.Records.Count()}");
            foreach (var item in result.Records)
            {
                Console.WriteLine($"{item.Name} - {item.Id}");
            }

            Assert.IsNotNull(result);
        }
    }
}
