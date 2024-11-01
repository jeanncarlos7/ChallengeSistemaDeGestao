using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using SistemaDeGestao.Models;
using SistemaDeGestao.Repositorios;
using SistemaDeGestao.Repositorios.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaDeGestao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacaoRepositorio _avaliacaoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public AvaliacaoController(IAvaliacaoRepositorio avaliacaoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _avaliacaoRepositorio = avaliacaoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        /// <summary>
        /// Lista os itens da avaliação.
        /// </summary>
        /// <returns>Os itens da avaliação.</returns>
        /// <response code="200">Returna os itens da avaliação cadastrado.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<AvaliacaoModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> BuscarAvaliacoes()
        {
            List<AvaliacaoModel> avaliacoes = await _avaliacaoRepositorio.BuscarTodasAvaliacao();
            return Ok(avaliacoes);
        }

        /// <summary>
        /// Lista os itens da avaliação por Id.
        /// </summary>
        /// <returns>Os itens da avaliação por Id.</returns>
        /// <response code="200">Returna os itens da avaliação cadastrado por Id.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AvaliacaoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            AvaliacaoModel avaliacao = await _avaliacaoRepositorio.BuscarPorId(id);
            if (avaliacao == null)
            {
                return NotFound();
            }
            return Ok(avaliacao);
        }

        /// <summary>
        /// Cria os itens da avaliação.
        /// </summary>
        /// <returns>Os itens da avaliação criados.</returns>
        /// <response code="200">Returna os itens da avaliação criado.</response>
        [HttpPost]
        [ProducesResponseType(typeof(AvaliacaoModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cadastrar([FromBody] AvaliacaoModelInsert avaliacaoModelInsert)
        {
            try
            {
                if (avaliacaoModelInsert == null)
                    return BadRequest();

                var usuarioExistente = await _usuarioRepositorio.ObterPorId(avaliacaoModelInsert.UsuarioId);
                if (usuarioExistente == null)
                    return BadRequest("O usuário fornecido não existe.");

                var avaliacaoModel = new AvaliacaoModel
                {
                    Nome = avaliacaoModelInsert.Nome,
                    Descricao = avaliacaoModelInsert.Descricao,
                    Status = avaliacaoModelInsert.Status,
                    Email = avaliacaoModelInsert.Email,
                    UsuarioId = usuarioExistente.Id,
                };

                AvaliacaoModel avaliacao = await _avaliacaoRepositorio.Adicionar(avaliacaoModel);

                return CreatedAtAction(nameof(BuscarPorId), new { id = avaliacao.Id }, avaliacao);
            }
            catch(Exception ex) {
                if (ex.InnerException is DbUpdateException dbUpdateException)
                {
                    var sqlException = dbUpdateException.GetBaseException() as OracleException;
                    if (sqlException != null)
                    {
                        var number = sqlException.Number;
                        var message = sqlException.Message;

                        return BadRequest($"Erro ao salvar as alterações no banco de dados: {message}");
                    }
                }
                return BadRequest(ex.Message); 
            }
        }

        /// <summary>
        /// Atualiza os itens da avaliação por Id.
        /// </summary>
        /// <returns>Atualiza os itens da avaliação por Id.</returns>
        /// <response code="200">Returna os itens da avaliação atualizado por Id.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AvaliacaoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AvaliacaoModel>> Atualizar(int id, [FromBody] AvaliacaoModel UpavaliacaoModel)
        {
            if (UpavaliacaoModel == null || UpavaliacaoModel.Id != id)
                return BadRequest();

            AvaliacaoModel avaliacaoAtualizada = await _avaliacaoRepositorio.AtualizarAvaliacao(UpavaliacaoModel, id);
            if (avaliacaoAtualizada == null)
            {
                return NotFound();
            }
            return Ok(avaliacaoAtualizada);
        }

        /// <summary>
        /// Deleta os itens da avaliação por Id.
        /// </summary>
        /// <returns>Apaga os itens da avaliação por Id.</returns>
        /// <response code="200">Returna os itens da avaliação deletado por Id.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _avaliacaoRepositorio.Apagar(id);
            if (!apagado)
                return NotFound();
            
            return Ok(apagado);
        }
    }
}
