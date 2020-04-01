using Microsoft.AspNetCore.Mvc;
using tsv_core.Models;
using System.Linq;

namespace tsv_core.Controllers
{
    public class HomeController : Controller
    {
        //[HttpGet]
        //public ViewResult Gallerien()
        //{    
        //    ViewBag.Post = false;
        //    return View(Gallery.Galleries);
        //}

        //[HttpPost]
        public ViewResult Galerien(string galleryName)
        {
            if (!string.IsNullOrEmpty(galleryName) && Gallery.Galleries.Where(di => di.Name == galleryName).ToList().Count() != 0)
            {
                ViewBag.Post = true;
                return View(Gallery.Galleries.Where(di => di.Name == galleryName).ToList());
            }

            ViewBag.Post = false;
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            return View(Gallery.Galleries);
        }

        //[Route("robots.txt")]
        public VirtualFileResult RobotsText()
        {
            return File("/robots.txt", "text/enriched");
        }

        //[Route("sitemap.xml")]
        public VirtualFileResult SitemapXml()
        {
            return File("/sitemap.xml", "text/enriched");
        }

        public ViewResult Error()
        {
            return View("Error");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Vereinschronik()
        {
            return View();
        }

        public IActionResult Impressionen()
        {
            return View();
        }
        public IActionResult Vorstandschaft()
        {
            return View();
        }
        public IActionResult Werbepartner()
        {
            return View();
        }
        public IActionResult Damengymnastik()
        {
            return View();
        }
        public IActionResult Fußball_allgemein()
        {
            return View();
        }
        public IActionResult Begegnungen()
        {
            return View();
        }
        public IActionResult Mannschaft1()
        {
            return View();
        }
        public IActionResult Mannschaft2()
        {
            return View();
        }
        public IActionResult Damenmannschaft()
        {
            return View();
        }
        public IActionResult A_Jugend()
        {
            return View();
        }
        public IActionResult B_Jugend()
        {
            return View();
        }
        public IActionResult C_Jugend()
        {
            return View();
        }

        public IActionResult D_Jugend()
        {
            return View();
        }

        public IActionResult E_Jugend()
        {
            return View();
        }
        public IActionResult F_Jugend()
        {
            return View();
        }
        public IActionResult G_Jugend()
        {
            return View();
        }
        public IActionResult Leichtathletik()
        {
            return View();
        }
        public IActionResult Kinder_Turnen()
        {
            return View();
        }
        public IActionResult Tennis()
        {
            return View();
        }
        public IActionResult Volleyball()
        {
            return View();
        }
        public IActionResult Routenplaner()
        {
            return View();
        }
        public IActionResult Vereinsshop()
        {
            return View();
        }
        public IActionResult Kontakt()
        {
            return View();
        }
        public IActionResult Impressum()
        {
            return View();
        }
        public IActionResult Datenschutzerklaerung()
        {
            return View();
        }

        public IActionResult Pachtinfo()
        {
            return View();
        }
    }
}
