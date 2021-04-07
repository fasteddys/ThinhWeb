using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public enum eTaskStatus
    {
        NOT_START_YET = 0,
        IN_PROGRESS = 1,
        RESOLVE = 2,
        RE_OPEN = 3,
        WAIT_CONFIRM = 4,
        WAIT_INFOR = 5,
        PENDING = 6
    }

    public enum eStatus
    {
        DISABLE = 0,
        ENABLE = 1,
    }
}
