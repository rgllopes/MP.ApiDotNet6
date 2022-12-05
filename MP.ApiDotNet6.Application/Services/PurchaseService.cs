using AutoMapper;
using MP.ApiDotNet6.Application.DTOs.Purchase;
using MP.ApiDotNet6.Application.DTOs.Validations;
using MP.ApiDotNet6.Application.Services.Interface;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.Repositories;
using MP.ApiDotNet6.Infra.Data.Repositories;

namespace MP.ApiDotNet6.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(
            IProductRepository productRepository, 
            IPersonRepository personRepository, 
            IPurchaseRepository purchaseRepository, 
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _personRepository = personRepository;
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
            try
            {
                await _unitOfWork.BeginTransaction();
                var productId = await _productRepository.GetIdByCodErpAsync(purchaseDTO.CodErp);
                if (productId == 0)
                {
                    var product = new Product(purchaseDTO.ProductName, purchaseDTO.CodErp, purchaseDTO.Price ?? 0);
                    await _productRepository.CreateAsync(product);
                    productId = product.Id;
                }

                var personId = await _personRepository.GetIdByDocumentAsync(purchaseDTO.Document);
                var purchase = new Purchase(productId, personId); //Criação do objeto de compra

                var data = await _purchaseRepository.CreateAsync(purchase); //Inserção na base de dados
                purchaseDTO.Id = data.Id; //Recupera o id gerado na compra
                await _unitOfWork.Commit();
                return ResultService.Ok(purchaseDTO); //Envia informação de id
            }
            catch(Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<PurchaseDTO>($"{ex.Message}");
            }
        }
        //##################################################################################
        public async Task<ResultService<ICollection<PurchaseDetailDTO>>> GetAsync()
        {
            var purchases = await _purchaseRepository.GetAllAsync();
            return ResultService.Ok(_mapper.Map<ICollection<PurchaseDetailDTO>>(purchases));
        }

        public async Task<ResultService<PurchaseDetailDTO>> GetByIdAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);
            if (purchase == null)
                return ResultService.Fail<PurchaseDetailDTO>("Compra não encontrada!");

            return ResultService.Ok(_mapper.Map<PurchaseDetailDTO>(purchase));
        }
        //##################################################################################
        public async Task<ResultService> RemoveAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);
            if (purchase == null)
                return ResultService.Fail("Compra não encontrada!");

            await _purchaseRepository.DeleteAsync(purchase);
            return ResultService.Ok($"Compra {id} foi removida com sucesso!");
        }
        //##################################################################################
        public async Task<ResultService<PurchaseDTO>> UpdateAsync(PurchaseDTO purchaseDTO)
        {
            if (purchaseDTO == null)
                return ResultService.Fail<PurchaseDTO>("Objeto deve ser informado!");

            var result = new PurchaseDTOValidator().Validate(purchaseDTO);
            if (result.IsValid)
                return ResultService.RequestError<PurchaseDTO>("Problemas de validação!", result);

            var purchase = await _purchaseRepository.GetByIdAsync(purchaseDTO.Id);
            if (purchaseDTO == null)
                return ResultService.Fail<PurchaseDTO>("Compra não encontrada!");

            var productId = await _productRepository.GetIdByCodErpAsync(purchaseDTO.CodErp);
            var personId = await _personRepository.GetIdByDocumentAsync(purchaseDTO.Document);
            purchase.Edit(purchase.Id, productId, personId);
            await _purchaseRepository.EditAsync(purchase);
            return ResultService.Ok(purchaseDTO);
        }
    }
}
