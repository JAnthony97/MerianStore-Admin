using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.Azure;
using Microsoft.Identity.Client;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CJ.MerianPartyStore.PL.UI.Admin.Repositories
{
    public class RepositoryBlobStorage
    {
        CloudBlobContainer container;
      public RepositoryBlobStorage(string nombrecarpeta) {
            String keys = CloudConfigurationManager.GetSetting(ConfigurationManager.AppSettings["MerianPartyStoreStorageConnectionString"]);
            CloudStorageAccount storageaccount=CloudStorageAccount.Parse(keys);
            CloudBlobClient blobClient = storageaccount.CreateCloudBlobClient();
            
            this.container = blobClient.GetContainerReference("MerianPartyStore");
            if (container is null)
                container.Create();


        }
        public void UploadFile() { 
        
        }
    }
}