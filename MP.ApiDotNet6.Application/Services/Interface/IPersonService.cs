using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.DTOs.Person;
using MP.ApiDotNet6.Domain.FiltersDb;

namespace MP.ApiDotNet6.Application.Services.Interface
{
    public interface IPersonService
    {
        Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDTO);
        Task<ResultService<ICollection<PersonDTO>>> GetAsync();
        Task<ResultService<PersonDTO>> GetByIsAsync(int id);
        Task<ResultService> UpdateAsync(PersonDTO personDTO);
        Task<ResultService> DeleteAsync(int id);
        Task<ResultService<PagedBaseResponseDTO<PersonDTO>>> GetPagedAsync(PersonFilterDb personFilter);
    }
}
