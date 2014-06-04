Core Service End Point Switching
===============================

Coding structure to allow switching between Tridion Core Service End Points

===============================

We found recently that we needed to connect to the Core Service using 2 different end points. TCP in production and Basic HTTP while in development. 

The CoreServiceClient and SessionAwareCoreServiceClient share the same method names but no common interface thus switching easily between them without a code change would prove difficult.

This recipe wraps up the Core Service calls so that you need to worry about which end point you are using. This decision can be made at application start using a method of your choosing e.g. Dependency Injection.

I have exposed the following methods from the Core Service. 

-ReadSchemaFields
-Read
-GetListXml
-GetList
-GetSearchResults
-GetDefaultData
-Save
-CheckIn
-CheckOut
-IsExistingObject

Also we have merge GetList and GetListXml so that we could serialise the XElement returned by GetListXml to a list of ‘XmlListItemData’. A simple model with properties ‘Id’ and ‘Title.

**To get yourself up and running:**

1. Copy your ‘Tridion.ContentManager.CoreService.Client.dll’ into the ‘lib’ folder
2. Edit the App.config 
..1.Ensure the End Point URLs are correct
..2.Change ‘CoreService.EndPoint’ appSetting to the end point you wish to use
..3.Add a username and password to the ‘CoreService.Username’ and ‘CoreService.Password’ appSettings


Reference Projects:

https://github.com/chrismrgn/Tridion.Templates.CoreService (Thanks Chris)
