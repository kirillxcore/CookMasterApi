using System.Collections.Generic;
using Android.Graphics;
using System.Net;
using Java.Util;

namespace CookAndroid.Repo
{
    public static class ImageDownloader
    {
        public static Dictionary<string, Bitmap> cache = new Dictionary<string, Bitmap>();

        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            if (cache.ContainsKey(url))
            {
                return cache[url];
            }

            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            cache[url] = imageBitmap;

            return imageBitmap;
        }
    }
}