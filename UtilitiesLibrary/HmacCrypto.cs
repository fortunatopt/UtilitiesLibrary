using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace UtilitiesLibrary
{
    public static class HmacCrypto
    {
        static byte[] HmacSHA512(String data, byte[] privateKey)
        {
            String algorithm = "HmacSHA512";
            KeyedHashAlgorithm kha = KeyedHashAlgorithm.Create(algorithm);
            kha.Key = privateKey;

            return kha.ComputeHash(Encoding.UTF8.GetBytes(data));
        }
        public static string Signature(String privateKey, String guid, String dateStamp)
        {
            string shortenedDate;
            if (dateStamp.Length > 9)
            {
                shortenedDate = dateStamp.Substring(0, 9);
            }
            else
            {
                shortenedDate = dateStamp;
            }

            byte[] pkBytes = Encoding.UTF8.GetBytes((privateKey).ToCharArray());
            byte[] hashDate = HmacSHA512(shortenedDate, pkBytes);
            byte[] hashGuid = HmacSHA512(guid, hashDate);

            return Convert.ToBase64String(hashGuid);
        }
        public static string HashedToken(String privateKey, String data)
        {
            byte[] pkBytes = Encoding.UTF8.GetBytes((privateKey).ToCharArray());
            byte[] hashDate = HmacSHA512(data, pkBytes);

            return Convert.ToBase64String(hashDate);
        }
        public static AuthorizeResponse Validate(String privateKey, string data, String dateStamp, String sig)
        {
            byte[] pkBytes = Encoding.UTF8.GetBytes((privateKey).ToCharArray());
            byte[] hashData = HmacSHA512(data, pkBytes);

            var signature = Convert.ToBase64String(hashData);

            if (sig == signature)
                return new AuthorizeResponse { Message = "Success", StatusCode = HttpStatusCode.OK };

            return new AuthorizeResponse { Message = $"Signature error: {data} - {dateStamp}", StatusCode = HttpStatusCode.Unauthorized };
        }
        public static string GenerateSignature(String privateKey, string data)
        {
            byte[] pkBytes = Encoding.UTF8.GetBytes((privateKey).ToCharArray());
            byte[] hashData = HmacSHA512(data, pkBytes);

            var signature = Convert.ToBase64String(hashData);
            return signature;
        }
        public static string GetSignatureTime(HttpRequestMessage request)
        {
            string val = null;

            try
            {
                IEnumerable<string> vals = request.Headers.GetValues("Signature-Time");
                if (vals.Count() > 0)
                    val = vals.FirstOrDefault();
            }
            catch { }

            return val;

        }
        public static string GenerateSignatureInput(string httpVerb, string uriBase, string dt, string contentType = null, string qsParams = null)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(httpVerb.ToUpper());
            sb.Append("\n");
            sb.Append(uriBase);
            sb.Append("\n");
            sb.Append(contentType == null ? String.Empty : contentType);
            sb.Append("\n");
            sb.Append(dt);
            sb.Append("\n");
            sb.Append(qsParams == null ? String.Empty : qsParams);

            string signatureInput = sb.ToString();

            return signatureInput;
        }
        public class AuthorizeResponse
        {
            public HttpStatusCode StatusCode { get; set; }
            public string Message { get; set; }
        }
    }
}
