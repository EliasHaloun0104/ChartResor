using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationForMigration.Models;

namespace Lab4_EliasHaloun.ControllersAdmin
{
    [Authorize(Roles = "Admin")]
    public class AdminBlobController : Controller
    {
        StorageContext context = new StorageContext();
        // GET: AdminBlob
        public ActionResult BlobContainerInfo()
        {
            var list = context.cloudContainer.ListBlobs().Count();

            ViewBag.Message = list + " Picture/s";
            return View(); 
        }

        public ActionResult BlobSize()
        {
            var list = context.ListAllBlobs();
            long size = 0;
            foreach (var item in list)
            {
                size += item.Properties.Length;
            }
            ViewBag.Message = "Total disk space utilized by blobs " + size + " Bytes";
            return View();
        }

        public ActionResult BlobList()
        {
            var list = context.ListAllBlobs();
            var resultList = new List<BlobViewModel>();
            foreach (var item in list)
            {
                item.FetchAttributes();
                resultList.Add(new BlobViewModel()
                {
                    Image = item.Uri,
                    Owner = item.Metadata["Owner"],
                    Like = int.Parse(item.Metadata["likes"]),
                    Dislike = int.Parse(item.Metadata["dislikes"]),
                    Download = int.Parse(item.Metadata["downloads"])
                });

            }
            return View(resultList);
        }
    }
}