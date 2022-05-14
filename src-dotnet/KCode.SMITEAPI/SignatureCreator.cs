using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KCode.SMITEAPI
{
    internal class SignatureCreator
    {
        public static string Create(int devId, string methodName, string authKey, string timestamp) => Hash(CreateString(devId, methodName, authKey, timestamp));
        internal static string CreateString(int devId, string methodName, string authKey, string timestamp) => $"{devId}{methodName}{authKey}{timestamp}";
        internal static string Hash(string v) => ToHexString(HashMd5(Decode(v)));
        private static byte[] Decode(string value) => Encoding.UTF8.GetBytes(value);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA5351:Do Not Use Broken Cryptographic Algorithms", Justification = "Required by upstream")]
        private static byte[] HashMd5(byte[] value) { using var md5 = MD5.Create(); return md5.ComputeHash(value); }
        private static string ToHexString(byte[] value) => string.Join("", value.Select(x => x.ToString("x2", provider: null)));
    }
}
