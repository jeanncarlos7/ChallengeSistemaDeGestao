using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using SistemaDeGestao.Models;
using SistemaDeGestao.Repositorios.Interfaces;

namespace SistemaDeGestao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProdutoController : ControllerBase
    {
        
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoController(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        /// <summary>
        /// Lista os itens de produto.
        /// </summary>
        /// <returns>Os itens de produto.</returns>
        /// <response code="200">Returna os itens de produto cadastrado.</response>
        [HttpGet]
        public async Task<ActionResult<List<ProdutoModel>>> Buscar()
        {
            List<ProdutoModel> produtos = await _produtoRepositorio.BuscarTodos();
            return Ok(produtos);
        }

        /// <summary>
        /// Lista os itens de produto por Id.
        /// </summary>
        /// <returns>Os itens de produto por Id.</returns>
        /// <response code="200">Returna os itens de produto cadastrado por Id.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<AvaliacaoModel>> BuscarPorId(int id)
        {
            ProdutoModel produto = await _produtoRepositorio.BuscarPorId(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        /// <summary>
        /// Cria os itens de produto.
        /// </summary>
        /// <returns>Os itens de produto criado.</returns>
        /// <response code="200">Returna os itens de produto criado.</response>
        [HttpPost]
        public async Task<ActionResult<ProdutoModel>> Cadastrar([FromBody] ProdutoInsertModel modelInsert)
        {
            try
            {
                if (modelInsert == null)
                    return BadRequest();


                var produto = new ProdutoModel { 
                    Descricao = modelInsert.Descricao,
                    Nome = modelInsert.Nome,
                    Preco = modelInsert.Preco
                };

                var produtomodel = await _produtoRepositorio.Adicionar(produto);

                return CreatedAtAction(nameof(BuscarPorId), new { id = produtomodel.Id }, produtomodel);
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
        /// Atualiza os itens de produto por Id.
        /// </summary>
        /// <returns>Os itens de produto atualizado por Id.</returns>
        /// <response code="200">Returna os itens de produto atualizado por Id.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoModel>> Atualizar(int id, [FromBody] ProdutoModel produtoModel)
        {
            if (produtoModel == null || !(id > 0))
                return BadRequest();

            var produtoGet = await _produtoRepositorio.BuscarPorId(id);
            if (produtoGet is null)
            {
                return NotFound();
            }

            var produto = await _produtoRepositorio.Atualizar(produtoModel, id);

            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        /// <summary>
        /// Deleta os itens de produto por Id.
        /// </summary>
        /// <returns>Os itens de produto deletado por Id.</returns>
        /// <response code="200">Returna os itens de produto deletado por Id.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _produtoRepositorio.Apagar(id);
            if (!apagado)
                return NotFound();
            
            return Ok(apagado);
        }
    }
}
