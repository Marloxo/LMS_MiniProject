using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS_MiniProject.Models
{
    public class BookViewModel
    {
    }

    [MetadataType(typeof(MetaData))]
    public partial class BookTbl
    {
        public class MetaData
        {
            public int Id { get; set; }

            [Required]
            [StringLength(60, MinimumLength = 3)]
            public string Title { get; set; }

            [Required]
            [StringLength(60, MinimumLength = 3)]
            public string Author { get; set; }

            [Required]
            [StringLength(60, MinimumLength = 3)]
            public string Tag { get; set; }

            [Required]
            public string Image { get; set; }

            [Required]
            [Range(0, 2018)]
            public int Year_of_publishing { get; set; }
        }

    }
}