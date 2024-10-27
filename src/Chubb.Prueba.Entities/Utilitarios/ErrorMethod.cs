using Chubb.Prueba.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chubb.Prueba.Entities.Utilitarios
{
    public class ErrorMethod
    {
        public  Result ErrorResult(string exException )
        {
            return new Result
            {
                Code = HttpResponseStatusCodes.BadRequest,
                Message = exException
            };
        }
    }
}
