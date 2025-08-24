using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface ICipherSerializerService
    {
        /// <summary>
        /// Decrypts the specified encrypted text.
        /// </summary>
        /// <param name="encryptedText">The encrypted text.</param>
        /// <returns>System.String.</returns>
        string Decrypt(string encryptedText);

        /// <summary>
        /// Encrypts the specified planin text.
        /// </summary>
        /// <param name="plainText">The planin text.</param>
        /// <returns>System.String.</returns>
        string Encrypt(string plainText);

        /// <summary>
        /// Gets the clear data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataCiphered">The data ciphered.</param>
        /// <returns>T.</returns>
        T GetClearData<T>(string dataCiphered);
    }
}
