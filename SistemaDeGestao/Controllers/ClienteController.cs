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

        [HttpGet]
        [Consumes("application/json")]
        public async Task<List<ClienteModel>> Get() =>
            await _mongoDbService.GetAsync();

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
