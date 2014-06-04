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
    public interface ITridionCoreServiceClient
    {
        void Open(string endPoint, NetworkCredential credentials);

        void Close();

        void Abort();

        SchemaFieldsData ReadSchemaFields(string schemaId, bool expandEmbeddedFields, ReadOptions readOptions);

        IdentifiableObjectData Read(string id, ReadOptions readOptions);

        XElement GetListXml(string id, SubjectRelatedListFilterData filter);

        IdentifiableObjectData[] GetList(string id, SubjectRelatedListFilterData filter);

        IdentifiableObjectData[] GetSearchResults(SearchQueryData filter);

        IdentifiableObjectData GetDefaultData(ItemType itemType, string containerId, ReadOptions readOptions);

        IdentifiableObjectData Save(IdentifiableObjectData deltaData, ReadOptions readBackOptions);

        VersionedItemData CheckIn(string id, ReadOptions readBackOptions);

        VersionedItemData CheckOut(string id, bool permanentLock, ReadOptions readBackOptions);

        bool IsExistingObject(string id);
    }
}