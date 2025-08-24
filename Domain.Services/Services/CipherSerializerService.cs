using Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
    public sealed class CipherSerializerService : ICipherSerializerService
    {
        /// <summary>
        /// The private key
        /// </summary>
        private readonly string privateKey;

        /// <summary>
        /// The eas iv
        /// </summary>
        private readonly string easIv;

        /// <summary>
        /// Initializes a new instance of the <see cref="CipherSerializerService" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public CipherSerializerService(IConfiguration configuration)
        {

            this.privateKey = configuration["KeySecret"]; //debe ser de 16 caracteres por la base
            this.easIv = configuration["IvSecret"]; //debe ser de 16 caracteres por la base
        }

        /// <summary>
        /// Decrypts the specified encrypted text.
        /// </summary>
        /// <param name="encryptedText">The encrypted text.</param>
        /// <returns>System.String.</returns>
        public string Decrypt(string encryptedText)
        {
            var keybytes = Encoding.UTF8.GetBytes(this.privateKey);
            var iv = Encoding.UTF8.GetBytes(this.easIv);

            var encrypted = Convert.FromBase64String(encryptedText);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return decriptedFromJavascript;
        }

        /// <summary>
        /// Encrypts the specified planin text.
        /// </summary>
        /// <param name="plainText">The planin text.</param>
        /// <returns>System.String.</returns>
        public string Encrypt(string plainText)
        {
            var keybytes = Encoding.UTF8.GetBytes(this.privateKey);
            var iv = Encoding.UTF8.GetBytes(this.easIv);

            var encryoFromJavascript = EncryptStringToBytes(plainText, keybytes, iv);
            return Convert.ToBase64String(encryoFromJavascript);
        }

        /// <summary>
        /// Gets the clear data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataCiphered">The data ciphered.</param>
        /// <returns>T.</returns>
        public T GetClearData<T>(string dataCiphered)
        {
            var decryptered = this.Decrypt(dataCiphered);
            var desarializated = JsonConvert.DeserializeObject<string>(decryptered);
            var downloadFileModel = JsonConvert.DeserializeObject<T>(desarializated, new JsonSerializerSettings() { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            return downloadFileModel;
        }

        /// <summary>
        /// Decrypts the string from bytes.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="key">The key.</param>
        /// <param name="iv">The iv.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentNullException">cipherText
        /// or
        /// key
        /// or
        /// key</exception>
        private string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException(nameof(cipherText));
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException(nameof(key));
            }

            string plaintext = null;
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                MemoryStream msDecrypt = null;
                CryptoStream csDecrypt = null;
                try
                {
                    msDecrypt = new MemoryStream(cipherText);
                    csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
                catch (Exception)
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }

        /// <summary>
        /// Encrypts the string to bytes.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <param name="key">The key.</param>
        /// <param name="iv">The iv.</param>
        /// <returns>System.Byte[].</returns>
        /// <exception cref="ArgumentNullException">plainText
        /// or
        /// key
        /// or
        /// key</exception>
        private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException(nameof(plainText));
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException(nameof(key));
            }

            byte[] encrypted = null;
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                MemoryStream msEncrypt = null;
                CryptoStream csEncrypt = null;
                try
                {
                    msEncrypt = new MemoryStream();
                    csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }
                catch (Exception)
                {
                    return encrypted;
                }

                encrypted = msEncrypt.ToArray();
            }

            return encrypted;
        }
    }
}
