using System.Security.Cryptography;
using System.Text;

namespace cipher
{
       public static class CipherManager
       {
              /// <summary>
              /// Encrypts the given text using the specified key
              /// </summary>
              /// <param name="textToEncrypt"></param>
              /// <param name="key"></param>
              /// <param name="ivLength"></param>
              /// <returns></returns>
              public static async Task<string> EncryptStringAsync(string textToEncrypt, string key, int ivLength = 16)
              {
                     byte[] iv = new byte[ivLength];
                     byte[] result;

                     using (Aes aesAlg = Aes.Create())
                     {
                            aesAlg.Key = Encoding.UTF8.GetBytes(key);
                            aesAlg.IV = iv;
                            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                   using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                                   {
                                          using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                                          {
                                                 await streamWriter.WriteAsync(textToEncrypt);
                                          }
                                          result = memoryStream.ToArray();
                                   }
                            }
                     }
                     return Convert.ToBase64String(result);
              }

              /// <summary>
              /// Decrypts the given text using the specified key
              /// </summary>
              /// <param name="encryptedData"></param>
              /// <param name="key"></param>
              /// <param name="ivLength"></param>
              /// <returns></returns>
              public static async Task<string> DecryptAsync(string encryptedData, string key, int ivLength = 16)
              {
                     byte[] iv = new byte[ivLength];
                     byte[] buffer = Convert.FromBase64String(encryptedData);

                     using (Aes aesAlg = Aes.Create())
                     {
                            aesAlg.Key = Encoding.UTF8.GetBytes(key);
                            aesAlg.IV = iv;
                            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                            using (MemoryStream memoryStream = new MemoryStream(buffer))
                            {
                                   using (CryptoStream cryptoScream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                   {
                                          using (StreamReader streamReader = new StreamReader(cryptoScream))
                                          {
                                                 return await streamReader.ReadToEndAsync();
                                          }
                                   }
                            }
                     }
              }
       }
}