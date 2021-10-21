using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace C_SHARP_RSA_EXAMPLE
{
    class MainFunction
    {
        private static string GetKey(string type)
        {
            if (File.Exists("./RSA.key.xml"))
            {
                using (StreamReader Write = new StreamReader("./RSA.key.xml"))
                {
                    string key = Write.ReadToEnd();
                    return key;
                }
            }
            else
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.KeySize = 2048;
                string key = rsa.ToXmlString(true);
                using (StreamWriter Write = new StreamWriter("./RSA.key.xml"))
                {
                    Write.Write(key);
                }
                return key;
            }
        }
        public static void DoCmd(string cmd)
        {
            try
            {

                int cmd_len = cmd.Length;
                if (cmd_len < 7)
                {
                    cmd = "---help";
                }
                if (cmd.Substring(0, 7) == "encrypt")
                {
                    string key = GetKey("pub");
                    int str_lenght = cmd.Length;
                    string filename = cmd.Substring(8, str_lenght - 8);
                    using (StreamReader read = new StreamReader(filename))
                    {
                        string data = read.ReadToEnd();
                        using (StreamWriter write = new StreamWriter(filename + ".rsa.encrypt.txt"))
                        {
                            Encoding wind1252 = Encoding.GetEncoding(1252);
                            byte[] b_data = wind1252.GetBytes(data);
                            byte[] crypt = RSA_crypt.Encryption(b_data, key);
                            string s_data = wind1252.GetString(crypt);
                            write.Write(s_data);
                        }
                    }
                    Console.Write("Encrypt finished");
                }
                else if (cmd.Substring(0, 7) == "decrypt")
                {
                    int str_lenght = cmd.Length;
                    string filename = cmd.Substring(8, str_lenght - 8);
                    string key = GetKey("prv");
                    RSA rsa = RSA.Create();
                    rsa.FromXmlString(key);
                    RSAParameters private_key = rsa.ExportParameters(true);
                    using (StreamReader read = new StreamReader(filename))
                    {
                        string data = read.ReadToEnd();
                        using (StreamWriter write = new StreamWriter(filename + ".rsa.decrypt.txt"))
                        {
                            Encoding wind1252 = Encoding.GetEncoding(1252);
                            byte[] b_data = wind1252.GetBytes(data);
                            byte[] crypt = RSA_crypt.Decryption(b_data, key);
                            string s_data = wind1252.GetString(crypt);
                            write.Write(s_data);
                        }
                    }
                    Console.Write("Decrypt finished");
                }
                else if (cmd.Substring(0, 7) == "version")
                {
                    Console.WriteLine("Version: 1.0.0");
                }
                else if (cmd.Substring(0, 7) == "---exit")
                {
                    Environment.Exit(0);
                }
                else if (cmd.Substring(0, 7) == "---info")
                {
                    Console.WriteLine("C# RSA Encrypt/Decrypt");
                    Console.WriteLine("By Minosuko");
                    Console.WriteLine("github: https://github.com/Minosuko");
                    Console.WriteLine("source code: https://github.com/Minosuko/RSA_Crypt_CS");
                }
                else if (cmd.Substring(0, 7) == "---help")
                {
                    Console.WriteLine("C# RSA Encrypt/Decrypt");
                    Console.WriteLine("Endrypt: encrypt [filename]");
                    Console.WriteLine("Dedrypt: decrypt [filename]");
                    Console.WriteLine("Version: version");
                    Console.WriteLine("Exit: ---exit");
                    Console.WriteLine("Info: ---info");
                }
                else
                {
                    Console.WriteLine("Invalid argument");
                    Console.WriteLine("type ---help for help");
                }
            }
            catch (Exception e)
            {
                using (StreamWriter Dump = new StreamWriter("c-sharp_rsa-MainFunction-error.txt"))
                {
                    Dump.Write(e.Message.ToString());
                }
                Console.WriteLine("Sorry, this application having some error with MainFunction Class");
                Console.WriteLine("Check error at c-sharp_rsa-MainFunction-error.txt");
            }
        }
    }
}
