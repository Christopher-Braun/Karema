using System;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.DataServices;

namespace Mvc4WebRole.Models
{
    public class RecipeTableServiceContext
    {
        private static RecipeTableServiceContext instance;
        public static RecipeTableServiceContext Instance
        {
            get
            {
                if ( instance == null )
                {
                    instance = new RecipeTableServiceContext();
                }

                return instance;
            }
        }

        public CloudTable RecipeTable { get; private set; }

        public TableServiceContext TableServiceContext { get; private set; }


        private RecipeTableServiceContext()
        {
            CloudTableClient tableClient = GetStorageAccount().CreateCloudTableClient();

            TableServiceContext = tableClient.GetTableServiceContext();

            RecipeTable = tableClient.GetTableReference("Recipes");
            RecipeTable.CreateIfNotExists();
        }


        private static CloudStorageAccount GetStorageAccount()
        {

            if ( Environment.UserName == "chribra1" )
            {
                return CloudStorageAccount.DevelopmentStorageAccount;
            }

            var connection = CloudConfigurationManager.GetSetting("StorageConnectionString");
            return CloudStorageAccount.Parse(connection);

            //const string settings = "DefaultEndpointsProtocol=https;AccountName=qwertz;AccountKey=gR/44Lgfnz6RkiQQ+7m6LgcXpkjg87pEYKD2PHOHjjeKJRH6XdnFtZ56mVoEYXBMyAKmKs3DLaFhNomGwwZktg==";
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(settings);
            //return storageAccount;
        }

        internal void Dispose()
        {

        }
    }
}