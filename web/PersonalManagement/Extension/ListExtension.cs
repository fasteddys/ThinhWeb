using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Extension
{
    public static class ListExtension
    {
        public static List<T> ReverseList<T>(this List<T> input)
        {
            input.Reverse();
            return input;
        }
    }
}
