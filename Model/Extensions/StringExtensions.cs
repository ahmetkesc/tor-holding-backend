using System.Security.Cryptography;
using System.Text;

namespace Model.Extensions;

public static class StringExtensions
{
    private static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
    }
    public static string EncryptString(this string str, string key)
    {
        var iv = new byte[16];
        byte[] bytes;

        using (var aes = Aes.Create())
        {

            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(str);
            }
            bytes = ms.ToArray();
        }

        var result = Convert.ToBase64String(bytes);
        return result;
    }
    public static string DecryptString(this string cipherText, string key)
    {
        if (cipherText.IsNullOrEmpty()) return string.Empty;

        cipherText = cipherText.Replace('-', '+').Replace('_', '/').Replace(' ', '+')
            .PadRight(4 * ((cipherText.Length + 3) / 4), '=');

        var cipherBytes = Convert.FromBase64String(cipherText);

        using var aes=Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = new byte[16];
        aes.Padding = PaddingMode.PKCS7;
        aes.Mode = CipherMode.CBC;

        using var ms = new MemoryStream(cipherBytes);
        var decryptor = aes.CreateDecryptor(aes.Key,aes.IV);

        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr=new StreamReader(cs,Encoding.UTF8);
        var result = sr.ReadToEnd();

        return result;
    }
}