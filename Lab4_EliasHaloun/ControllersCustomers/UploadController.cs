using Lab4_EliasHaloun;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplicationForMigration.Models;

namespace WebApplicationForMigration.Controllers
{
    [Authorize(Roles = "Customer")]
    public class UploadController : Controller
    {
        public StorageContext db = new StorageContext();
        // GET: Upload/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            if (Request.Files.Count > 0)
            {
                for (int fileNum = 0; fileNum < Request.Files.Count; fileNum++)
                {
                    string fileName = Path.GetFileName(Request.Files[fileNum].FileName);
                    if (Request.Files[fileNum] != null && Request.Files[fileNum].ContentLength > 0)
                    {
                        var userManger = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByIdAsync(System.Web.HttpContext.Current.User.Identity.GetUserId());
                        var user = User.Identity.GetUserName();
                        CloudBlockBlob blockBlob = db.cloudContainer.GetBlockBlobReference(fileName);
                        blockBlob.Properties.ContentType = Path.GetExtension(fileName);
                        blockBlob.Metadata["Owner"] = user;
                        blockBlob.Metadata["likes"] = 0 + "";
                        blockBlob.Metadata["dislikes"] = 0 + "";
                        blockBlob.Metadata["downloads"] = 0 + "";
                        blockBlob.UploadFromStream(Request.Files[fileNum].InputStream);
                        
                    }
                }
                return RedirectToAction("Create");
            }
            return RedirectToAction("Create");
        }
        
        
        public ActionResult ViewImage()
        {
            var list = db.ListAllBlobs();
            var resultList = new List<BlobViewModel>();
            foreach (var item in list)
            {                
                item.FetchAttributes();
                resultList.Add(new BlobViewModel() {
                    Image = item.Uri,
                    Owner = item.Metadata["Owner"],
                    Like = int.Parse(item.Metadata["likes"]),
                    Dislike = int.Parse(item.Metadata["dislikes"]),
                    Download = int.Parse(item.Metadata["downloads"])
                });                
            }
              
            return View(resultList);
        }

        public ActionResult SelectImageToPreview(string uri)
        {
            var blob = db.cloudContainer.GetBlockBlobReference(uri);
            var comments = db.ListCommentsByCustomer(uri);
            ViewBag.CommentsList = comments;
            return View(blob);
        }

        public ActionResult Like(string uri)
        {
            var blockBlob = db.cloudContainer.GetBlockBlobReference(uri);
            blockBlob.FetchAttributes();
            int like = int.Parse(blockBlob.Metadata["likes"]);
            like++;
            blockBlob.Metadata["likes"] = like + "";
            blockBlob.SetMetadata();
            return RedirectToAction("ViewImage");
        }
        
        public ActionResult Dislikes(string uri)
        {
            var blockBlob = db.cloudContainer.GetBlockBlobReference(uri);
            blockBlob.FetchAttributes();
            int dislike = int.Parse(blockBlob.Metadata["dislikes"]);
            dislike++;
            blockBlob.Metadata["dislikes"] = dislike + "";
            blockBlob.SetMetadata();
            return RedirectToAction("ViewImage");
        }
        
        public ActionResult Download(string uri)
        {
            var blockBlob = db.cloudContainer.GetBlockBlobReference(uri);
            blockBlob.FetchAttributes();
            int downloads = int.Parse(blockBlob.Metadata["downloads"]);
            downloads++;
            blockBlob.Metadata["downloads"] = downloads + "";
            blockBlob.SetMetadata();

            //return RedirectToAction("ViewImage");
            return RedirectToAction("ViewImage");
        }
        
        public ActionResult Delete(string uri)
        {
            var blockBlob = db.cloudContainer.GetBlockBlobReference(uri);
            //For multiable users access
            //lock (db.cloudContainer) ;
            blockBlob.Delete();
            return RedirectToAction("ViewImage");
        }

        
    }
}
