using Microsoft.AspNetCore.Mvc;
using SistemaDeGestao.Models;
using SistemaDeGestao.Repositorios.Interfaces;

namespace SistemaDeGestao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        /// <summary>
        /// Lista os itens de usuário.
        /// </summary>
        /// <returns>Os itens de usuário.</returns>
        /// <response code="200">Returna os itens de usuário cadastrado.</response>
        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarTodosUsuarios() 
        {
            List<UsuarioModel> usuarios = await _usuarioRepositorio.BuscarTodosUsuarios();
            return Ok(usuarios);
        }

        /// <summary>
        /// Lista os itens de usuário por Id.
        /// </summary>
        /// <returns>Os itens de usuário por Id.</returns>
        /// <response code="200">Returna os itens de usuário cadastrado por Id.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> BuscarPorId(int id)
        {
            UsuarioModel usuario = await _usuarioRepositorio.BuscarPorId(id);
            return Ok(usuario);
        }

        /// <summary>
        /// Cria os itens de usuário.
        /// </summary>
        /// <returns>Os itens de usuário.</returns>
        /// <response code="200">Returna os itens de usuário criado.</response>
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Cadastrar([FromBody] UsuarioModelInsert usuarioModelInsert)
        {
            var usuarioModel = new UsuarioModel();
            usuarioModel.Nome = usuarioModelInsert.Nome;
            usuarioModel.Email = usuarioModelInsert.Email;

            UsuarioModel usuario = await _usuarioRepositorio.Adicionar(usuarioModel);
            return Ok(usuario);
        }

        /// <summary>
        /// Atualiza os itens de usuário por Id.
        /// </summary>
        /// <returns>Os itens de usuário por Id.</returns>
        /// <response code="200">Returna os itens de usuário atualizado por Id.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuarioModel, int id)
        {
            usuarioModel.Id = id;
            UsuarioModel usuario = await _usuarioRepositorio.Atualizar(usuarioModel, id);
            return Ok(usuario);
        }

        /// <summary>
        /// Deleta os itens de usuário por Id.
        /// </summary>
        /// <returns>Os itens de usuário por Id.</returns>
        /// <response code="200">Returna os itens de usuário deletado por Id.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioModel>> Apagar(int id)
        {
            bool apagado = await _usuarioRepositorio.Apagar(id);
            return Ok(apagado);
        }
    }
}

