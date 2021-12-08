using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CPMA_Core_APP.API
{
    public interface IStorage
    {
        Task UploadPhoto(string photoUri, string photoPath);
        Task CopyPhoto(string source, string target);
    }
}