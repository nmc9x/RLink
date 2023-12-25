using EncrytionFile.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BarcodeVerificationSystem.Controller
{
    /// <summary>
    /// @Author: Dung Le
    /// @Create date: 11/08/2022
    /// @Decryption allow ID
    /// </summary>
    public class DecryptionHardwareID
    {
        public static string _EncyptionPassword = "rynan_encrypt_remember";
        private static string _EncrytionFilePassword = "RhapsodosZyl_ft_Tieunhan1st";

        public static void DecryptFile(string inputFile)
        {
            try
            {
                string password = _EncrytionFilePassword;

                byte[] key = CreateKey(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();
                RMCrypto.BlockSize = 256;
                RMCrypto.KeySize = 256;
                RMCrypto.IV = key;
                RMCrypto.Key = key;

                using (var cs = new CryptoStream(fsCrypt, RMCrypto.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (var reader = new StreamReader(cs))
                    {
                        List<string> hardwareIDList = new List<string>();

                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();

                            hardwareIDList.Add(string.Concat(line));
                        }

                        foreach (string hw in hardwareIDList)
                        {
                            HardwareIDModel hardwareID = new HardwareIDModel();
                            hardwareID.HardwareID = hw;
                            Shared.listPCAllow.Add(hardwareID);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }

        }
        public static void DecryptFile_OLD(string inputFile)
        {
            try
            {
                if(!File.Exists(inputFile)) { return; }
                string password = _EncrytionFilePassword;

                byte[] key = CreateKey(password);
                
                FileStream fsCrypt = new FileStream(inputFile,FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();
                RMCrypto.BlockSize = 256;
                RMCrypto.KeySize = 256;
                RMCrypto.IV = key;
                RMCrypto.Key = key;

                using (var cs = new CryptoStream(fsCrypt,RMCrypto.CreateDecryptor(),CryptoStreamMode.Read))
                {
                    using (var reader = new StreamReader(cs))
                    {
                        List<string> hardwareIDList = new List<string>();
                        
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();

                            hardwareIDList = JsonConvert.DeserializeObject<List<string>>(line);
                        }
                        foreach (string hw in hardwareIDList)
                        {
                            HardwareIDModel hardwareID = new HardwareIDModel();
                            string a = SecurityController.Decrypt(hw, _EncyptionPassword);
                            Console.WriteLine(a);
                            hardwareID.HardwareID = hw;
                            Shared.listPCAllow.Add(hardwareID);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        private static readonly byte[] Salt = new byte[] { 10,20,30,40,50,60,70,80 };
        private static byte[] CreateKey(string password,int keyBytes = 32)
        {
            const int Iterations = 300;
            var keyGenerator = new Rfc2898DeriveBytes(password,Salt,Iterations);
            return keyGenerator.GetBytes(keyBytes);
        }
    }
}
