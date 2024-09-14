using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<ActionResult<List<ProdutoModel>>> Buscar()
        {
            List<ProdutoModel> produtos = await _produtoRepositorio.BuscarTodos();
            return Ok(produtos);
        }

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
                    var sqlException = dbUpdateException.GetBaseException() as SqlException;
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
