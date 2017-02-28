using System.IO;
using System.Security.Cryptography;

namespace LANChat
{
    internal static class Security
    {
        //  The initialization vector for the algorithm.
        private static byte[] IV = { 91, 139, 119, 203, 109, 221, 242, 14, 169, 115, 63, 77, 110, 214, 201, 149 };
        //  The secret key for the algorithm.
        private static byte[] Key = { 40, 13, 56, 123, 152, 175, 138, 37, 227, 111, 40, 36, 156, 220, 151, 9, 235, 
                                        58, 189, 181, 49, 143, 73, 183, 50, 130, 87, 17, 220, 116, 124, 199 };

        private static byte[] GetIV()
        {
            RijndaelManaged rijndael = new RijndaelManaged();
            return rijndael.IV;
        }

        private static byte[] GetKey()
        {
            RijndaelManaged rijndael = new RijndaelManaged();
            return rijndael.Key;
        }

        public static byte[] EncryptString(string plainText)
        {
            // Declare the stream used to encrypt to an in memory
            // array of bytes.
            MemoryStream msEncrypt = null;

            // Declare the RijndaelManaged object
            // used to encrypt the data.
            RijndaelManaged aesAlg = null;

            try {
                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                msEncrypt = new MemoryStream();
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {

                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                }
            }
            finally {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return msEncrypt.ToArray();
        }

        public static string DecryptString(byte[] cipherText)
        {
            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            try {
                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText)) {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }
    }
}
