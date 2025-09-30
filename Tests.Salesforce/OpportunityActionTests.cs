using Apps.Salesforce.Crm.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Salesforce.Base;

namespace Tests.Salesforce
{
    [TestClass]
    public class OpportunityActionTests :TestBase
    {
        [TestMethod]
        public async Task CreateOpportunity_Success()
        {
           var action = new OpportunityActions(InvocationContext);

           var response =await action.CreateOpportunity(new Apps.Salesforce.Crm.Models.Requests.CreateOpportunityRequest
           {
                Name = "Test Opportunity",
                CloseDate = DateTime.Now.AddDays(30),
                StageName = "Prospecting"
           });

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(response);

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));

            Assert.IsNotNull(response);
        }
    }
}
