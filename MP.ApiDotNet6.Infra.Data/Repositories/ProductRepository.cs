using Microsoft.EntityFrameworkCore;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.Repositories;
using MP.ApiDotNet6.Infra.Data.Context;
using System;

namespace MP.ApiDotNet6.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContex;

        public ProductRepository(ApplicationDbContext dbContex)
        {
            _dbContex = dbContex;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _dbContex.Add(product);
            await _dbContex.SaveChangesAsync();
            return product;
        }

        public async Task DeleteAsync(Product product)
        {
            _dbContex.Remove(product);
            await _dbContex.SaveChangesAsync();
        }

        public async Task EditAsync(Product product)
        {
            _dbContex.Update(product);
            await _dbContex.SaveChangesAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _dbContex.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetIdByCodErpAsync(string codErp)
        {
            //Busca código ERP, se não encontrar envia 0
            return (await _dbContex.Products.FirstOrDefaultAsync(x => x.CodErp == codErp)) ?.Id ?? 0;
        }

        public async Task<ICollection<Product>> GetProducAsync()
        {
            return await _dbContex.Products.ToListAsync();
        }
    }
}
