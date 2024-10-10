using API.Errores;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //Agrega Manejo de Errores para un NotFound endPoint
    [Route("errores/{codigo}")]
    [ApiExplorerSettings(IgnoreApi = true)]//Para que no tenga en cuenta el metodo Get que no tiene y nomuestre Error
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int codigo)
        {
            return new ObjectResult(new ApiErrorResponse(codigo));
        }
    }
}
