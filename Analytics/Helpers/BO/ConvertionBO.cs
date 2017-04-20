using Analytics.Helpers.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Analytics.Helpers.BO
{
    public class ConvertionBO
    {
        shortenurlEntities dc = new shortenurlEntities();
       // private static readonly char[] BaseChars =
       //"0123456789ABCDEFGHIJKLMOPQRSTUVWXYZabcdefghijklmopqrstuvwxyz-.!~*'_".ToCharArray();
        private static readonly char[] BaseChars =
        "gSYmAQiPeWfq0Ov~rbuyU9dkT3hcsK5IH*pz8Z4o!RxD1BFwXE-JtVj2lG'_aM7.6LC".ToCharArray();
        private static readonly Dictionary<char, int> CharValues = BaseChars
                   .Select((c, i) => new { Char = c, Index = i })
                   .ToDictionary(c => c.Char, c => c.Index);

        //convert number to base64 format
        public string LongToBase(long value)
        {
            try
            {
                long targetBase = BaseChars.Length;
                // Determine exact number of characters to use.
                char[] buffer = new char[Math.Max(
                           (int)Math.Ceiling(Math.Log(value + 1, targetBase)), 1)];

                var i = (long)buffer.Length;
                do
                {
                    buffer[--i] = BaseChars[value % targetBase];
                    value = value / targetBase;
                }
                while (value > 0);
                return new string(buffer);
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
                return "";
            }
        }
        //convert base64 to number
        public  long BaseToLong(string number)
        {
            try
            {
                char[] chrs = number.ToCharArray();
                int m = chrs.Length - 1;
                int n = BaseChars.Length, x;
                long result = 0;
                for (int i = 0; i < chrs.Length; i++)
                {
                    x = CharValues[chrs[i]];
                    result += x * (long)Math.Pow(n, m--);
                }
                return result;
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
                return 0;
            }
        }

        public string GetIP4Address()
        {
            try
            {
                string IP4Address = String.Empty;

                foreach (IPAddress IPA in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
                {
                    if (IPA.AddressFamily.ToString() == "InterNetwork")
                    {
                        IP4Address = IPA.ToString();
                        break;
                    }
                }

                if (IP4Address != String.Empty)
                {
                    return IP4Address;
                }

                foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (IPA.AddressFamily.ToString() == "InterNetwork")
                    {
                        IP4Address = IPA.ToString();
                        break;
                    }
                }

                return IP4Address;
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.InnerException.ToString());
                return "";
            }
        }
    }
}