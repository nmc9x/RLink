using CommonVariable;
using Microsoft.Win32;
using System.Diagnostics;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

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
            using (var webClient = new WebClient())
            {
                byte[] imageData = webClient.DownloadData(uri);
                using (var ms = new MemoryStream(imageData))
                {
                    var image = new Bitmap(ms);
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
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                memoryStream.Position = 0;
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.CopyTo(fileStream);
                }
            }
        }

        public static void SaveKeyToRegistry(string idPC)
        {
            string registryPath = @"SOFTWARE\RLinkHelpFolder";
            var key = Registry.LocalMachine.OpenSubKey(registryPath, true) ?? Registry.LocalMachine.CreateSubKey(registryPath);
            key.SetValue("IDPC", idPC);
            key.Close();
        }

        public static void OpenDialog(string dirPath)
        {
            try
            {
                using (var openFileDialog = new System.Windows.Forms.OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = dirPath;
                    openFileDialog.Filter = "Image files (*.bmp)|*.bmp";
                    openFileDialog.FilterIndex = 0;
                    openFileDialog.Multiselect = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedFile = openFileDialog.FileName;
                        Process.Start("notepad.exe", selectedFile);
                    }
                }
            }
            catch (Exception) { }
        }
    }
}
