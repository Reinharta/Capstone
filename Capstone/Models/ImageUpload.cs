using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class ImageUpload
    {
        [Key]
        public int ImageFileId { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Image")]
        public string File { get; set; }

    }
}