using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace tsv_core.Models
{
    public static class Gallery
    {
        private const string _galleryPath = @"wwwroot/images/gallerien";
        private static DirectoryInfo _di = new DirectoryInfo(_galleryPath);

        /// <summary>
        /// Returns all first level subdirectories in the folder "wwwroot/images/gallerien" .
        /// </summary>
        public static List<DirectoryInfo> Galleries
        {
            get
            {
                return _di.GetDirectories("*.*", SearchOption.TopDirectoryOnly).ToList();
            }
        }
    }
}
