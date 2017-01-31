using System;
using System.Collections.Generic;
using notcreepy.Models;
using Microsoft.AspNetCore.Mvc;
using notcreepy.Factory; //Need to include reference to new Factory Namespace
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;

namespace notcreepy.Controllers
{
    public class HomeController : Controller
    {
        // private IHostingEnvironment hostingEnv;
     
        private readonly UserFactory userFactory;

        public HomeController(UserFactory user) {
            userFactory = user;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            //We can call upon the methods of the userFactory directly now.
            return View();
        }

        // [HttpPost]
        // [RouteAttribute("upload")]
        // public IActionResult UploadFiles(IList<IFormFile> files)
        // {
        //     long size = 0;
        //     foreach (var file in files)
        //     {
        //         ViewBag.Photo = file;
        //         var filename = ContentDispositionHeaderValue
        //                         .Parse(file.ContentDisposition)
        //                         .FileName
        //                         .Trim('"');
        //                         ViewBag.Test = filename;
        //         filename = hostingEnv.WebRootPath + $@"\{filename}";
        //         size += file.Length;
        //         using (FileStream fs = System.IO.File.Create(filename))
        //         {
        //             file.CopyTo(fs);
        //             fs.Flush();
        //             ViewBag.fs = fs;
        //         }
        //     }
        //     ViewBag.Message = $"{files.Count} file(s) uploaded successfully!";
        //     return View("Index");
        // }

          [HttpPost]
        [RouteAttribute("upload")]
        public IActionResult UploadFiles(IList<IFormFile> files)
        {
            // long size = 0;
            foreach (var file in files)
            {
                byte[] image = userFactory.ConvertToBytes(file);
                System.Console.WriteLine(image + "******************************************");
                // ViewBag.Photo = file;
                // var filename = ContentDispositionHeaderValue
                //                 .Parse(file.ContentDisposition)
                //                 .FileName
                //                 .Trim('"');
                //                 ViewBag.Test = filename;
                // filename = hostingEnv.WebRootPath + $@"\{filename}";
                // size += file.Length;
                // using (FileStream fs = System.IO.File.Create(filename))
                // {
                //     file.CopyTo(fs);
                //     fs.Flush();
                //     ViewBag.fs = fs;
                // }
            }  
            ViewBag.Message = $"{files.Count} file(s) uploaded successfully!";
            return View("Index");
        }
    }
}