using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace _19T1021302.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Converter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime? DMYStringToDateTime(string s, string format = "dd/MM/yyyy")
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
    }
}