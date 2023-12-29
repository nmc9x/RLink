using EncrytionFile.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace BarcodeVerificationSystem.Controller
{
    public class DecryptionHardwareID
    {
        public static string _EncyptionPassword = "rynan_encrypt_remember";
        private static readonly string _EncrytionFilePassword = "RhapsodosZyl_ft_Tieunhan1st";
        public static void DecryptFile(string inputFile)
        {
            try
            {
                string password = _EncrytionFilePassword;
                byte[] key = CreateKey(password);
                var fsCrypt = new FileStream(inputFile, FileMode.Open);
                var RMCrypto = new RijndaelManaged
                {
                    BlockSize = 256,
                    KeySize = 256,
                    IV = key,
                    Key = key
                };

                using (var cs = new CryptoStream(fsCrypt, RMCrypto.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (var reader = new StreamReader(cs))
                    {
                       var hardwareIDList = new List<string>();

                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            hardwareIDList.Add(string.Concat(line));
                        }
                        foreach (string hw in hardwareIDList)
                        {
                            var hardwareID = new HardwareIDModel
                            {
                                HardwareID = hw
                            };
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
