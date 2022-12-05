using AutoMapper;
using MP.ApiDotNet6.Application.DTOs.Person;
using MP.ApiDotNet6.Application.DTOs.Product;
using MP.ApiDotNet6.Application.DTOs.Purchase;
using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Application.Mappings
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping()
        {
            CreateMap<Person, PersonDTO>();
            CreateMap<Product, ProductDTO>();

            //Customizando o retorno para ter acesso aos dados de Pessoa e Produto 
            CreateMap<Purchase, PurchaseDetailDTO>()
            .ForMember(x => x.Person, opt => opt.Ignore())
            .ForMember(x => x.Product, opt => opt.Ignore())
            .ConstructUsing((model, context) =>
            {
                var dto = new PurchaseDetailDTO
                {
                    Product = model.Product.Name,
                    Id = model.Id,
                    Date = model.Date,
                    Person = model.Person.Name
                };
                return dto;
            });
        }
    }
}
