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

## Numbers

## Object Copier

## Strings
