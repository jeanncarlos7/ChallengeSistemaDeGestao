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

        [HttpGet]
        public async Task<ActionResult<List<TarefaModel>>> BuscarTodas() 
        {
            List<TarefaModel> usuarios = await _tarefaRepositorio.BuscarTodas();
            return Ok(usuarios);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaModel>> BuscarPorId(int id)
        {
            TarefaModel tarefa= await _tarefaRepositorio.BuscarPorId(id);
            return Ok(tarefa);
        }

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
        [HttpPut("{id}")]
        public async Task<ActionResult<TarefaModel>> Atualizar([FromBody] TarefaModel TarefaModel, int id)
        {
            TarefaModel.Id = id;
            TarefaModel tarefa = await _tarefaRepositorio.Atualizar(TarefaModel, id);
            return Ok(tarefa);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<TarefaModel>> Apagar(int id)
        {
            bool apagado = await _tarefaRepositorio.Apagar(id);
            return Ok(apagado);
        }
    }
}

