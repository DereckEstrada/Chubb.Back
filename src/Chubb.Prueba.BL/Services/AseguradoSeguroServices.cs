using Chubb.Prueba.BL.Interfaces;
using Chubb.Prueba.Const;
using Chubb.Prueba.Entities.Entities;
using Chubb.Prueba.Entities.Utilitarios;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chubb.Prueba.BL.Services
{
    public class AseguradoSeguroServices : ErrorMethod, IAseguradoSeguroServices
    {
        private readonly IAseguradoSeguroRepositorio _repositorio;
        public AseguradoSeguroServices(IAseguradoSeguroRepositorio repositorio)
        {
            this._repositorio = repositorio;
        }

        public async Task<Result> DeleteAseguradoSeguro(AseguradoSeguro relation)
        {
            var result=new Result();
            try
            {
                var parametros = new SqlParameter(Const.Procedures.VariableId, relation.IdAseguradoSeguro);                
                result=await _repositorio.DeleteAseguradoSeguro(parametros);
            }
            catch (Exception ex)
            {
                result = this.ErrorResult(ex.Message);
            }
            return result;
        }

        public async Task<Result> GetAseguradoSeguro(QueryParameters parametros)
        {
            var result = new Result();
            Expression<Func<AseguradoSeguro, bool>> query = relation => false;
            try
            {
                switch (parametros.SearchOption)
                {
                    case Const.SearchOption.All:
                        query = relation => relation.IdEstado == 1;
                        break;
                    case Const.SearchOption.CedulaCompleta:

                        query = relation => relation.IdAseguradoNavigation.CedulaAsegurado.Equals(Convert.ToString(parametros.data)) 
                        && relation.IdEstado == StatusVariables.EstadoActivo 
                        && relation.IdAseguradoNavigation.IdEstado== StatusVariables.EstadoActivo;
                        break;
                    case Const.SearchOption.CodigoCompleto:
                        query = relation => relation.IdSeguroNavigation.CodigoSeguro.Equals(Convert.ToString(parametros.data)) 
                        && relation.IdEstado ==StatusVariables.EstadoActivo 
                        && relation.IdSeguroNavigation.IdEstado==StatusVariables.EstadoActivo;
                        break;
                    default:
                        throw new Exception($"Opcion: {parametros.SearchOption} no es valida");
                        

                }
                result = await _repositorio.GetAseguradoSeguro(query);
            }
            catch (Exception ex)
            {
                result = ErrorResult(ex.Message);
            }
            return result;
        }

        public async Task<Result> PostAseguradoSeguro(AseguradoSeguro relation)
        {
            var result = new Result();
            try
            {
                relation.IdEstado = StatusVariables.EstadoActivo;
                relation.FechaRegistro = DateOnly.FromDateTime(DateTime.Today);
                result = await _repositorio.PostAseguradoSeguro(relation);
            }
            catch (Exception ex)
            {
                result = ErrorResult(ex.Message);
            }
            return result;
        }

        public async Task<Result> UpdateAseguradoSeguro(AseguradoSeguro relation)
        {
            var result = new Result();
            try
            {
                result = await _repositorio.UpdateAseguradoSeguro(relation);
            }
            catch (Exception ex)
            {
                result = ErrorResult(ex.Message);
            }
            return result;
        }
    }
}
