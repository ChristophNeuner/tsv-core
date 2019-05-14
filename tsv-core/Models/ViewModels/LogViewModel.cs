using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tsv_core.Models
{
    public class LogViewModel
    {
        public List<Request> Requests { get; set; }

        public List<RequestPathWithCount> DistinctRequestPathsWithCount {get;set;}

        [UIHint("DateTime")]
        [Required]
        public DateTime FromDate { get; set; }

        [UIHint("DateTime")]
        [Required]
        public DateTime UntilDate { get; set; }
    }
}
