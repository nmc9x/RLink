using System.Drawing;
using System.IO;
using System.Net;

namespace BarcodeVerificationSystem.Controller
{
    public static class UtilityFunctions
    {
        public static int BoolsToInt(bool[] bools)
        {
            int value = 0;
            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i])
                    value |= 1 << (bools.Length - i - 1);
            }
            return value;
        }
        public static bool[] IntToBools(int value, int numBools)
        {
            bool[] bools = new bool[numBools];
            for (int i = 0; i < numBools; i++)
            {
                bools[i] = (value & (1 << (numBools - i - 1))) != 0;
            }
            return bools;
        }
        public static Bitmap GetImageFromUri(string uri)
        {
            if(uri == "") return null;
            using (WebClient webClient = new WebClient())
            {
                byte[] imageData = webClient.DownloadData(uri);
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    Bitmap image = new Bitmap(ms);
                    return image;
                }
            }
        }
        public static void SaveBitmap(Bitmap bitmap, string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                memoryStream.Position = 0;
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.CopyTo(fileStream);
                }
            }
        }
    }
}
