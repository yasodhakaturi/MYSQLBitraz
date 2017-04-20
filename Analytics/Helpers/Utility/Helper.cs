using HashidsNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Analytics.Helpers.Utility
{
    public class Helper
    {
        public static string GetHashID(int id)
        {
            var salt = new Hashids("this is my salt", 5);
            var hashid = salt.Encode(id);
            return hashid;
        }
        public static string GetUTCTime()
        {
            string datetimestr = DateTime.UtcNow.Year + "-" + DateTime.UtcNow.Month + "-" + DateTime.UtcNow.Day + " " + DateTime.UtcNow.TimeOfDay;
            return datetimestr;
        }
        //public static void GenerateUniqueIDs()
        
        //{
        //    int length = 5; int t=0;
        //    shortenurlEntities dc = new shortenurlEntities();
        //    const string alphanumericCharacters =
        //        "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
        //        "abcdefghijklmnopqrstuvwxyz" +
        //        "0123456789";
        //    try
        //    {
        //        //for (int r = 0; r < 1000; r++)
        //        //{
        //            var characterArray = alphanumericCharacters.Distinct().ToArray();
        //            //if (characterArray.Length == 0)
        //            //    throw new ArgumentException("characterSet must not be empty", "characterSet");

        //            var bytes = new byte[length * 8];
        //            new RNGCryptoServiceProvider().GetBytes(bytes);
        //            var result = new char[length];
        //            for (int i1 = 0; i1 < length; i1++)
        //            {
        //                ulong value = BitConverter.ToUInt64(bytes, i1 * 8);
        //                result[i1] = characterArray[value % (uint)characterArray.Length];
        //            }
        //            UniqueNumbers_Test UOBJ = new UniqueNumbers_Test();
        //            UOBJ.uniqueid = new string(result);
        //            dc.UniqueNumbers_Test.Add(UOBJ);
        //            dc.SaveChanges();
        //            //t = r;
        //       // }
        //       //int t1 = t;
        //    }
        //    catch(Exception ex)
        //    {
        //        t++;
        //    }

        //}

        public static string GetRandomAlphanumericString(int length)
        {
            const string alphanumericCharacters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz" +
                "0123456789";
            return GetRandomString(length, alphanumericCharacters);
        }

        public static string GetRandomString(int length, IEnumerable<char> characterSet)
        {
            //if (length < 0)
            //    throw new ArgumentException("length must not be negative", "length");
            //if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
            //    throw new ArgumentException("length is too big", "length");
            //if (characterSet == null)
            //    throw new ArgumentNullException("characterSet");
            var characterArray = characterSet.Distinct().ToArray();
            //if (characterArray.Length == 0)
            //    throw new ArgumentException("characterSet must not be empty", "characterSet");

            var bytes = new byte[length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }
        public static byte[] GetHashKey(string hashKey)
        {
            // Initialise
            UTF8Encoding encoder = new UTF8Encoding();

            // Get the salt
            string salt = "I am a nice little salt";
            byte[] saltBytes = encoder.GetBytes(salt);

            // Setup the hasher
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(hashKey, saltBytes);

            // Return the key
            return rfc.GetBytes(16);
        }
        public static string Encrypt(byte[] key, string dataToEncrypt)
        {
            // Initialise
            AesManaged encryptor = new AesManaged();

            // Set the key
            encryptor.Key = key;
            encryptor.IV = key;

            // create a memory stream
            using (MemoryStream encryptionStream = new MemoryStream())
            {
                // Create the crypto stream
                using (CryptoStream encrypt = new CryptoStream(encryptionStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    // Encrypt
                    byte[] utfD1 = UTF8Encoding.UTF8.GetBytes(dataToEncrypt);
                    encrypt.Write(utfD1, 0, utfD1.Length);
                    encrypt.FlushFinalBlock();
                    encrypt.Close();

                    // Return the encrypted data
                    return Convert.ToBase64String(encryptionStream.ToArray());
                }
            }
        }
        public static string DecryptQueryString(byte[] key, string encryptedString)
        {
            // Initialise
            AesManaged decryptor = new AesManaged();
            byte[] encryptedData = Convert.FromBase64String(encryptedString);

            // Set the key
            decryptor.Key = key;
            decryptor.IV = key;

            // create a memory stream
            using (MemoryStream decryptionStream = new MemoryStream())
            {
                // Create the crypto stream
                using (CryptoStream decrypt = new CryptoStream(decryptionStream, decryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    // Encrypt
                    decrypt.Write(encryptedData, 0, encryptedData.Length);
                    decrypt.Flush();
                    decrypt.Close();

                    // Return the unencrypted data
                    byte[] decryptedData = decryptionStream.ToArray();
                    return UTF8Encoding.UTF8.GetString(decryptedData, 0, decryptedData.Length);
                }
            }
        }
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ToString();
            }
        }

        public static int CurrentUserId
        {
            get
            {

                return Convert.ToInt32(HttpContext.Current.Session["userdata"].ToString().Split('^')[0]);

            }
        }
        public static string CurrentUseremail
        {
            get
            {

                return HttpContext.Current.Session["userdata"].ToString().Split('^')[1];

            }

        }
        public static string CurrentUserName
        {
            get
            {

                return HttpContext.Current.Session["userdata"].ToString().Split('^')[2];

            }

        }
        public static string CurrentUserRole
        {
            get
            {

                return HttpContext.Current.Session["userdata"].ToString().Split('^')[3].ToLower();

            }
        }
        public static bool CurrentUserActiveStatus
        {
            get
            {

                return Convert.ToBoolean(HttpContext.Current.Session["userdata"].ToString().Split('^')[4]);

            }
        }

        //internal static string Decrypt(byte[] key, string encryptedString)
        //{
        //    // Initialise
        //    AesManaged decryptor = new AesManaged();
        //    byte[] encryptedData = Convert.FromBase64String(encryptedString);

        //    // Set the key
        //    decryptor.Key = key;
        //    decryptor.IV = key;

        //    // create a memory stream
        //    using (MemoryStream decryptionStream = new MemoryStream())
        //    {
        //        // Create the crypto stream
        //        using (CryptoStream decrypt = new CryptoStream(decryptionStream, decryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //        {
        //            // Encrypt
        //            decrypt.Write(encryptedData, 0, encryptedData.Length);
        //            decrypt.Flush();
        //            decrypt.Close();

        //            // Return the unencrypted data
        //            byte[] decryptedData = decryptionStream.ToArray();
        //            return UTF8Encoding.UTF8.GetString(decryptedData, 0, decryptedData.Length);
        //        }
        //    }
        //}

       
    }
    public enum UserRole
    {
        Admin,
        client

    }
}