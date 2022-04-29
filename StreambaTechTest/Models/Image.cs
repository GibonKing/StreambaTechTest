using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreambaTechTest.Models
{
    public class Image
    {
        public string Scr { get; set; }
        public string ImageName { get; set; }
        public string UserName { get; set; }

        public Image(string scr, string imageName, string userName)
        {
            Scr = scr;
            ImageName = imageName;
            UserName = userName;
        }
    }
}