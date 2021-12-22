using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Areas.API
{
    public enum ErrorCodeEnum
    {
        [Description("00000000")]
        OK = 0,
        [Description("10000000")]
        INCORRECT_PARAM = 1,
    }

    public static class ErrorCodeExtensions
    {
        public static string ToDescriptionString(this ErrorCodeEnum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
