using Chubb.Prueba.BL.Interfaces;
using Chubb.Prueba.Const;
using Chubb.Prueba.Entities.Entities;
using Chubb.Prueba.Entities.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml;
using System.Linq.Expressions;
using System.Net;


namespace Chubb.Prueba.BL.Services
{
    public class AseguradoServices :ErrorMethod, IAseguradoServices
    {
        private readonly IAseguradoRepositorio _repositorio;
        public AseguradoServices(IAseguradoRepositorio repositorio)
        {
            this._repositorio = repositorio;
        }

        public async Task<Result> DeleteAsegurado(Asegurado asegurado)
        {
            var result = new Result();
            try
            {
                var parametros = new SqlParameter(Const.Procedures.VariableId, asegurado.IdAsegurado);
                result = await _repositorio.DeleteAsegurado(parametros);
            }
            catch (Exception ex)
            {
                result = this.ErrorResult(ex.Message);
            }
            return result;
        }

        public async Task<Result> GetAsegurado(QueryParameters parametros)
        {
            var result = new Result();
            Expression<Func<Asegurado, bool>> query = asegurado => false;
            try
            {
                switch (parametros.SearchOption)
                {
                    case Const.SearchOption.All:
                        query = asegurado => asegurado.IdEstado == 1;
                        break;
                    case Const.SearchOption.CedulaParcial:
                        query = asegurado => asegurado.CedulaAsegurado.Contains(Convert.ToString(parametros.data))
                        && asegurado.IdEstado == StatusVariables.EstadoActivo;
                        break;
                    case Const.SearchOption.CedulaCompleta:
                        query = asegurado => asegurado.CedulaAsegurado.Equals(Convert.ToString(parametros.data))
                        && asegurado.IdEstado == StatusVariables.EstadoActivo;
                        break;
                    case Const.SearchOption.Fecha:
                        query = asegurado => asegurado.FechaRegistro.Equals(Convert.ToDateTime(parametros.data))
                        && asegurado.IdEstado == StatusVariables.EstadoActivo;
                        break;
                    default:
                        throw new Exception($"Opcion: {parametros.SearchOption} no es valida");
                }
                result = await _repositorio.GetAsegurado(query);
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
                if (await this.VerificarCedula(asegurado.CedulaAsegurado))
                {
                    throw new Exception($"La cedula: {asegurado.CedulaAsegurado} ya se encuentra registrada");
                }
                asegurado.IdEstado = StatusVariables.EstadoActivo;
                asegurado.FechaRegistro = DateOnly.FromDateTime(DateTime.Today);
                if (asegurado.AseguradoSeguros.Count > 0)
                {
                    foreach(var relation in asegurado.AseguradoSeguros)
                    {
                        relation.FechaRegistro = DateOnly.FromDateTime(DateTime.Today);
                    }
                }
                result = await _repositorio.PostAsegurado(asegurado);
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
                result = await _repositorio.UpdateAsegurado(asegurado);
            }
            catch (Exception ex)
            {
                result = ErrorResult(ex.Message);
            }
            return result;
        }
        public async Task<Result> UploadFile(IFormFile file)
        {
            var result = new Result();
            var aseguradoList = new List<Asegurado>();
            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];

                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                        {
                            var asegurado = new Asegurado();
                            if (worksheet.Cells[row, 1].Value != null)
                            {
                                asegurado.CedulaAsegurado = worksheet.Cells[row, 1].Value?.ToString();
                                asegurado.NombreAsegurado = worksheet.Cells[row, 2].Value?.ToString();
                                asegurado.ApellidoAsegurado = worksheet.Cells[row, 3].Value?.ToString();
                                asegurado.TelefonoAsegurado = worksheet.Cells[row, 4].Value?.ToString();
                                asegurado.EdadAsegurado = int.Parse(worksheet.Cells[row, 5].Value?.ToString());
                                var verificate = await this.VerificarCedula(asegurado.CedulaAsegurado);
                                if (verificate)
                                {
                                    throw new Exception("Cedula: " + asegurado.CedulaAsegurado + " ya existente");
                                }

                                aseguradoList.Add(asegurado);
                            }
                        }
                        foreach (var asegurado in aseguradoList)
                        {
                            result=await this.PostAsegurado(asegurado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = ErrorResult(ex.Message);
            }
            return result;
        }
        private async Task<bool> VerificarCedula(string cedula)
        {
            var existAsegurado=false;
            var parametros = new QueryParameters();
            try
            {
                parametros.SearchOption= Const.SearchOption.CedulaCompleta;
                parametros.data= cedula;    
                var result=await this.GetAsegurado(parametros);
                if (result.Code == HttpResponseStatusCodes.Ok)
                {
                    existAsegurado = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return existAsegurado;
        }        
    }
}
