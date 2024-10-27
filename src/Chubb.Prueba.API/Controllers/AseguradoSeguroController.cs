using Chubb.Prueba.BL.Interfaces;
using Chubb.Prueba.Const;
using Chubb.Prueba.Entities.Entities;
using Chubb.Prueba.Entities.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Chubb.Prueba.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AseguradoSeguroController:ControllerBase
    {
        private readonly IAseguradoSeguroServices _services;
        public AseguradoSeguroController(IAseguradoSeguroServices services)
        {
            this._services = services;  
        }
        [HttpPost("GetAseguradoSeguro")]
        public async Task<Result> GetAseguradoSeguro([FromBody] QueryParameters parametros)
        {
            var result = new Result();
            try
            {
                result = await _services.GetAseguradoSeguro(parametros);
            }
            catch (Exception ex)
            {
                result.Code = HttpResponseStatusCodes.BadRequest;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost("PostAseguradoSeguro")]
        public async Task<Result> PostAseguradoSeguro([FromBody] AseguradoSeguro relation)
        {
            var result = new Result();
            try
            {
                result = await _services.PostAseguradoSeguro(relation);
            }
            catch (Exception ex)
            {
                result.Code = HttpResponseStatusCodes.BadRequest;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPut("UpdateAseguradoSeguro")]
        public async Task<Result> UpdateAseguradoSeguro([FromBody] AseguradoSeguro relation)
        {
            var result = new Result();
            try
            {
                result = await _services.UpdateAseguradoSeguro(relation);
            }
            catch (Exception ex)
            {
                result.Code = HttpResponseStatusCodes.BadRequest;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost("DeleteAseguradoSeguro")]
        public async Task<Result> DeleteAseguradoSeguro([FromBody] AseguradoSeguro relation)
        {
            var result = new Result();
            try
            {
                result = await _services.DeleteAseguradoSeguro(relation);
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
