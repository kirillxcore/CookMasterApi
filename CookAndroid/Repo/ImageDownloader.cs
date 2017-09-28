using System;
using System.Collections.Generic;
using Android.Graphics;
using System.Net.Http;
using System.Threading.Tasks;
using Android.App;
using Android.Graphics.Drawables;
using Android.Widget;

namespace CookAndroid.Repo
{
    public static class ImageDownloader
    {
        public static void SetUrlAsync(this ImageView image, Activity activity, string url, Drawable drawable)
        {
            if (Cache.ContainsKey(url))
            {
                image.SetImageBitmap(Cache[url]);
                return;
            }

            image.SetImageDrawable(drawable);
            GetImageBitmapFromUrl(url).ContinueWith((a) =>
            {
                activity.RunOnUiThread(() =>
                {
                    if (a.Result != null)
                    {
                        image.SetImageBitmap(a.Result);
                    }
                });
            });
        }

        private static readonly Dictionary<string, Bitmap> Cache = new Dictionary<string, Bitmap>();

        private static Task<Bitmap> GetImageBitmapFromUrl(string url)
        {
            lock (Cache)
            {
                if (Cache.ContainsKey(url))
                {
                    return Task.FromResult(Cache[url]);
                }
            }

            var webClient = new HttpClient();
            return webClient.GetByteArrayAsync(url)
                .ContinueWith(a =>
                {
                    try
                    {
                        var imageBytes = a.Result;
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            var imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                            lock (Cache)
                            {
                                Cache[url] = imageBitmap;
                            }
                            return imageBitmap;
                        }

                        return null;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                })
                .ContinueWith(a =>
                {
                    try
                    {
                        var result = a.Result;
                        webClient.Dispose();
                        return result;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                });
        }
    }
}