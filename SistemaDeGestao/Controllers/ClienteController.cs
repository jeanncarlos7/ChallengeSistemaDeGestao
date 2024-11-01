using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SistemaDeGestao.Models;
using SistemaDeGestao.Services;


namespace SistemaDeGestao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ClienteController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public ClienteController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        /// <summary>
        /// Lista os itens de cliente.
        /// </summary>
        /// <returns>Os itens de cliente.</returns>
        /// <response code="200">Returna os itens de cliente cadastrado.</response>
        [HttpGet]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(List<ClienteModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> BuscarTodos()
        {
            return Ok(await _mongoDbService.GetAsync());
        } 
        
        /// <summary>
        /// Lista os itens de cliente por Id.
        /// </summary>
        /// <returns>Os itens de cliente por Id.</returns>
        /// <response code="200">Returna os itens de cliente por Id cadastrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AvaliacaoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteModel>> BuscarPorId(string id)
        {
            var entity = await _mongoDbService.GetByIdAsync(id);

            if (entity is null)
            {
                return NotFound();
            }
            return entity;
        }

        /// <summary>
        /// Cria os itens de cliente.
        /// </summary>
        /// <returns>Os itens de cliente criado.</returns>
        /// <response code="200">Returna os itens criados de cliente cadastrado.</response>
        [HttpPost]
        [ProducesResponseType(typeof(AvaliacaoModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cadastrar(ClienteInsertModel clienteInsert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = new ClienteModel { 
                Id = ObjectId.GenerateNewId().ToString(),
                Nome = clienteInsert.Nome,
                Email = clienteInsert.Email            
            };

            await _mongoDbService.CreateAsync(cliente);
            return CreatedAtAction(nameof(BuscarPorId), new { id = cliente.Id }, cliente);
        }

        /// <summary>
        /// Atualiza os itens de cliente por Id.
        /// </summary>
        /// <returns>Os itens de cliente atualizado por Id.</returns>
        /// <response code="200">Returna os itens de cliente atualizado por Id.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AvaliacaoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Atualizar(string id, ClienteInsertModel updatedEntity)
        {
            var entity = await _mongoDbService.GetByIdAsync(id);

            if (entity is null)
                return NotFound();

            var cliente = new ClienteModel { 
                Id = id,
                Nome = updatedEntity.Nome,
                Email = updatedEntity.Email
            };

            cliente.Id = entity.Id;
            await _mongoDbService.UpdateAsync(id, cliente);

            return Ok(cliente);
        }

        /// <summary>
        /// Deleta os itens de cliente por Id.
        /// </summary>
        /// <returns>Os itens de cliente deletado por Id.</returns>
        /// <response code="200">Returna os itens de cliente deletado por Id.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Apagar(string id)
        {
            var entity = await _mongoDbService.GetByIdAsync(id);

            if (entity is null)
            {
                return NotFound();
            }

            await _mongoDbService.RemoveAsync(id);
            return NoContent();
        }
    }
}
