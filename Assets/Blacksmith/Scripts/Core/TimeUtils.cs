using System;

namespace Blacksmith
{
    public class TimeUtils
    {
        #region Methods
        public static string GetFormatedTime(string format)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            return DateTime.Now.ToString(format);
        }
        #endregion
    }
}