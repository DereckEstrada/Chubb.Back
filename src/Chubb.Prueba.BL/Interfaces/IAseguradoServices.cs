using Chubb.Prueba.Entities.Entities;
using Chubb.Prueba.Entities.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chubb.Prueba.BL.Interfaces
{
    public interface IAseguradoServices
    {
        Task<Result> GetAsegurado(QueryParameters parametros);
        Task<Result> PostAsegurado(Asegurado asegurado);
        Task<Result> UpdateAsegurado(Asegurado asegurado);
        Task<Result> DeleteAsegurado(Asegurado asegurado);
        Task<Result>UploadFile(IFormFile file);

    }
}
