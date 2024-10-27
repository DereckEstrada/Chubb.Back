using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chubb.Prueba.Const
{
    public static class HttpResponseMessages
    {
        public const string OK = "Request has succeeded.";
        public const string Created = "Resource has been successfully created.";
        public const string NoContent = "The server successfully processed the request, but is not returning any content.";
        public const string BadRequest = "The server cannot or will not process the request due to a client error.";        
    }
}
