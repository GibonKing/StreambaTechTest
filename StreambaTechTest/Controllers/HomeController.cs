using StreambaTechTest.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace StreambaTechTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Models.Image> images = PhotoDA.LoadPictures(ConfigurationManager.AppSettings["rootPath"]);
            return View(images);
        }

        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public void UploadFiles(FormCollection data)
        {
            List<FileInfo> files = new List<FileInfo>();
            List<byte[]> byteArrays = new List<byte[]>();
            string rootPath = ConfigurationManager.AppSettings["rootPath"];

            //Format Name
            string name = data["name"].ToString();
            if (!String.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                name = Regex.Replace(name, @"\s+", "_");
            }
            else
            {
                name = "Anonymous";
            }

            //Read uploaded images and create a ByteArrays and FileInfo for them.
            if (Request.Files["file"] != null)
            {
                using (var binaryReader = new BinaryReader(Request.Files["file"].InputStream))
                {
                    byte[] byteArray = binaryReader.ReadBytes(Request.Files["file"].ContentLength);
                    FileInfo file = new FileInfo($"{rootPath}{name}_{Request.Files["file"].FileName.Replace("_","-")}");
                    file.Directory.Create();
                    PhotoDA.UploadPhoto(file, byteArray);
                }
            }
        }
    }
}