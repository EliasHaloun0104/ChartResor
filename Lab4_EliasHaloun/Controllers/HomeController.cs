using Lab4_EliasHaloun.Models.ModelsResor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationForMigration.Models;

namespace Lab4_EliasHaloun.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public StorageContext db2 = new StorageContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Visitor()
        {
            return View();
        }

        public ActionResult VisitorFlight()
        {
            var flights = db.Flights;
            return View(flights.ToList());
        }
        public ActionResult VisitorImage()
        {
            var list = db2.ListAllBlobs();
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

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return View();
        }
        [Authorize(Roles = "Customer")]
        public ActionResult Customer()
        {
            return View();
        }
    }
}