using MongoDB.Driver;
using SistemaDeGestao.Models;
using SistemaDeGestao.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaDeGestao.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IMongoCollection<ClienteModel> _clientes;

        public ClienteRepositorio(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("DB_SistemaTarefas");
            _clientes = database.GetCollection<ClienteModel>("Clientes");
        }

        public async Task<List<ClienteModel>> BuscarTodosClientes()
        {
            return await _clientes.Find(cliente => true).ToListAsync();
        }

        public async Task<ClienteModel> BuscarPorId(string id)
        {
            return await _clientes.Find(cliente => cliente.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ClienteModel> Adicionar(ClienteModel cliente)
        {
            await _clientes.InsertOneAsync(cliente);
            return cliente;
        }

        public async Task<bool> Atualizar(string id, ClienteModel cliente)
        {
            var resultado = await _clientes.ReplaceOneAsync(cliente => cliente.Id == id, cliente);
            return resultado.ModifiedCount > 0;
        }

        public async Task<bool> Apagar(string id)
        {
            var resultado = await _clientes.DeleteOneAsync(cliente => cliente.Id == id);
            return resultado.DeletedCount > 0;
        }
    }
}
