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
    public interface IAseguradoRepositorio
    {
        Task<Result> GetAsegurado(Expression<Func<Asegurado, bool>> query);
        Task<Result> PostAsegurado(Asegurado asegurado);
        Task<Result> UpdateAsegurado(Asegurado asegurado);
        Task<Result> DeleteAsegurado(SqlParameter parameter);

    }
}
