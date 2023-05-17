using System;
using System.Text.Json;

namespace Apps.Salesforce
{ 

    public class QueryResult
    {
        public int TotalSize { get; set; }
        public bool Done { get; set; }
        public List<Account> Records { get; set; }
    }

    public class Account
    {
        public string Name { get; set; }
        public Attributes Attributes { get; set; }
    }

    public class Attributes
    {
        public string Type { get; set; }
        public string url { get; set; }
    }

    public class AccountName
    {
        public AccountName(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

}

