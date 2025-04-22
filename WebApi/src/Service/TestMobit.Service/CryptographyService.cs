using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestMobit.Domain.Configurations;
using TestMobit.Domain.Interfaces.Services;

namespace TestMobit.Service
{
    public class CryptographyService : ICryptographyService
    {
        private readonly CryptographySettings _cryptographySettings;
        public CryptographyService(CryptographySettings cryptographySettings)
        {
            _cryptographySettings = cryptographySettings;
        }

        public async Task<string> Decrypt(string text)
        {
            text = Decode(text);

            var strVetorInicialBytes = Encoding.ASCII.GetBytes(_cryptographySettings.VectorInit);
            var strChaveValueBytes = Encoding.ASCII.GetBytes(_cryptographySettings.ValueKey);
            byte[] cipherTextBytes1 = Convert.FromBase64String(text ?? string.Empty);
            PasswordDeriveBytes password1 = new PasswordDeriveBytes(_cryptographySettings.StringKey, strChaveValueBytes, _cryptographySettings.HashAlgorithm, _cryptographySettings.PasswordIterations);
            byte[] keyBytes1 = password1.GetBytes(Convert.ToInt32(Math.Round((Convert.ToDouble(_cryptographySettings.KeySize) / 8))));
            RijndaelManaged symmetricKey1 = new RijndaelManaged();
            symmetricKey1.Mode = CipherMode.CBC;
            ICryptoTransform decryptor1 = symmetricKey1.CreateDecryptor(keyBytes1, strVetorInicialBytes);
            MemoryStream memoryStream1 = new MemoryStream(cipherTextBytes1);
            CryptoStream cryptoStream1 = new CryptoStream((Stream)memoryStream1, decryptor1, CryptoStreamMode.Read);
            byte[] strTextoBytes1 = new byte[(cipherTextBytes1.Length + 1)];
            int decryptedByteCount1 = cryptoStream1.Read(strTextoBytes1, 0, strTextoBytes1.Length);
            memoryStream1.Close();
            cryptoStream1.Close();
            return await Task.FromResult(HttpUtility.HtmlDecode(Encoding.UTF8.GetString(strTextoBytes1, 0, decryptedByteCount1)));
        }

        public async Task<string> Encrypt(string text)
        {
            var strVetorInicialBytes = Encoding.ASCII.GetBytes(_cryptographySettings.VectorInit);
            var strChaveValueBytes = Encoding.ASCII.GetBytes(_cryptographySettings.ValueKey);
            var strTextoBytes = Encoding.UTF8.GetBytes(text);
            PasswordDeriveBytes password1 = new PasswordDeriveBytes(_cryptographySettings.StringKey, strChaveValueBytes, _cryptographySettings.HashAlgorithm, _cryptographySettings.PasswordIterations);
            byte[] keyBytes1 = password1.GetBytes(Convert.ToInt32(Math.Round((Convert.ToDouble(_cryptographySettings.KeySize) / 8))));
            RijndaelManaged symmetricKey1 = new RijndaelManaged();
            symmetricKey1.Mode = CipherMode.CBC;
            ICryptoTransform encryptor1 = symmetricKey1.CreateEncryptor(keyBytes1, strVetorInicialBytes);
            MemoryStream memoryStream1 = new MemoryStream();
            CryptoStream cryptoStream1 = new CryptoStream((Stream)memoryStream1, encryptor1, CryptoStreamMode.Write);
            cryptoStream1.Write(strTextoBytes, 0, strTextoBytes.Length);
            cryptoStream1.FlushFinalBlock();
            byte[] cipherTextBytes1 = memoryStream1.ToArray();
            memoryStream1.Close();
            cryptoStream1.Close();
            return await Task.FromResult(Encode(Convert.ToBase64String(cipherTextBytes1)));
        }
        protected string Decode(string m) => Encoding.UTF8.GetString(Convert.FromBase64String(m));

        protected string Encode(string m) => Convert.ToBase64String(Encoding.UTF8.GetBytes(m));
    }
}
