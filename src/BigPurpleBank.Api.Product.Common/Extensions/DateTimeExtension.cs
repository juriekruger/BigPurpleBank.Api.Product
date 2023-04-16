namespace BigPurpleBank.Api.Product.Common.Extensions;

public static class DateTimeExtension
{
    public static int ToUnixTime(this DateTime dateTime)
    {
        return (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }
    
    public static DateTime FromUnixTime(this int unixTime)
    {
        return new DateTime(1970, 1, 1).AddSeconds(unixTime);
    }
}