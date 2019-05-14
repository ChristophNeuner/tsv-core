using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using tsv_core.Models;

namespace tsv_core.Controllers
{
    [Authorize]
    public class UserAreaController : Controller
    {
        private IHostingEnvironment hostingEnv;
        private string galleriesPath;

        public UserAreaController(IHostingEnvironment env)
        {
            hostingEnv = env;
            galleriesPath = hostingEnv.WebRootPath + "/images/gallerien/";
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateGallery()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGallery(List<IFormFile> files, [Required]string folderName)
        {
            string folderPath = galleriesPath + folderName;
            Directory.CreateDirectory(folderPath);
            long size = 0;
            int fileCount = 0;
            foreach (var file in files)
            {
                string filetype = file.ContentType;
                if (filetype.Contains("image"))
                {
                    fileCount++;
                    string filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .ToString()
                                .Trim('"');
                    var filepath = folderPath + $@"/{filename}";
                    size += file.Length;
                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        await file.CopyToAsync(fs);
                        //file.CopyTo(fs);
                        //fs.Flush();
                    }
                }
            }
            TempData["Message"] = $"{fileCount} file(s) / {size} bytes uploaded successfully! {folderPath}";
            return RedirectToAction("Gallerien", "Home");
        }

        [HttpPost]
        public IActionResult DeleteGallery(string folderName)
        {
            string folderPath = galleriesPath + folderName;
            Directory.Delete(folderPath, true);
            return RedirectToAction("Gallerien", "Home");
        }
    }
}
