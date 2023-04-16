namespace BigPurpleBank.Api.Product.Common.Extensions;

public static class DateTimeExtension
{
    /// <summary>
    /// Total seconds since 1970-01-01 00:00:00
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static int ToUnixTime(
        this DateTime dateTime) => (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

    /// <summary>
    /// DateTime from Unix time
    /// </summary>
    /// <param name="unixTime"></param>
    /// <returns></returns>
    public static DateTime FromUnixTime(
        this int unixTime) => new DateTime(1970, 1, 1).AddSeconds(unixTime);
}