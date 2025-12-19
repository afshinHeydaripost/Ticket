using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Security.Claims;


namespace Helper;
public static class Tools
{

    public static string GetRemoteIpAddress(this HttpContext item)
    {
        return item.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }
    public static string GetLoginedUserId(this ClaimsPrincipal user)
    {
        var userId = user.FindFirst("UserId")?.Value;
        if (userId == null || string.IsNullOrEmpty(userId))
        {
            return "";
        }
        return userId;
    }
    public static string ToDateTimeFa(DateTime? dateTime = null)
    {
        if (dateTime is null)
            return "";
        PersianCalendar pc = new PersianCalendar();
        return string.Format("{0}/{1}/{2}", pc.GetYear(dateTime.Value), pc.GetMonth(dateTime.Value).ToString().PadLeft(2, '0'), pc.GetDayOfMonth(dateTime.Value).ToString().PadLeft(2, '0'));
    }
}


