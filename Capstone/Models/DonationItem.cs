using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class DonationItem
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public string ItemName { get; set; }

        public int? ItemQuantity { get; set; }

        public string ItemSize { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public ItemCategory Category { get; set; }

        public string Brand { get; set; }

        public string Color { get; set; }

        [Required]
        public string ItemDescription { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }

        public string ImageFilePath { get; set; }

        [ForeignKey("Organization")]
        public int RequestingOrganizationId { get; set; }
        public NonprofitOrganization Organization { get; set; }


    }
}