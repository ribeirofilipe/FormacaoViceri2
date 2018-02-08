using Fatec.RD.Bussiness;
using Fatec.RD.Bussiness.Inputs;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Dominio.ViewModel;
using Microsoft.AspNetCore.JsonPatch;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Fatec.RD.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/Despesa")]
    public class DespesaController : ApiController
    {
        DespesaNegocio _appDespesa;

        public DespesaController()
        {
            _appDespesa = new DespesaNegocio();
        }

        /// <summary>
        /// Método que insere uma nova despesa
        /// </summary>
        /// <param name="usuario">Input de despesa</param>
        /// <remarks>Insere uma nova despesa</remarks>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.Created)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(DespesaViewModel))]
        [HttpPost]
        public IHttpActionResult Post([FromBody] DespesaInput input)
        {
            var obj = _appDespesa.Adicionar(input);
            return Created($"{Request?.RequestUri}/{obj.Id}", obj);
        }

        /// <summary>
        /// Método que altera uma despesa....
        /// </summary>
        /// <param name="id">Id da despesa</param>
        /// <returns></returns>
        /// <remarks>Deleta uma despesa</remarks>
        /// <response code="202">Accepted</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.Accepted)]
        [SwaggerResponse(HttpStatusCode.NotFound, "NotFound")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(Despesa))]
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] DespesaInput input)
        {
            var obj = _appDespesa.Atualizar(id, input);
            return Content(HttpStatusCode.Accepted, obj);
        }

        /// <summary>
        /// Método que altera uma despesa
        /// </summary>
        /// <param name="id">Id da despesa</param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>Deleta uma despesa</remarks>
        /// <response code="202">Accepted</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.Accepted)]
        [SwaggerResponse(HttpStatusCode.NotFound, "NotFound")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(Despesa))]
        [Route("{id}")]
        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody] JsonPatchDocument<Despesa> input)
        {
            var obj = _appDespesa.Atualizar(id, input);
            return Content(HttpStatusCode.Accepted, obj);
        }


        /// <summary>
        /// Método que exclui uma despesa....
        /// </summary>
        /// <param name="id">Id da despesa</param>
        /// <returns></returns>
        /// <remarks>Deleta um aluno</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound, "NotFound")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(DespesaViewModel))]
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _appDespesa.Deletar(id);
            return Ok();
        }
        /// <summary>
        /// Método que lista despesa....
        /// </summary>

        /// <returns></returns>
        /// /// <param name="id">Id da despesa</param>
        /// <remarks>Insere uma nova despesa</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(DespesaViewModel))]
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(_appDespesa.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que obtem uma lista de despesa....
        /// </summary>
        /// <returns>Lista de Despesa</returns>
        /// <remarks>Obtem lista de depesa</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_appDespesa.Selecionar());
        }
    }
}
