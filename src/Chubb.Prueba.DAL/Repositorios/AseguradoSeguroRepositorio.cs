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
    public class AseguradoSeguroRepositorio:ErrorMethod, IAseguradoSeguroRepositorio
    {
        private readonly ChubbDbContext _context;
        private DynamicValidator dynamicValidator = new DynamicValidator();
        public AseguradoSeguroRepositorio(ChubbDbContext context)
        {
            this._context = context;
        }

        public async Task<Result> DeleteAseguradoSeguro(SqlParameter parameter)
        {
            var result = new Result();
            try
            {
                await _context.Database.ExecuteSqlRawAsync(Const.Procedures.DeleteAseguradoSeguro+ " " + Const.Procedures.VariableId, parameter);
                result.Code = HttpResponseStatusCodes.Created;
                result.Message = HttpResponseMessages.Created;
            }
            catch (Exception ex)
            {
                result = this.ErrorResult(ex.Message);
            }
            return result;
        }

        public async Task<Result> GetAseguradoSeguro(Expression<Func<AseguradoSeguro, bool>> query)
        {
            var result = new Result();
            try
            {
                result.Data = await _context.AseguradoSeguros.Where(query)
                    .Include(relation=>relation.IdSeguroNavigation)
                    .Include(relation=>relation.IdAseguradoNavigation).Select(relation=>new AseguradoSeguroDTO
                    {
                        IdAseguradoSeguro=relation.IdAseguradoSeguro,
                        IdAsegurado=relation.IdAsegurado,
                        NombreAsegurado=relation.IdAseguradoNavigation.NombreAsegurado,
                        ApellidoAsegurado=relation.IdAseguradoNavigation.ApellidoAsegurado,
                        CedulaAsegurado=relation.IdAseguradoNavigation.CedulaAsegurado,
                        IdSeguro=relation.IdSeguro,
                        CodigoSeguro = relation.IdSeguroNavigation.CodigoSeguro,
                        NombreSeguro=relation.IdSeguroNavigation.NombreSeguro,
                        IdEstado=relation.IdEstado,
                        FechaRegistro=relation.FechaRegistro,
                        Prima=relation.IdSeguroNavigation.Prima,
                        SumaAsegurada=relation.IdSeguroNavigation.SumaAsegurada

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

        public async Task<Result> PostAseguradoSeguro(AseguradoSeguro relation)
        {
            var result = new Result();
            try
            {
                _context.AseguradoSeguros.Add(relation);
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

        public async Task<Result> UpdateAseguradoSeguro(AseguradoSeguro relation)
        {
            var result = new Result();
            try
            {
                _context.AseguradoSeguros.Update(relation);
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
