using System.Globalization;

namespace Nucleus.Utilities;

public static class DataExtentions
{
    public static bool IsValidBase64String(this string key)
    {
        try
        {
            Convert.FromBase64String(key);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool IsZero(this object value)
    {
        try
        {
            Convert.ToInt32(value, CultureInfo.InvariantCulture);

            return false;
        }
        catch (System.Exception)
        {
            return false;
        }
    }
}
