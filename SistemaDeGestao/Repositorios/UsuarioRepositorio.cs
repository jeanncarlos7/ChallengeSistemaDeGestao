﻿using Microsoft.EntityFrameworkCore;
using SistemaDeGestao.Data;
using SistemaDeGestao.Models;

namespace SistemaDeGestao.Repositorios
{
    public class UsuarioRepositorio : Interfaces.IUsuarioRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;
        public UsuarioRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
        {
            _dbContext = sistemaTarefasDBContext;
        }
        public async Task<UsuarioModel> BuscarPorId(int id)
        {
            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario == null)
            {
                throw new Exception($"Usuário com ID {id} não encontrado.");
            }
            return usuario;
        }
        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }
        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            try
            {
                await _dbContext.Usuarios.AddAsync(usuario);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }
        public async Task<UsuarioModel> Atualizar(UsuarioModelUpdate usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }
            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;

            _dbContext.Usuarios.Update(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return usuarioPorId;

        }
        public async Task<bool> Apagar(int id)
        {
            try
            {
                UsuarioModel usuarioPorId = await BuscarPorId(id);

                if (usuarioPorId == null)
                {
                    throw new ArgumentException($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
                }

                _dbContext.Usuarios.Remove(usuarioPorId);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw new ArgumentException($"Usuário para o ID: {id} não foi encontrado no banco de dados.", ex);
            }
        }

        public async Task<UsuarioModel> ObterPorId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<UsuarioModel> Buscar(UsuarioModel usuario, int id)
        {
            throw new NotImplementedException();
        }
    }
}
