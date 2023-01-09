using System.Net;
using System.Net.Http;
using SkiaSharp;

namespace DogApi.BL
{
    public class ImageHandler
    {
        //create a Bitmap class 

         public static async Task<string> GetImageColor(string url)
         {

             var bitmap = await WebImage(url);

             var color = bitmap.GetPixel(bitmap.Width / 2, bitmap.Height / 2);
             return $"#{color.Red:X2}{color.Green:X2}{color.Blue:X2}";
         }
         private static async Task<SKBitmap?> WebImage(string url)
         {
            var httpClient = new HttpClient(); 
            var bytes = await httpClient.GetByteArrayAsync(url);

            var stream = new MemoryStream(bytes);

            var bitmap = SKBitmap.Decode(stream);

            return bitmap;
         }

    }
}
