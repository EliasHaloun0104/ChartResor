using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebApplicationForMigration.Models
{
    public class BlobViewModel
    {
        public Uri Image { get; set; }

        public string Owner { get; set; }
    
        public int Like { get; set; }
        public int Dislike { get; set; }
        public int Download { get; set; }
    }
}