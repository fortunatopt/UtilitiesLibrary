# Utilities Library
This is a collection of utilities that we are using in a project all combined into one class library
## Dates
```csharp
DateTime GetEpoch();

DateTime ToDateTimeFromEpoch(this long intDate);

string ToUnixTimestamp(this DateTime dateTime);

DateTime UnixTimeStampToDateTime(this string unixTimeStampString);

List<DateTime> SortAscending(this List<DateTime> list);

List<DateTime> SortDescending(this List<DateTime> list);

List<DateTime> SortMonthAscending(this List<DateTime> list);

List<DateTime> SortMonthDescending(this List<DateTime> list);

```
## Hmac Crypto
```csharp
byte[] HmacSHA512(String data, byte[] privateKey);

string Signature(String privateKey, String guid, String dateStamp);

string HashedToken(String privateKey, String data);

public class AuthorizeResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}
AuthorizeResponse Validate(String privateKey, string data, String dateStamp, String sig);

string GenerateSignature(String privateKey, string data);

string GetSignatureTime(HttpRequestMessage request)

string GenerateSignatureInput(string httpVerb, string uriBase, string dt, string contentType = null, string qsParams = null);
```
## HTTP
```csharp
string PostJSON(string url, object postObject);

string PostJSONSigned(string url, object postObject, string accessKey, string secretKey, string qString = null);

string GetJSONSigned(string url, string accessKey, string secretKey, string qString = null);

Task<string> GetJSONSignedAsync(string url, string accessKey, string secretKey, string qString = null);

string PostFORMEncoded(string url, string formPostData);

string GetJSON(string url);

string GetJSONWithIp(string url, string ip);

string GetJSONNoContType(string url);

string GetHashedToken(string httpVerb, string uriBase, string contentType, string dt, string qsParams, string body, string secretKey);

string AuthString(bool first, bool unix, string key, string privateKey);

string GetServerIP(this string criteria);
```
## Numbers
```csharp
long[] ToLongArray(this string value, char separator);

int[] ToIntArray(this string value, char separator);

int StringToInt(this string input);
```
## Object Copier
```csharp
T CloneObject<T>(this T objSource);
```
## Strings
```csharp
List<string> ToStringList(this string value, char separator);

string StripCharacter(this string variable, string characters);

ToSnakeCase(this string str);

Left(this string value, int maxLength);
```