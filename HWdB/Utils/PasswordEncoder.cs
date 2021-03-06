﻿using System;
using System.Text;
using System.Security.Cryptography;


namespace HWdB.Utils
{
    public class PasswordEncoder
    {
        const string SALT = @"Salt-for-HWdB!";
        public static string GetMd5Encoding(string password)
        {
            MD5 md5Hash = MD5.Create();
            return CreateMd5Encoding(md5Hash, password + SALT);
        }

        private static string CreateMd5Encoding(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static bool VerifyMd5Encoding(string accountPassword, string dbPassword)
        {
            MD5 md5Hash = MD5.Create();
            return CheckMd5Encoding(md5Hash, accountPassword, dbPassword);
        }

        private static bool CheckMd5Encoding(MD5 md5Hash, string accountPassword, string hashPassword)
        {
            // Hash the accountPassword. 
            string hashOfInput = GetMd5Encoding(accountPassword);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hashPassword) == 0;
        }
    }
}
