using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CPMA_Core_APP.API
{
    class JpegContent : StreamContent
    {
        public JpegContent(string filePath)
            : base(new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
        }
    }
}
