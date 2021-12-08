using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CPMA_Core_APP.API.TestInterfaces

{
    public class MockSetReport : ISetReport
    {
        public async Task SetReport(string ReportImageID, string ReportTitle, string Barcode)
        {
            return;
        }

    }
}
