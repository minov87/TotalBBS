using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TotalBBS.Common.WebBase
{
    // TripleDES 재정의를 위해 변수 별도 추가

    public class TripleDES
    {
        // Fields
        private static byte[] iv = new byte[] 
        {
            0xc1, 0xf7, 0x23, 0x38, 60, 15, 0xf1, 9
        };
        private static byte[] key = new byte[] 
        {
            0x39, 0x81, 0x7d, 0x76, 0xe9, 60, 13, 0x5e, 0x99, 0x9c, 0xbc, 9, 0x6d, 20, 0x8a, 7,
            0x1f, 0xdd, 0xd7, 0x5b, 0xf1, 0x52, 0xfe, 0xbd
        };

        // Methods
        public string DecryptData(string data)
        {
            string str = string.Empty;
            byte[] bytIn = null;
            byte[] bytes = null;
            try
            {
                bytIn = Convert.FromBase64String(data);
                bytes = this.DecryptData(bytIn);
                str = Encoding.UTF8.GetString(bytes);
            }
            catch
            {
            }
            return str;
        }

        public byte[] DecryptData(byte[] bytIn)
        {
            byte[] destinationArray = null;
            MemoryStream stream = null;
            TripleDESCryptoServiceProvider provider = null;
            CryptoStream stream2 = null;
            ICryptoTransform transform = null;
            try
            {
                stream = new MemoryStream(bytIn, 0, bytIn.Length);
                provider = new TripleDESCryptoServiceProvider();
                transform = provider.CreateDecryptor(key, iv);
                stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
                byte[] buffer = new byte[bytIn.Length];
                int length = stream2.Read(buffer, 0, buffer.Length);
                destinationArray = new byte[length];
                Array.Copy(buffer, 0, destinationArray, 0, length);
            }
            catch
            {
            }
            finally
            {
                if (stream2 != null)
                {
                    stream2.Clear();
                    stream2 = null;
                }
                if (transform != null)
                {
                    transform.Dispose();
                    transform = null;
                }
                if (provider != null)
                {
                    provider.Clear();
                    provider = null;
                }
                if (stream != null)
                {
                    stream = null;
                }
            }
            return destinationArray;
        }

        public string EncryptData(string data)
        {
            string str = string.Empty;
            byte[] bytIn = null;
            byte[] inArray = null;
            try
            {
                bytIn = Encoding.UTF8.GetBytes(data);
                inArray = this.EncryptData(bytIn);
                str = Convert.ToBase64String(inArray, 0, inArray.Length);
            }
            catch
            {
            }
            return str;
        }

        public byte[] EncryptData(byte[] bytIn)
        {
            byte[] buffer = null;
            MemoryStream stream = null;
            TripleDESCryptoServiceProvider provider = null;
            CryptoStream stream2 = null;
            ICryptoTransform transform = null;
            try
            {
                stream = new MemoryStream();
                provider = new TripleDESCryptoServiceProvider();
                transform = provider.CreateEncryptor(key, iv);
                stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
                stream2.Write(bytIn, 0, bytIn.Length);
                stream2.FlushFinalBlock();
                buffer = stream.ToArray();
            }
            catch
            {
            }
            finally
            {
                if (stream2 != null)
                {
                    stream2.Clear();
                    stream2 = null;
                }
                if (transform != null)
                {
                    transform.Dispose();
                    transform = null;
                }
                if (provider != null)
                {
                    provider.Clear();
                    provider = null;
                }
                if (stream != null)
                {
                    stream = null;
                }
            }
            return buffer;
        }
    }
}
