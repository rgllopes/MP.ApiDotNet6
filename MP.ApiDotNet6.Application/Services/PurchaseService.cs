using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.DTOs.Validations;
using MP.ApiDotNet6.Application.Services.Interface;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.Repositories;

namespace MP.ApiDotNet6.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IProductRepository productRepository, IPersonRepository personRepository, IPurchaseRepository purchaseRepository)
        {
            _productRepository = productRepository;
            _personRepository = personRepository;
            _purchaseRepository = purchaseRepository;
        }

        public async Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDTO)
        {
            //Validação se o objeto não é nulo
            if (purchaseDTO == null)
                return ResultService.Fail<PurchaseDTO>("Objeto deve ser informado!");

            //Validação dos campos obrigatórios foram informados
            var validate = new PurchaseDTOValidator().Validate(purchaseDTO);
            if (!validate.IsValid)
                return ResultService.RequestError<PurchaseDTO>("Problemas de validação!", validate);

            //Busca dos Id produto e Id pessoa
            var productId = await _productRepository.GetIdByCodErpAsync(purchaseDTO.CodErp);
            var personId = await _personRepository.GetIdByDocumentAsync(purchaseDTO.Document);

            //Criação do objeto de compra
            var purchase = new Purchase(productId, personId);

            //Inserção na base de dados
            var data = await _purchaseRepository.CreateAsync(purchase);

            //Recupera o id gerado na compra
            purchaseDTO.Id = data.Id;

            //Envia informação de id
            return ResultService.Ok(purchaseDTO);
        }
    }
}
