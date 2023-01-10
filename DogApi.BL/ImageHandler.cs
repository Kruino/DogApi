using System.Net;
using System.Net.Http;
using SkiaSharp;

namespace DogApi.BL
{
    public class ImageHandler
    {
        //create a Bitmap class 

         public static async Task<string> GetAvarageRGB(string url)
         {
             var bitmap = await WebImage(url);

             int r = 0;
             int g = 0;
             int b = 0;

             int total = 0;

             if (bitmap != null)
                 for (int x = 0; x < bitmap.Width / 4; x += 4)
                 {
                     for (int y = 0; y < bitmap.Height / 4; y += 4)
                     {
                         var clr = bitmap.GetPixel(x, y);

                         r += clr.Red;
                         g += clr.Green;
                         b += clr.Blue;

                         total++;
                     }
                 }

             r /= total;
             g /= total;
             b /= total;

            return $"#{r:X2}{g:X2}{b:X2}";
         }
         public static async Task<SKBitmap?> WebImage(string url)
         {
            var httpClient = new HttpClient(); 
            var bytes = await httpClient.GetByteArrayAsync(url);

            var stream = new MemoryStream(bytes);

            var bitmap = SKBitmap.Decode(stream);

            return bitmap;
         }
    }
}
