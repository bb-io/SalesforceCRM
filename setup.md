## some notes on setup for salesforce instances

process for setting up salesforce connector

things needed:
• domain name
• client id
• client secret

steps:
• create a connected app on salesforce
https://help.salesforce.com/s/articleView?id=sf.connected_app_create_api_integration.htm&type=5
walk through the steps to create a connected app, you must select the enable oauth settings options, then select the permissions you would like to give access to to your connection

note: put something in here about which actions require which permissions once the actions get built out.

additionally, in the redirect uri selection, put in the blackbird uri

https://sandbox.blackbird.io/api-rest/connections/AuthorizationCode

• get client id and client secret from connected app
The consumer key of the connected app. To access the consumer key, from the App Manager, find the connected app, and select View from the dropdown. Then click Manage Consumer Details. You’re sometimes prompted to verify your identity before you can view the consumer key.

note: consumer key is client id and consumer secret is client secret
