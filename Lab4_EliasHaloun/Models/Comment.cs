using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace WebApplicationForMigration.Models
{ 


    public class Comment: TableEntity
    {
        public string CommentText { get; set; }

        public Comment(string URL, string Comment)
        {
            PartitionKey = URL;
            RowKey = Guid.NewGuid().ToString();
            CommentText = Comment;
        }

       
    }
}