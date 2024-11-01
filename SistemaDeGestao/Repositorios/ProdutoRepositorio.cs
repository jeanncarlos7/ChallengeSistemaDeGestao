using Microsoft.EntityFrameworkCore;
using SistemaDeGestao.Data;
using SistemaDeGestao.Models;
using SistemaDeGestao.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaDeGestao.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;

        public ProdutoRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
        {
            _dbContext = sistemaTarefasDBContext;
        }

        public async Task<List<ProdutoModel>> BuscarTodos()
        {
            return await _dbContext.Produtos.ToListAsync();
        }

        public async Task<ProdutoModel> BuscarPorId(int id)
        {
            return await _dbContext.Produtos.FindAsync(id);
        }

        public async Task<ProdutoModel> Adicionar(ProdutoModel produto)
        {
            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();
            return produto;
        }

        public async Task<ProdutoModel> Atualizar(ProdutoModel produto, int id)
        {
            ProdutoModel produtoPorId = await BuscarPorId(id);

            if (produtoPorId == null)
            {
                throw new Exception($"Produto com ID {id} não encontrado.");
            }

            produtoPorId.Nome = produto.Nome;
            produtoPorId.Preco = produto.Preco;
            produtoPorId.Descricao = produto.Descricao;

            _dbContext.Produtos.Update(produtoPorId);
            await _dbContext.SaveChangesAsync();

            return produtoPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            ProdutoModel produtoPorId = await BuscarPorId(id);

            if (produtoPorId == null)
            {
                throw new Exception($"Produto com ID {id} não encontrado.");
            }

            _dbContext.Produtos.Remove(produtoPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
