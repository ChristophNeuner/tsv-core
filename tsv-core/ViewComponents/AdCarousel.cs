using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace tsv_core.Infrastructure
{
    public class AdCarousel : ViewComponent
    {
        private DirectoryInfo di;

        public AdCarousel()
        {
            di = new DirectoryInfo("wwwroot/images/sponsoren");
        }

        public IViewComponentResult Invoke()
        {
            return View("AdCarouselPartialView", di?.GetFiles().ToList());
        }
    }
}
