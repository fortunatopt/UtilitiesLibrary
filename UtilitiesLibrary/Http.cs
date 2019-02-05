using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesLibrary
{
    public static class Http
    {
        public static string PostJSON(string url, object postObject)
        {
            var webAddr = url;
            var request = (HttpWebRequest)WebRequest.Create(webAddr);
            request.ContentType = "application/json; charset=utf-8";
            request.Accept = "application/json; charset=utf-8";
            request.Method = "POST";

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(postObject, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
        public static string PostJSONSigned(string url, object postObject, string accessKey, string secretKey, string qString = null)
        {
            Uri uri = new Uri(url);

            string sigURL = uri.AbsolutePath;
            string contentType = "application/json; charset=utf-8";
            string dt = ((DateTime)DateTime.Now.ToUniversalTime()).ToString("yyyyMMdd'T'HHmmss'Z'");
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(postObject, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            string method = "POST";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = contentType;
            request.Headers["x-esri-date"] = dt;
            request.Headers["authorization"] = accessKey + ":" + GetHashedToken(method, sigURL, contentType, dt, qString, json, secretKey);
            request.Method = method;


            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
        public static string GetJSONSigned(string url, string accessKey, string secretKey, string qString = null)
        {
            Uri uri = new Uri(url);

            string sigURL = uri.AbsolutePath;
            string contentType = String.Empty;
            string dt = ((DateTime)DateTime.Now.ToUniversalTime()).ToString("yyyyMMdd'T'HHmmss'Z'");
            string method = "GET";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = contentType;
            request.Headers["x-esri-date"] = dt;
            request.Headers["authorization"] = accessKey + ":" + GetHashedToken(method, sigURL, contentType, dt, qString, "", secretKey);
            request.Method = method;

            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
        public static async Task<string> GetJSONSignedAsync(string url, string accessKey, string secretKey, string qString = null)
        {
            Uri uri = new Uri(url);

            string sigURL = uri.AbsolutePath;
            string contentType = String.Empty;
            string dt = ((DateTime)DateTime.Now.ToUniversalTime()).ToString("yyyyMMdd'T'HHmmss'Z'");
            string method = "GET";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = contentType;
            request.Headers["x-esri-date"] = dt;
            request.Headers["authorization"] = accessKey + ":" + GetHashedToken(method, sigURL, contentType, dt, qString, "", secretKey);
            request.Method = method;

            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = await streamReader.ReadToEndAsync();
                return result;
            }
        }
        public static string PostFORMEncoded(string url, string formPostData)
        {
            var webAddr = url;
            var request = (HttpWebRequest)WebRequest.Create(webAddr);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(formPostData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
        /// <summary>
        /// Get data service.
        /// </summary>
        /// <param name="url">Service url</param>
        /// <returns>Returns response as a string</returns>
        public static string GetJSON(string url)
        {
            var webAddr = url;
            var request = (HttpWebRequest)WebRequest.Create(webAddr);
            request.Accept = "application/json; charset=utf-8";
            request.Method = "GET";

            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
        /// <summary>
        /// Get data service.
        /// </summary>
        /// <param name="url">Service url</param>
        /// <returns>Returns response as a string</returns>
        public static string GetJSONWithIp(string url, string ip)
        {
            var webAddr = url;
            var request = (HttpWebRequest)WebRequest.Create(webAddr);
            request.Accept = "application/json; charset=utf-8";
            request.Method = "GET";
            request.Headers.Add("True-Client-IP", ip);

            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
        /// <summary>
        /// Get data service.
        /// </summary>
        /// <param name="url">Service url</param>
        /// <returns>Returns response as a string</returns>
        public static string GetJSONNoContType(string url)
        {
            var webAddr = url;
            var request = (HttpWebRequest)WebRequest.Create(webAddr);
            request.Accept = "application/json; charset=utf-8";
            request.Method = "GET";

            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
        public static string GetHashedToken(string httpVerb, string uriBase, string contentType, string dt, string qsParams, string body, string secretKey)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append(httpVerb.ToUpper());
            sb.Append("\n");
            sb.Append(uriBase);
            sb.Append("\n");
            sb.Append(contentType ?? String.Empty);
            sb.Append("\n");
            sb.Append(dt);
            sb.Append("\n");
            sb.Append(qsParams ?? String.Empty);
            sb.Append("\n");
            sb.Append(body ?? String.Empty);

            string signatureInput = sb.ToString();

            return HmacCrypto.HashedToken(secretKey, signatureInput.Replace("\\", ""));

        }
        /// <summary>
        /// Create Auth String
        /// </summary>
        /// <param name="first">true = first item in QueryString, false = not first item</param>
        /// <param name="guid">key to be used with the timestamp for the signature</param>
        /// <param name="privateKey">private key for the signature</param>
        /// <returns>string of Authentication Guid, Timestamp, and Signature</returns>
        public static string AuthString(bool first, bool unix, string key, string privateKey)
        {

            string ts = null;
            if (unix == true)
            {
                ts = DateTime.Now.ToUnixTimestamp();
            }
            else
            {
                ts = DateTime.Now.ToUniversalTime().ToString();
            }

            //String privateKey, String guid, String dateStamp

            string qs = first == true ? "?" : "&";
            string urlAuthKeyTS = qs + "key=" + key + "&timestamp=" + ts;
            string urlAuth = urlAuthKeyTS + "&signature=" + HmacCrypto.Signature(privateKey, key, ts);

            return urlAuth;

        }
        public static string GetServerIP(this string criteria)
        {

            string host = Dns.GetHostName();
            string ip = null;

            for (int i = 0; i <= Dns.GetHostEntry(host).AddressList.Length - 1; i++)
            {
                if (criteria == null)
                {
                    ip = Dns.GetHostEntry(host).AddressList[i].MapToIPv4().ToString();
                }
                else
                {

                    if (Dns.GetHostEntry(host).AddressList[i].MapToIPv4().ToString().StartsWith(criteria) == true)
                        return Dns.GetHostEntry(host).AddressList[i].MapToIPv4().ToString();
                }

            }
            return ip;

        }
    }
}
