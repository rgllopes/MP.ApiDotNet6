using MP.ApiDotNet6.Application.DTOs;

namespace MP.ApiDotNet6.Application.Services.Interface
{
    public interface IPersonService
    {
        Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDTO);
        Task<ResultService<ICollection<PersonDTO>>> GetAsync();
        Task<ResultService<PersonDTO>> GetByIsAsync(int id);
        Task<ResultService> UpdateAsync(PersonDTO personDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
