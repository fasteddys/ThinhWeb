using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Helper
{
    public static class PagingHelper
    {
        public static int Skip(int currentPage, int recordPerPage)
        {
            return currentPage >= 1 ? recordPerPage * (currentPage - 1) : 0;
        }
    }
}
