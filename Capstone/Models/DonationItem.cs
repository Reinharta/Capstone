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
        [Display(Name = "Name")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int ItemQuantity { get; set; }

        [Display(Name = "Size")]
        public string ItemSize { get; set; }

        [Required]
        [ForeignKey("Category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public ItemCategory Category { get; set; }

        public string Brand { get; set; }

        public string Color { get; set; }

        [Display(Name = "Description")]
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