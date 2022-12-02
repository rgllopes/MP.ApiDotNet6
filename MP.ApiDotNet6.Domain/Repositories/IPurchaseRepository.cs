﻿using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Domain.Repositories
{
    public interface IPurchaseRepository
    {
        Task<Purchase> GetByIdAsync(int id);
        Task<ICollection<Purchase>> GetPeopleAsync();
        Task<Purchase> CreateAsync(Purchase purchase);
        Task EditAsync(Product purchase);
        Task DeleteAsync(Product purchase);
    }
}