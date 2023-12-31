﻿using System.Security.Cryptography;
using System.Text;

namespace eXtensionSharp
{
    public static class XCryptSHA512
    {
        /// <summary>
        ///     SHA512 Encrypt (Decrypt is not support.)
        /// </summary>
        /// <param name="encryptText"></param>
        /// <returns></returns>
        public static string xToSHA512(this string encryptText)
        {
            using (var sha512 = SHA512.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(encryptText);
                var hash = sha512.ComputeHash(bytes);
                var sb = new StringBuilder();

                for (var i = 0; i < hash.Length; i++) sb.Append(hash[i].ToString("X2"));

                return sb.ToString();
            }
        }
    }
}