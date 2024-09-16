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
        public async Task<List<ClienteModel>> Get() =>
            await _mongoDbService.GetAsync();

        /// <summary>
        /// Lista os itens de cliente por Id.
        /// </summary>
        /// <returns>Os itens de cliente por Id.</returns>
        /// <response code="200">Returna os itens de cliente por Id cadastrado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteModel>> Get(string id)
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
        public async Task<IActionResult> Post(ClienteInsertModel clienteInsert)
        {
            var cliente = new ClienteModel { 
                Id = ObjectId.GenerateNewId().ToString(),  // Gera um novo ObjectId
                Nome = clienteInsert.Nome,
                Email = clienteInsert.Email            
            };

            await _mongoDbService.CreateAsync(cliente);
            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
        }

        /// <summary>
        /// Atualiza os itens de cliente por Id.
        /// </summary>
        /// <returns>Os itens de cliente atualizado por Id.</returns>
        /// <response code="200">Returna os itens de cliente atualizado por Id.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ClienteInsertModel updatedEntity)
        {
            var entity = await _mongoDbService.GetByIdAsync(id);

            if (entity is null)
            {
                return NotFound();
            }

            var cliente = new ClienteModel { 
                Id = id,
                Nome = updatedEntity.Nome,
                Email = updatedEntity.Email
            };

            cliente.Id = entity.Id;
            await _mongoDbService.UpdateAsync(id, cliente);

            return NoContent();
        }

        /// <summary>
        /// Deleta os itens de cliente por Id.
        /// </summary>
        /// <returns>Os itens de cliente deletado por Id.</returns>
        /// <response code="200">Returna os itens de cliente deletado por Id.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
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
