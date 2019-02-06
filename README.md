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

## HTTP

## Numbers

## Object Copier

## Strings
