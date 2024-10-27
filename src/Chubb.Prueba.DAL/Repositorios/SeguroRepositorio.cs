using Chubb.Prueba.BL.Interfaces;
using Chubb.Prueba.Const;
using Chubb.Prueba.DTOs;
using Chubb.Prueba.Entities.Entities;
using Chubb.Prueba.Entities.Utilitarios;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chubb.Prueba.DAL.Repositorios
{
    public class SeguroRepositorio : ErrorMethod, ISeguroRepositorio
    {
        private readonly ChubbDbContext _context;
        private DynamicValidator dynamicValidator = new DynamicValidator();
        public SeguroRepositorio(ChubbDbContext context)
        {
            this._context = context;
        }

        public async Task<Result> DeleteSeguro(SqlParameter parameter)
        {
            var result = new Result();
            try
            {
                await _context.Database.ExecuteSqlRawAsync(Const.Procedures.DeleteSeguro+ " " + Const.Procedures.VariableId, parameter);
                result.Code = HttpResponseStatusCodes.Created;
                result.Message = HttpResponseMessages.Created;
            }
            catch (Exception ex)
            {
                result = this.ErrorResult(ex.Message);
            }
            return result;
        }

        public async Task<Result> GetSeguro(Expression<Func<Seguro, bool>> query)
        {
            var result = new Result();
            try
            {
                result.Data = await _context.Seguros.Where(query).Select(seguro=>new SeguroDTO
                {
                    IdSeguro=seguro.IdSeguro,
                    NombreSeguro=seguro.NombreSeguro,
                    CodigoSeguro=seguro.CodigoSeguro,
                    SumaAsegurada=seguro.SumaAsegurada,
                    Prima=seguro.Prima,
                    IdEstado=seguro.IdEstado
                }).ToListAsync();
                result.Code = dynamicValidator.IsDynamicEmpty(result.Data) ?
                    HttpResponseStatusCodes.NoContent : HttpResponseStatusCodes.Ok;
                result.Message = dynamicValidator.IsDynamicEmpty(result.Data) ?
                    HttpResponseMessages.NoContent : HttpResponseMessages.OK;
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
                _context.Seguros.Add(seguro);
                await _context.SaveChangesAsync();
                result.Code = HttpResponseStatusCodes.Created;
                result.Message = HttpResponseMessages.Created;
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
                _context.Seguros.Update(seguro);
                await _context.SaveChangesAsync();
                result.Code = HttpResponseStatusCodes.Created;
                result.Message = HttpResponseMessages.Created;
            }
            catch (Exception ex)
            {
                result = ErrorResult(ex.Message);
            }
            return result;
        }
    }
}
