using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace StreambaTechTest.DataAccess
{
    public static class PhotoDA
    {
        public static void UploadPhoto(FileInfo file, byte[] byteArray)
        {
            File.WriteAllBytes(file.FullName, byteArray);
        }

        public static List<Models.Image> LoadPictures(string folderPath)
        {
            List<Models.Image> images = new List<Models.Image>();
            foreach (string file in Directory.GetFiles(folderPath))
            {
                //Get image src from byteArray
                byte[] byteArray = File.ReadAllBytes(file);
                string imageBase64 = Convert.ToBase64String(byteArray);
                string imageScr = string.Format("data:image/gif;base64,{0}", imageBase64);

                //Get UserName and ImageName from FileName
                string[] s = Path.GetFileNameWithoutExtension(file).Split('\\').Last().Split('_');
                List<string> names = new List<string>();
                for(int i = 0; i < (s.Count() - 1); i++)
                {
                    names.Add(s[i]);
                }
                string userName = String.Join(" ", names);
                string imageName = s.Last();

                images.Add(new Models.Image(imageScr, imageName, userName));

            }
            return images;
        }
    }
}