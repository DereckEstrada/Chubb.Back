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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chubb.Prueba.DAL.Repositorios
{
    public  class AseguradoRepositorio: ErrorMethod, IAseguradoRepositorio
    {
        private readonly ChubbDbContext _context;
        private DynamicValidator dynamicValidator=new DynamicValidator();
        public AseguradoRepositorio(ChubbDbContext context)
        {
            this._context = context;    
        }

        public async Task<Result> DeleteAsegurado(SqlParameter parameter)
        {
            var result = new Result();
            try
            {
                 await _context.Database.ExecuteSqlRawAsync(Const.Procedures.DeleteAsegurado + " " + Const.Procedures.VariableId, parameter);
                result.Code = HttpResponseStatusCodes.Created;
                result.Message = HttpResponseMessages.Created;
            }
            catch (Exception ex)
            {
                result=this.ErrorResult(ex.Message);
            }
            return result;
        }
 

        public async Task<Result> GetAsegurado(Expression<Func<Asegurado, bool>> query)
        {
            var result = new Result();
            try
            {
                result.Data = await _context.Asegurados.Where(query).Include(asegurado=>asegurado.AseguradoSeguros).Select(asegurado=>new AseguradoDTO
                {
                    IdAsegurado=asegurado.IdAsegurado,
                    CedulaAsegurado=asegurado.CedulaAsegurado,
                    NombreAsegurado=asegurado.NombreAsegurado,
                    ApellidoAsegurado=asegurado.ApellidoAsegurado,
                    EdadAsegurado=asegurado.EdadAsegurado,
                    TelefonoAsegurado=asegurado.TelefonoAsegurado,
                    FechaRegistro=asegurado.FechaRegistro,
                    IdEstado=asegurado.IdEstado,
                    AseguradoSeguros=asegurado.AseguradoSeguros
                    
                }).ToListAsync();
                result.Code=dynamicValidator.IsDynamicEmpty(result.Data)?
                    HttpResponseStatusCodes.NoContent: HttpResponseStatusCodes.Ok;
                result.Message = dynamicValidator.IsDynamicEmpty(result.Data) ? 
                    HttpResponseMessages.NoContent : HttpResponseMessages.OK;
            }
            catch (Exception ex)
            {
                result = ErrorResult(ex.Message);
            }
            return result;
        }

        public async Task<Result> PostAsegurado(Asegurado asegurado)
        {
            var result = new Result();
            try
            {
                _context.Asegurados.Add(asegurado);
                await _context.SaveChangesAsync();  
                result.Code = HttpResponseStatusCodes.Created;
                result.Message= HttpResponseMessages.Created;
            }
            catch (Exception ex)
            {
                result = ErrorResult(ex.Message);
            }
            return result;
        }

        public async Task<Result> UpdateAsegurado(Asegurado asegurado)
        {
            var result = new Result();
            try
            {
                _context.Asegurados.Update(asegurado);
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
