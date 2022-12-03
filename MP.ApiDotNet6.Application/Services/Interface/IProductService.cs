﻿using MP.ApiDotNet6.Application.DTOs;

namespace MP.ApiDotNet6.Application.Services.Interface
{
    public interface IProductService
    {
        Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDTO);
        Task<ResultService<ProductDTO>> GetByIdAsync(int id);
        Task<ResultService<ICollection<ProductDTO>>> GetAsync();
        Task<ResultService> UpdateAsync(ProductDTO productDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
