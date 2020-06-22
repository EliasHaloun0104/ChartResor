using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplicationForMigration.Models
{
    public class StorageContext
    {
        private readonly static string ImageTable = "imagetable";
        private readonly static string ImageBlob = "imageblob";
        
        public CloudTable cloudTable;
        public CloudBlobContainer cloudContainer;
        public StorageContext()
        {
            var cloudStorageAccount = Microsoft.Azure.Cosmos.Table.CloudStorageAccount.DevelopmentStorageAccount;
            var cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            cloudTable = cloudTableClient.GetTableReference(ImageTable);
            cloudTable.CreateIfNotExists();

            var cloudBlobAccount = Microsoft.Azure.Storage.CloudStorageAccount.DevelopmentStorageAccount;
            var cloudBlobClient = cloudBlobAccount.CreateCloudBlobClient();
            cloudContainer = cloudBlobClient.GetContainerReference(ImageBlob);
            cloudContainer.CreateIfNotExists();
        }

        public List<CloudBlockBlob> ListAllBlobs()
        {
            List<CloudBlockBlob> allBlobs = new List<CloudBlockBlob>();
            foreach (IListBlobItem blob in cloudContainer.ListBlobs())
            {
                if (blob.GetType() == typeof(CloudBlockBlob))
                    allBlobs.Add((CloudBlockBlob) blob);
                
            }
            return allBlobs;
        }

        

        public async void DeleteBlob(string name)
        {            
            Uri uri = new Uri(name);
            string filename = Path.GetFileName(uri.LocalPath);
            var blob = cloudContainer.GetBlockBlobReference(filename);
            await blob.DeleteIfExistsAsync();
        }

        

        public IEnumerable<Comment> ListCommentsByCustomer(string uri)
        {
            var query = new TableQuery<Comment>().Where(
                                TableQuery.GenerateFilterCondition(
                                "PartitionKey",
                                QueryComparisons.GreaterThanOrEqual,
                                uri));
            var list = query.Execute();
            return list;
        }

        public async void AddComment(string comment, string uri)
        {
            var commentObj = new Comment(comment, uri);
            TableOperation to = TableOperation.Insert(commentObj);
            await cloudTable.ExecuteAsync(to);
        }
    }
}