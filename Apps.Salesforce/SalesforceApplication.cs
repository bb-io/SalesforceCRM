﻿using Apps.Salesforce.Crm.Auth.OAuth2;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.Salesforce.Crm;

public class SalesforceApplication : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.Crm];
        set { }
    }
    
    private readonly Dictionary<Type, object> _container;
    public string Name
    {
        get => "Salesforce";
        set { }
    }
    public SalesforceApplication()

    {
        _container = LoadTypes();
    }

    public T GetInstance<T>()
    {
        if (!_container.TryGetValue(typeof(T), out var value))
        {
            throw new InvalidOperationException($"Instance of type '{typeof(T)}' not found");
        }
        return (T)value;
    }

    private Dictionary<Type, object> LoadTypes()

    {

        return new Dictionary<Type, object>()

        {

            { typeof(IOAuth2AuthorizeService), new OAuth2AuthorizeService() },

            { typeof(IOAuth2TokenService), new OAuth2TokenService() }

        };

    }
}