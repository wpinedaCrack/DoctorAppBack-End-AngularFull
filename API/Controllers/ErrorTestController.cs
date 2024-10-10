using API.Errores;
using Data;
using Microsoft.AspNetCore.Mvc;
using Models.Entidades;

namespace API.Controllers
{
    public class ErrorTestController : BaseApiController
    {
        public readonly AplicationDbContext _db;

        public ErrorTestController(AplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("auth")]
        public ActionResult<string> GetNoAuthorize()
        {
            return "No Autorizado";
        }

        [HttpGet("not-found")]
        public ActionResult<Usuario> GetNoFound()
        {
            var objeto = _db.Usuarios.Find(-1);
            if (objeto == null)
            {
                return NotFound(new ApiErrorResponse(404));
            }
            return objeto;
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var objeto = _db.Usuarios.Find(-1);
            var objetoString = objeto.ToString();
            return objetoString;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequestr()
        {
            return BadRequest(new ApiErrorResponse(400));// "La Solicitud es no valida - Bad Request");
        }
    }
}
