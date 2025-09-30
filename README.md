# Blackbird.io Salesforce CRM

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

Salesforce is a leading cloud-based customer relationship management (CRM) platform designed to help businesses manage their customer data, improve relationships, and drive growth. It provides a comprehensive suite of tools that facilitate sales, customer service, marketing, and analytics.

## Before setting up

Before you can connect you will need:

• Domain name
• Client ID
• Client secret

To get this, create a connected app on Salesforce - find instructions [here](https://help.salesforce.com/s/articleView?id=sf.connected_app_create_api_integration.htm&type=5). Walk through the steps to create a connected app, you must select the enable OAuth settings options, then select the permissions you would like to give access to to your connection. Additionally, in the redirect URI selection, fill in the following URI: `https://bridge.blackbird.io/api/AuthorizationCode`
 
Once the connected app is created, you can get the Client ID (consumer key) and Client secret (consumer secret). To access the consumer key, from the App Manager, find the connected app, and select `View` from the dropdown. Then click `Manage Consumer Details`. You’re sometimes prompted to verify your identity before you can view the consumer key.
 
## Connecting

1. Navigate to Apps and search for Salesforce CRM.
2. Click _Add Connection_.
3. Name your connection for future reference e.g. 'My Salesforce'.
4. Fill in the _Domain name_, _Client ID_ and _Client secret_ obtained earlier.
5. Click Authorize connection.
6. Follow the instructions that Salesforce provides, authorizing Blackbird.io to act on your behalf.
9. When you return to Blackbird, confirm that the connection has appeared and the status is Connected.

## Actions

###  Account
- **Search all accounts** List all accounts
- **Get account** Get account by id
- **Create account** Create account
- **Update account field** Update account field
- **Delete account** Delete account by id

###  Contact
- **Search all contacts** List all contacts
- **Get contact** Get contact by id
- **Create contact** Create contact
- **Update contact field** Update contact field
- **Delete contact** Delete contact by id

###  Files
- **Search all files** List all files
- **Get file info** Get file info by id
- **Download file** Download file by id
- **Upload file** Upload file
- **Delete file** Delete file

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
