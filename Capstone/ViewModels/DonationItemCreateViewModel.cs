using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Capstone.Models;

namespace Capstone.ViewModels
{
    public class DonationItemCreateViewModel
    {
        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
        public NonprofitOrganization Organization { get; set; }

        public string ItemName { get; set; }

        public int ItemQuantity { get; set; }

        public string ItemSize { get; set; }

        public int CategoryId { get; set; }

        public string Brand { get; set; }

        public string Color { get; set; }

        public string ItemDescription { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
        public string File { get; set; }

        //public IEnumerable<ItemCategory> Categories { get; set; }



    }
}