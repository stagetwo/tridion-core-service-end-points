using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Tridion.ContentManager.CoreService.Client;

namespace StageTwo.TridionServiceClient.App.Services
{
    public interface ITridionClientService
    {
        T Get<T>(string id, ReadOptions readOptions = null) where T : class;

        List<T> GetList<T>(string id, SubjectRelatedListFilterData filterData = null) where T : class;

        List<T> GetSearchList<T>(SearchQueryData searchData) where T : class;

        SchemaFieldsData ReadSchemaFields(string schemaId, bool expandEmbeddedFields = true, ReadOptions readOptions = null);

        T GetDefaultData<T>(ItemType itemType, string containerId, ReadOptions readOptions = null) where T : class;

        T Save<T>(T deltaData, ReadOptions readBackOptions = null) where T : IdentifiableObjectData;

        T CheckIn<T>(string id, ReadOptions readBackOptions = null) where T : VersionedItemData;

        T CheckOut<T>(string id, bool permanentLock, ReadOptions readBackOptions = null) where T : VersionedItemData;

        bool IsExistingObject(string id);
    }
}
