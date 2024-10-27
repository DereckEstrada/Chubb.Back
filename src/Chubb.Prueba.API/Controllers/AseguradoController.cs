using Chubb.Prueba.BL.Interfaces;
using Chubb.Prueba.Const;
using Chubb.Prueba.Entities.Entities;
using Chubb.Prueba.Entities.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Chubb.Prueba.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AseguradoController : ControllerBase
    {
        private readonly IAseguradoServices _services;
        public AseguradoController(IAseguradoServices services)
        {
            this._services = services;
        }
        [HttpPost("GetAsegurado")]
        public async Task<Result> GetAsegurado([FromBody]QueryParameters parametros)
        {
            var result = new Result();
            try
            {
                result = await _services.GetAsegurado(parametros);
            }
            catch (Exception ex)
            {
                result.Code = HttpResponseStatusCodes.BadRequest;
                result.Message = ex.Message;
            }
            return result;
;        }
        [HttpPost("PostAsegurado")]
        public async Task<Result> PostAsegurado([FromBody]Asegurado asegurado)
        {
            var result = new Result();
            try
            {
                result = await _services.PostAsegurado(asegurado);
            }
            catch (Exception ex)
            {
                result.Code = HttpResponseStatusCodes.BadRequest;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost("UpdateAsegurado")]
        public async Task<Result> UpdateAsegurado([FromBody]Asegurado asegurado)
        {
            var result = new Result();
            try
            {
                result = await _services.UpdateAsegurado(asegurado);
            }
            catch (Exception ex)
            {
                result.Code = HttpResponseStatusCodes.BadRequest;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost("UploadFile")]
        public async Task<Result> UploadFile([FromForm] IFormFile file)
        {
            var result = new Result();
            try
            {
                result = await _services.UploadFile(file);
            }
            catch (Exception ex)
            {
                result.Code = HttpResponseStatusCodes.BadRequest;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost("DeleteAsegurado")]
        public async Task<Result> DeleteAsegurado([FromBody]Asegurado asegurado)
        {
            var result = new Result();
            try
            {
                result = await _services.DeleteAsegurado(asegurado);
            }
            catch (Exception ex)
            {
                result.Code = HttpResponseStatusCodes.BadRequest;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
