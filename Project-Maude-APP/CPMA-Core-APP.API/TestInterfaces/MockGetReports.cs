using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CPMA_Core_APP.API.TestInterfaces

{
    public class MockGetReports : IGetReports
    {
        public async Task<Report[]> getReport()
        {

            var list = new List<Report>
                {
                    new Report()
                    {
                        ReportId = 1,
                        ReportTitle = "Listy",
                        Barcode="010101",
                        ReportScore= 100,
                        ReportImageUrl="lalalalalalalalala"
                    },
                    new Report()
                    {
                       ReportId = 2,
                        ReportTitle = "this is report 2",
                        Barcode="010102",
                        ReportScore= 120,
                        ReportImageUrl="banananananana"
                    },
                    new Report()
                    {
                        ReportId = 3,
                        ReportTitle = "TINCAN",
                        Barcode="0",
                        ReportScore= 10000,
                        ReportImageUrl="canyoufindme"
                    }
                };


                return await Task.Run(() =>
                {
                    return list.ToArray();
                });
        }

    }
}
