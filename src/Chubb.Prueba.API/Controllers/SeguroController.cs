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
    public class SeguroController : ControllerBase
    {
        private readonly ISeguroServices _services;
        public SeguroController(ISeguroServices services)
        {
            this._services = services;
        }
        [HttpPost("GetSeguro")]
        public async Task<Result> GetSeguro([FromBody] QueryParameters parametros)
        {
            var result = new Result();
            try
            {
                result = await _services.GetSeguro(parametros);
            }
            catch (Exception ex)
            {
                result.Code = HttpResponseStatusCodes.BadRequest;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost("PostSeguro")]
        public async Task<Result> PostSeguro([FromBody] Seguro seguro)
        {
            var result = new Result();
            try
            {
                
                result = await _services.PostSeguro(seguro);
            }
            catch (Exception ex)
            {
                result.Code = HttpResponseStatusCodes.BadRequest;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost("UpdateSeguro")]
        public async Task<Result> UpdateSeguro([FromBody] Seguro seguro)
        {
            var result = new Result();
            try
            {
                result = await _services.UpdateSeguro(seguro);
            }
            catch (Exception ex)
            {
                result.Code = HttpResponseStatusCodes.BadRequest;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost("DeleteSeguro")]
        public async Task<Result> DeleteSeguro([FromBody] Seguro seguro)
        {
            var result = new Result();
            try
            {
                result = await _services.DeleteSeguro(seguro);
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
