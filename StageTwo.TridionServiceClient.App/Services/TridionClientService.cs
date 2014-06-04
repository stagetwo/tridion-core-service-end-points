using StageTwo.TridionServiceClient.App.Tridion.Models;
using StageTwo.TridionServiceClient.App.Tridion.Service;
using StageTwo.TridionServiceClient.App.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tridion.ContentManager.CoreService.Client;

namespace StageTwo.TridionServiceClient.App.Services
{
    public class TridionClientService<C> : ITridionClientService
       where C : ITridionCoreServiceClient, new()
    {
        private NetworkCredential _credentials;
        private string _endPoint;

        public TridionClientService(string endPoint, NetworkCredential credentials)
        {
            _credentials = credentials;
            _endPoint = endPoint;
        }

        public ITridionCoreServiceClient CoreService()
        {
            ITridionCoreServiceClient client = null;

            try
            {
                client = new C();
                client.Open(_endPoint, _credentials);
            }
            catch (EndpointNotFoundException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }

            return client;
        }

        public T Get<T>(string id, ReadOptions readOptions = null) where T : class
        {
            object obj = null;

            CoreService().Using(client =>
            {
                try
                {
                    if (readOptions == null) readOptions = new ReadOptions { LoadFlags = LoadFlags.Expanded };
                    obj = client.Read(id, readOptions);
                }
                catch (Exception e)
                {
                    throw;
                }
            });

            return obj as T;
        }

        public List<T> GetList<T>(string id, SubjectRelatedListFilterData filterData = null) where T : class
        {
            object[] obj = null;
            List<T> list = new List<T>();

            CoreService().Using(client =>
            {
                try
                {
                    if (filterData == null) filterData = new SubjectRelatedListFilterData { };

                    if (typeof(T) == typeof(XmlListItemData))
                    {
                        XElement element = client.GetListXml(id, filterData);

                        list = XmlListItemData.GetListOf(element) as List<T>;
                    }
                    else
                    {
                        obj = client.GetList(id, filterData);

                        if (obj != null && obj.Length > 0)
                        {
                            list = obj.OfType<T>().ToList();
                        }
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            });

            return list;
        }

        public List<T> GetSearchList<T>(SearchQueryData searchData) where T : class
        {
            object[] obj = null;
            List<T> list = new List<T>();

            CoreService().Using(client =>
            {
                try
                {
                    obj = client.GetSearchResults(searchData);
                }
                catch (Exception e)
                {
                    throw;
                }
            });

            if (obj != null && obj.Length > 0)
            {
                list = obj.OfType<T>().ToList();
            }

            return list;
        }

        public SchemaFieldsData ReadSchemaFields(string schemaId, bool expandEmbeddedFields = true, ReadOptions readOptions = null)
        {
            SchemaFieldsData data = null;

            CoreService().Using(client =>
            {
                try
                {
                    if (readOptions == null) readOptions = new ReadOptions { LoadFlags = LoadFlags.Expanded };
                    data = client.ReadSchemaFields(schemaId, expandEmbeddedFields, readOptions);
                }
                catch (Exception e)
                {
                    throw;
                }
            });

            return data;
        }

        public T GetDefaultData<T>(ItemType itemType, string containerId, ReadOptions readOptions = null)
             where T : class
        {
            object obj = null;

            CoreService().Using(client =>
            {
                try
                {
                    if (readOptions == null) readOptions = new ReadOptions { LoadFlags = LoadFlags.Expanded };
                    obj = client.GetDefaultData(itemType, containerId, readOptions);
                }
                catch (Exception e)
                {
                    throw;
                }
            });

            return obj as T;
        }

        public T Save<T>(T deltaData, ReadOptions readBackOptions = null)
            where T : IdentifiableObjectData
        {
            object obj = null;

            CoreService().Using(client =>
            {
                try
                {
                    if (readBackOptions == null) readBackOptions = new ReadOptions { LoadFlags = LoadFlags.Expanded };
                    obj = client.Save(deltaData, readBackOptions);
                }
                catch (Exception e)
                {
                    throw;
                }
            });

            return obj as T;
        }

        public T CheckIn<T>(string id, ReadOptions readBackOptions) where T : VersionedItemData
        {
            object obj = null;

            CoreService().Using(client =>
            {
                try
                {
                    if (readBackOptions == null) readBackOptions = new ReadOptions { LoadFlags = LoadFlags.Expanded };
                    obj = client.CheckIn(id, readBackOptions);
                }
                catch (Exception e)
                {
                    throw;
                }
            });

            return obj as T;
        }

        public T CheckOut<T>(string id, bool permanentLock, ReadOptions readBackOptions) where T : VersionedItemData
        {
            object obj = null;

            CoreService().Using(client =>
            {
                try
                {
                    if (readBackOptions == null) readBackOptions = new ReadOptions { LoadFlags = LoadFlags.Expanded };
                    obj = client.CheckOut(id, permanentLock, readBackOptions);
                }
                catch (Exception e)
                {
                    throw;
                }
            });

            return obj as T;
        }

        public bool IsExistingObject(string id)
        {
            bool exists = false;

            CoreService().Using(client =>
            {
                try
                {
                    exists = client.IsExistingObject(id);
                }
                catch (Exception e)
                {
                    throw;
                }
            });

            return exists;
        }

    }
}
