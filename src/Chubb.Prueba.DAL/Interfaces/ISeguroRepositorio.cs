using Chubb.Prueba.Entities.Entities;
using Chubb.Prueba.Entities.Utilitarios;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chubb.Prueba.BL.Interfaces
{
    public interface ISeguroRepositorio
    {
        Task<Result> GetSeguro(Expression<Func<Seguro, bool>> query);
        Task<Result> PostSeguro(Seguro seguro);
        Task<Result> UpdateSeguro(Seguro seguro);
        Task<Result> DeleteSeguro(SqlParameter parameter);
    }
}
