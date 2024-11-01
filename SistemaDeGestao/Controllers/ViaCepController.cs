using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeGestao.Services.Interfaces;

namespace SistemaDeGestao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ViaCepController : ControllerBase
    {
        private readonly IViaCepService _viaCepService;

        public ViaCepController(IViaCepService viaCepService)
        {
            _viaCepService = viaCepService;
        }

        // GET: api/ViaCep/{cep}
        /// <summary>
        /// Busca o endereço com base no CEP fornecido.
        /// </summary>
        /// <param name="cep">O CEP do endereço a ser buscado.</param>
        /// <returns>Retorna os dados do endereço correspondente ao CEP.</returns>
        /// <response code="200">Retorna o endereço correspondente ao CEP.</response>
        /// <response code="400">Se o CEP fornecido for inválido.</response>
        /// <response code="404">Se o CEP não for encontrado.</response>
        /// <response code="500">Erro interno ao tentar consumir a API ViaCep.</response>
        [HttpGet("{cep}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressByCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return BadRequest("CEP não pode ser nulo ou vazio.");

            try
            {
                var address = await _viaCepService.GetAddressAsync(cep);

                if (address == null)
                    return NotFound("Endereço não encontrado para o CEP fornecido.");

                return Ok(address);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Erro ao consumir a API ViaCep: {ex.Message}");
            }
        }
    }
}
