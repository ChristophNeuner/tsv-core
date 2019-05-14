using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace tsv_core.Models
{
    public class Request
    {
        [Key]
        public int ID { get; set; }
        public string Path { get; set; }
        public string Time { get; set; }
        public string IPAddressClient { get; set; }
        public string UserAgent { get; set; }
    }
}
