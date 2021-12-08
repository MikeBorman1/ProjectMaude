using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CPMA_Core_APP.API
{
    public interface ISetProductMaterialLink
    {
        Task SetLink(string productName, string productPhototId, string barcode, string materials);
    }
}
