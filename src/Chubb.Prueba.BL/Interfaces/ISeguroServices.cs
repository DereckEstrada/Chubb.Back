using Chubb.Prueba.Entities.Entities;
using Chubb.Prueba.Entities.Utilitarios;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chubb.Prueba.BL.Interfaces
{
    public interface ISeguroServices
    {
        Task<Result> GetSeguro(QueryParameters parametros);
        Task<Result> PostSeguro(Seguro seguro);
        Task<Result> UpdateSeguro(Seguro seguro);
        Task<Result> DeleteSeguro(Seguro seguro);

    }
}
