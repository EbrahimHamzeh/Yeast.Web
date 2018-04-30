using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Yeast.Utilities
{
    public static class Filepond
    {
        public static List<ImageJSON> DeserlizerArrayImageUpload(string[] input)
        {
            List<ImageJSON> imageList = new List<ImageJSON>();
            if (input.Length > 0)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                foreach (var item in input)
                {
                    imageList.Add(serializer.Deserialize<ImageJSON>(item));
                }
            }
            return imageList;
        }

        public static string SerializeImageUpload(List<ImageJSON> images)
        {
            return JsonConvert.SerializeObject(images);
        }

        public static List<ImageJSON> DeserializeImageUpload(string json)
        {
            return JsonConvert.DeserializeObject<List<ImageJSON>>(json); ;
        }

        public static string SaveArrayImageToJsonString(string[] input)
        {
            List<ImageJSON> deserializ = DeserlizerArrayImageUpload(input);
            return SerializeImageUpload(deserializ);
        }
    }

    public class ImageJSON
    {
        public string link { get; set; }
    }
}
