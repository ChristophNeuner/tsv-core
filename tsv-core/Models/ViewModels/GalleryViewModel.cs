using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tsv_core.Models
{
    public class GalleryViewModel
    {
        [Required]
        public string folderName;

        [Required]
        public IList<IFormFile> files;
    }
}
