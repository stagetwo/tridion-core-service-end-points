Core Service End Point Switching
===============================

**Coding structure to allow switching between Tridion Core Service End Points**

We found recently that we needed to connect to the Core Service using two different end points, TCP in production and Basic HTTP while in development.

CoreServiceClient and SessionAwareCoreServiceClient share the same method names but no common interface therefore switching easily between them without a code change would prove difficult.

This recipe wraps up the Core Service calls so that you don't need to worry about which end point you're using. This decision can be made at application start using a method of your choosing e.g. Dependency Injection.

We have exposed the following methods from the Core Service:

- ReadSchemaFields
- Read
- GetListXml
- GetList
- GetSearchResults
- GetDefaultData
- Save
- CheckIn
- CheckOut
- IsExistingObject

Also we have merged GetList and GetListXml so that we could serialise the XElement returned by GetListXml to a list of ‘XmlListItemData’. A simple model with properties ‘Id’ and ‘Title.

**To get yourself up and running:**

1. Copy your ‘Tridion.ContentManager.CoreService.Client.dll’ into the ‘lib’ folder
2. Edit the App.config and ensure the End Point URLs are correct
3. Edit the App.config and change the ‘CoreService.EndPoint’ appSetting to the end point you wish to use
4. Edit the App.config and add a username and password to the ‘CoreService.Username’ and ‘CoreService.Password’ in the appSettings

This project expands on the code from the 'Tridion.Templates.CoreService' repo by Chris Morgan, which is available here:

https://github.com/chrismrgn/Tridion.Templates.CoreService

Thanks Chris.
