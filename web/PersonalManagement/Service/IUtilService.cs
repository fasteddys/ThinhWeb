﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Service
{
    public interface IUtilService
    {
        List<SelectListItem> GetListTags();
    }
}
