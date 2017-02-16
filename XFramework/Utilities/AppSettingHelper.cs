using System;
using System.Configuration;
using System.Globalization;

namespace XFramework.Utilities
{
    //public interface IAppSettingHelper
    //{
    //    T Get<T>(string key);
    //    T Get<T>(string key, Func<T> defaultFunc);
    //}

    public class AppSettingHelper
    {
        public static T Get<T>(string key)
        {
            return Get<T>(key, () => default(T));
        }

        public static T Get<T>(string key, T defaultVal)
        {
            var result = default(T);

            try
            {
                result = Get<T>(key, () => default(T));
            }
            catch (Exception)
            {
                return defaultVal;
            }
            finally
            {
                if (result == null)
                    result = defaultVal;

                if (!result.Equals(defaultVal))
                    result = defaultVal;
            }

            return result;
        }

        public static T Get<T>(string key, Func<T> defaultFunc)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value))
                return defaultFunc();
            return (T) Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
    }
}