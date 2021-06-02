using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Helpers
{
    public static class StringHelpers
    {
        public static bool IsContainText(this string text, string searchText, bool isIgnoreCase = true)
        {
            try
            {
                return text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;
            }
            catch
            {
                return true;
            }
        }
    }
}
