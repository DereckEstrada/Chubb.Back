﻿using Chubb.Prueba.Entities.Entities;
using Chubb.Prueba.Entities.Utilitarios;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chubb.Prueba.BL.Interfaces
{
    public interface IAseguradoSeguroServices
    {
        Task<Result> GetAseguradoSeguro(QueryParameters parametros);
        Task<Result> PostAseguradoSeguro(AseguradoSeguro relation);
        Task<Result> UpdateAseguradoSeguro(AseguradoSeguro relation);
        Task<Result> DeleteAseguradoSeguro(AseguradoSeguro relation);
    }
}