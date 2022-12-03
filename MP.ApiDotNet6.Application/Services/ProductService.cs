using AutoMapper;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.DTOs.Validations;
using MP.ApiDotNet6.Application.Services.Interface;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.Repositories;

namespace MP.ApiDotNet6.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDTO)
        {
            if (productDTO == null)
                return ResultService.Fail<ProductDTO>("Objeto deve ser informado!");

            var result = new ProductDTOValidator().Validate(productDTO);
            if (!result.IsValid)
                return ResultService.RequestError<ProductDTO>("Problemas na validação!", result);

            //Salva na base de dados
            var product = _mapper.Map<Product>(productDTO);
            var data = await _productRepository.CreateAsync(product);

            //Recupera os dados gravados e devolve dados full
            return ResultService.Ok(_mapper.Map<ProductDTO>(data));
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return ResultService.Fail("Produto não encontrado!");

            await _productRepository.DeleteAsync(product);
            return ResultService.Ok($"Produto do id: {id} foi deletado!");
        }

        public async Task<ResultService<ICollection<ProductDTO>>> GetAsync()
        {
            var products = await _productRepository.GetProducAsync();
            return ResultService.Ok(_mapper.Map<ICollection<ProductDTO>>(products));
        }

        public async Task<ResultService<ProductDTO>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return ResultService.Fail<ProductDTO>("Produto não encontrado");

            return ResultService.Ok(_mapper.Map<ProductDTO>(product));
        }

        public async Task<ResultService> UpdateAsync(ProductDTO productDTO)
        {
            //Verifica se o produto foi informado
            if (productDTO == null)
                return ResultService.Fail("Objeto deve ser informado!");

            //Valida os dados para edição
            var validation = new ProductDTOValidator().Validate(productDTO);
            if (!validation.IsValid)
                return ResultService.RequestError("Problemas de validação!", validation);

            //Procura o produto na base de dados
            var product = await _productRepository.GetByIdAsync(productDTO.Id);
            if (product == null)
                return ResultService.Fail("Produto não encontrado!");

            //Realiza a edição do produto informado
            product = _mapper.Map(productDTO, product);
            await _productRepository.EditAsync(product);
            return ResultService.Ok("Produto editado");
        }
    }
}
