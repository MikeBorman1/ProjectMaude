﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CPMA_Core_APP.Model;
using Newtonsoft.Json.Linq;

namespace CPMA_Core_APP.API.Interfaces
{
    public interface IGetReports
    {
        Task<Report[]> getReport();

    }
}