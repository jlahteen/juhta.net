
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Juhta.Net.Common
{
    /// <summary>
    /// Defines a class that simplifies the use of symmetric cryptographic service providers.
    /// </summary>
    public class SymmetricCipher
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <remarks>Instances created with this constructor use the <see cref="TripleDES"/> algorithm.</remarks>
        public SymmetricCipher() : this(new TripleDESCryptoServiceProvider())
        {}

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="symmetricAlgorithm">Specifies a SymmetricAlgorithm object.</param>
        public SymmetricCipher(SymmetricAlgorithm symmetricAlgorithm)
        {
            m_symmetricAlgorithm = symmetricAlgorithm;

            var keySizes =
                from keySize in symmetricAlgorithm.LegalKeySizes
                orderby keySize.MaxSize descending
                select keySize;

            m_keySize = keySizes.First().MaxSize;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Decrypts a specified array of bytes containing encrypted data.
        /// </summary>
        /// <param name="cipherKey">Specifies a cipher key.</param>
        /// <param name="encryptedData">Specifies an array of bytes containing encrypted data.</param>
        /// <returns>Returns an array of bytes containing the plain data.</returns>
        public byte[] DecryptData(string cipherKey, byte[] encryptedData)
        {
            byte[] key, iv, plainBytes;
            CryptoStream cryptoStream;
            Stream encryptedDataStream = new MemoryStream(encryptedData);
            BinaryReader cryptoStreamReader = null;
            MemoryStream plainDataStream = new MemoryStream();

            try
            {
                CreateKeyAndInitializationVector(cipherKey, out key, out iv);

                cryptoStream = new CryptoStream(encryptedDataStream, m_symmetricAlgorithm.CreateDecryptor(key, iv), CryptoStreamMode.Read);

                cryptoStreamReader = new BinaryReader(cryptoStream);

                do
                {
                    plainBytes = cryptoStreamReader.ReadBytes(UInt16.MaxValue);

                    if (plainBytes.Length > 0)
                        plainDataStream.Write(plainBytes, 0, plainBytes.Length);
                }
                while (plainBytes.Length > 0);

                return(plainDataStream.ToArray());
            }

            finally
            {
                if (cryptoStreamReader != null)
                    cryptoStreamReader.Close();
            }
        }

        /// <summary>
        /// Encrypts a specified array of bytes containing plain data.
        /// </summary>
        /// <param name="cipherKey">Specifies a cipher key.</param>
        /// <param name="plainData">Specifies an array of bytes containing plain data.</param>
        /// <returns>Returns an array of bytes containing the encrypted data.</returns>
        public byte[] EncryptData(string cipherKey, byte[] plainData)
        {
            byte[] key, iv;
            CryptoStream cryptoStream;
            MemoryStream encryptedDataStream = new MemoryStream();
            BinaryWriter cryptoStreamWriter = null;

            try
            {
                CreateKeyAndInitializationVector(cipherKey, out key, out iv);

                cryptoStream = new CryptoStream(encryptedDataStream, m_symmetricAlgorithm.CreateEncryptor(key, iv), CryptoStreamMode.Write);

                cryptoStreamWriter = new BinaryWriter(cryptoStream);

                cryptoStreamWriter.Write(plainData);

                cryptoStreamWriter.Flush();

                cryptoStream.FlushFinalBlock();

                return(encryptedDataStream.ToArray());
            }

            finally
            {
                if (cryptoStreamWriter != null)
                    cryptoStreamWriter.Close();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a key and initialization vector based on a specified cipher key.
        /// </summary>
        /// <param name="cipherKey">Specifies a cipher key.</param>
        /// <param name="key">A byte array that returns the key for the specified cipher key.</param>
        /// <param name="iv">A byte array that returns the initialization vector for the specified cipher key.</param>
        private void CreateKeyAndInitializationVector(string cipherKey, out byte[] key, out byte[] iv)
        {
            Random random = new Random(cipherKey.GetHashCode());

            key = new byte[m_keySize];

            random.NextBytes(key);

            iv = new byte[m_keySize];

            random.NextBytes(iv);
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies the key size to use with the associated SymmetricAlgorithm object. The class determines the key
        /// size by selecting the maximum key size supported by <see cref="m_symmetricAlgorithm"/>.
        /// </summary>
        private int m_keySize;

        /// <summary>
        /// Specifies a SymmetricAlgorithm object that performs actual encryption and decryption operations.
        /// </summary>
        private SymmetricAlgorithm m_symmetricAlgorithm;

        #endregion
    }
}
