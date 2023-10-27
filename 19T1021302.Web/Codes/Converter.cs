using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using _19T1021302.DomainModels;

namespace _19T1021302.Web
{
    public static class Converter
    {
        public static DateTime? DMYStringToDateTime(string s, string format = "d/M/yyyy")
        {
            try
            {
                return DateTime.ParseExact(s, format, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }
        public static UserAccount CookieToUserAccount(string value)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<UserAccount>(value);
            }
            catch
            {
                return null;
            }
        }
    }
}