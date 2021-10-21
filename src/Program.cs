using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace C_SHARP_RSA_EXAMPLE
{
    class Program
    {
        static void Main(string[] args)
        {
            string cmd = "";
            Console.Title = "C# RSA Encrypt/Decrpt by Minosuko";
            Console.WriteLine("C# RSA Crypt");
            while (true)
            {
                Console.Write("RSA > ");
                cmd = Console.ReadLine();
                MainFunction.DoCmd(cmd);
                Console.WriteLine();
            }
        }
    }
}
