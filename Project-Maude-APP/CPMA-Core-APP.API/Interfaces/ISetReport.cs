using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CPMA_Core_APP.API
{
    public interface ISetReport
    {
        Task SetReport(string ReportImageID, string ReportTitle, string Barcode);
    }
}
