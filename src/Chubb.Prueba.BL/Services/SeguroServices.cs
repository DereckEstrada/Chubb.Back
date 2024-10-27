using Chubb.Prueba.BL.Interfaces;
using Chubb.Prueba.Const;
using Chubb.Prueba.Entities.Entities;
using Chubb.Prueba.Entities.Utilitarios;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Text.Json.Nodes;


namespace Chubb.Prueba.BL.Services
{
    public class SeguroServices:ErrorMethod,ISeguroServices
    {
        private readonly ISeguroRepositorio _repositorio;
        public SeguroServices(ISeguroRepositorio repositorio)
        {
            this._repositorio = repositorio;    
        }

        public async Task<Result> DeleteSeguro(Seguro seguro)
        {
            var result = new Result();
            try
            {
                var parametros = new SqlParameter(Const.Procedures.VariableId, seguro.IdSeguro);
                result = await _repositorio.DeleteSeguro(parametros);
            }
            catch (Exception ex)
            {
                result = this.ErrorResult(ex.Message);
            }
            return result;
        }

        public async Task<Result> GetSeguro(QueryParameters parametros)
        {
            var result = new Result();
            Expression<Func<Seguro, bool>> query = seguro => false;
            try
            {
                switch (parametros.SearchOption)
                {
                    case Const.SearchOption.All:
                        query = seguro => seguro.IdEstado == 1;
                        break;
                    case Const.SearchOption.CodigoParcial:
                        query = seguro => seguro.CodigoSeguro.Contains(Convert.ToString(parametros.data))
                        && seguro.IdEstado == StatusVariables.EstadoActivo;
                        break;
                    case Const.SearchOption.CodigoCompleto:
                        query = seguro => seguro.CodigoSeguro.Equals(Convert.ToString(parametros.data))
                        && seguro.IdEstado == StatusVariables.EstadoActivo;
                        break;
                    default:
                        throw new Exception($"Opcion: {parametros.SearchOption} no es valida");

                }
                result = await _repositorio.GetSeguro(query);
            }
            catch (Exception ex)
            {
                result = ErrorResult(ex.Message);
            }
            return result;
        }

        public async Task<Result> PostSeguro(Seguro seguro)
        {
            var result = new Result();
            try
            {
                if(await this.ValidatorCodigo(seguro.CodigoSeguro)){
                    throw new Exception($"EL codigo {seguro.CodigoSeguro} ya se encuentra registrado");
                }
                seguro.IdEstado = StatusVariables.EstadoActivo;
                result = await _repositorio.PostSeguro(seguro);
            }
            catch (Exception ex)
            {
                result = ErrorResult(ex.Message);
            }
            return result;
        }

        public async Task<Result> UpdateSeguro(Seguro seguro)
        {
            var result = new Result();
            try
            {
                result = await _repositorio.UpdateSeguro(seguro);
            }
            catch (Exception ex)
            {
                result = ErrorResult(ex.Message);
            }
            return result;
        }
        private async Task<bool> ValidatorCodigo(string codigo)
        {
            var validator = false;
            var query = new QueryParameters();
            try
            {
                query.SearchOption = Const.SearchOption.CodigoCompleto;
                query.data = codigo;
                var result = await this.GetSeguro(query);
                if (result.Code == HttpResponseStatusCodes.Ok)
                {
                    validator =true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return validator;
        }
    }
}
