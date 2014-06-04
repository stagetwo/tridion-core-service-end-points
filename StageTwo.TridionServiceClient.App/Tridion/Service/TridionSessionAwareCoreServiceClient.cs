using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.ServiceModel;
using System.Xml.Linq;
using Tridion.ContentManager.CoreService.Client;

namespace StageTwo.TridionServiceClient.App.Tridion.Service
{
    public class TridionSessionAwareCoreServiceClient : ITridionCoreServiceClient
    {

        SessionAwareCoreServiceClient _client;

        public void Close()
        {
            _client.Close();
        }

        public void Abort()
        {
            _client.Abort();
        }

        public SchemaFieldsData ReadSchemaFields(string schemaId, bool expandEmbeddedFields, ReadOptions readOptions)
        {
            return _client.ReadSchemaFields(schemaId, expandEmbeddedFields, readOptions);
        }

        public IdentifiableObjectData Read(string id, ReadOptions readOptions)
        {
            return _client.Read(id, readOptions);
        }

        public XElement GetListXml(string id, SubjectRelatedListFilterData filter)
        {
           return _client.GetListXml(id, filter);
        }

        public IdentifiableObjectData[] GetList(string id, SubjectRelatedListFilterData filter)
        {
            return _client.GetList(id, filter);
        }

        public IdentifiableObjectData[] GetSearchResults(SearchQueryData filter)
        {
            return _client.GetSearchResults(filter);
        }

        public IdentifiableObjectData GetDefaultData(ItemType itemType, string containerId, ReadOptions readOptions)
        {
            return _client.GetDefaultData(itemType, containerId, readOptions);
        }

        public IdentifiableObjectData Save(IdentifiableObjectData deltaData, ReadOptions readBackOptions)
        {
            return _client.Save(deltaData, readBackOptions);
        }

        public VersionedItemData CheckIn(string id, ReadOptions readBackOptions)
        {
            return _client.CheckIn(id, readBackOptions);
        }

        public VersionedItemData CheckOut(string id, bool permanentLock, ReadOptions readBackOptions)
        {
            return _client.CheckOut(id, permanentLock, readBackOptions);
        }

        public bool IsExistingObject(string id)
        {
            return _client.IsExistingObject(id);
        }

        public void Open(string endPoint, NetworkCredential credentials)
        {
            _client = new SessionAwareCoreServiceClient(endPoint);
            if (credentials != null)
            {
                _client.ChannelFactory.Credentials.Windows.ClientCredential = credentials;
            }
        }
    }
}