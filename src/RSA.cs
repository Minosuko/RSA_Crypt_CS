using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace C_SHARP_RSA_EXAMPLE
{
    class RSA_crypt
    {
        static public byte[] Encryption(byte[] Data, string key, bool DoOAEPPadding = false)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.FromXmlString(key);
                    RSAParameters pkey = RSA.ExportParameters(true);
                    RSA.ImportParameters(pkey);
                    encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                using (StreamWriter Dump = new StreamWriter("c-sharp_rsa-RSA-error.txt"))
                {
                    Dump.Write(e.Message.ToString());
                }
                Console.WriteLine("Sorry, this application having some error with RSA Class");
                Console.WriteLine("Check error at c-sharp_rsa-RSA-error.txt");
                return null;
            }
        }
        static public byte[] Decryption(byte[] Data, string key, bool DoOAEPPadding = false)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.FromXmlString(key);
                    RSAParameters pkey = RSA.ExportParameters(true);
                    RSA.ImportParameters(pkey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                using (StreamWriter Dump = new StreamWriter("c-sharp_rsa-RSA-error.txt"))
                {
                    Dump.Write(e.Message.ToString());
                }
                Console.WriteLine("Sorry, this application having some error with RSA Class");
                Console.WriteLine("Check error at c-sharp_rsa-RSA-error.txt");
                return null;
            }
        }
    }
}
