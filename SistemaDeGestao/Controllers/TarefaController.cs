using Microsoft.AspNetCore.Mvc;
using SistemaDeGestao.Models;
using SistemaDeGestao.Repositorios.Interfaces;

namespace SistemaDeGestao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaController(ITarefaRepositorio usuarioRepositorio)
        {
            _tarefaRepositorio = usuarioRepositorio;
        }

        /// <summary>
        /// Lista os itens de tarefa.
        /// </summary>
        /// <returns>Os itens de tarefa.</returns>
        /// <response code="200">Returna os itens de tarefa cadastrado.</response>
        [HttpGet]
        public async Task<ActionResult<List<TarefaModel>>> BuscarTodas() 
        {
            List<TarefaModel> usuarios = await _tarefaRepositorio.BuscarTodas();
            return Ok(usuarios);
        }

        /// <summary>
        /// Lista os itens de tarefa por Id.
        /// </summary>
        /// <returns>Os itens de tarefa por Id.</returns>
        /// <response code="200">Returna os itens de tarefa cadastrado por Id.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaModel>> BuscarPorId(int id)
        {
            TarefaModel tarefa= await _tarefaRepositorio.BuscarPorId(id);
            return Ok(tarefa);
        }

        /// <summary>
        /// Cria os itens de tarefa.
        /// </summary>
        /// <returns>Os itens de tarefa.</returns>
        /// <response code="200">Returna os itens de tarefa criado.</response>
        [HttpPost]
        public async Task<ActionResult<TarefaModel>> Cadastrar([FromBody] TarefaModelInsert TarefaModelInsert)
        {
            var TarefaModel = new TarefaModel();
            TarefaModel.Nome = TarefaModelInsert.Nome;
            TarefaModel.Status = TarefaModelInsert.Status;
            TarefaModel.Descricao = TarefaModelInsert.Descricao;

            TarefaModel tarefa = await _tarefaRepositorio.Adicionar(TarefaModel);
            return Ok(tarefa);
        }

        /// <summary>
        /// Atualiza os itens de tarefa por Id.
        /// </summary>
        /// <returns>Os itens de tarefa por Id.</returns>
        /// <response code="200">Returna os itens de tarefa atualizado por Id.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<TarefaModel>> Atualizar([FromBody] TarefaModel TarefaModel, int id)
        {
            TarefaModel.Id = id;
            TarefaModel tarefa = await _tarefaRepositorio.Atualizar(TarefaModel, id);
            return Ok(tarefa);
        }

        /// <summary>
        /// Deleta os itens de tarefa por Id.
        /// </summary>
        /// <returns>Os itens de tarefa por Id.</returns>
        /// <response code="200">Returna os itens de tarefa deletado por Id.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<TarefaModel>> Apagar(int id)
        {
            bool apagado = await _tarefaRepositorio.Apagar(id);
            return Ok(apagado);
        }
    }
}

