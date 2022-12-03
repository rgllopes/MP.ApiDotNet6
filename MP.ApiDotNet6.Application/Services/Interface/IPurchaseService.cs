using MP.ApiDotNet6.Application.DTOs;

namespace MP.ApiDotNet6.Application.Services.Interface
{
    public interface IPurchaseService
    {
        Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDTO);
    }
}
