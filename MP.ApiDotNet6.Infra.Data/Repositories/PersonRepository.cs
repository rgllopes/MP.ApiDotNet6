using Microsoft.EntityFrameworkCore;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.FiltersDb;
using MP.ApiDotNet6.Domain.Repositories;
using MP.ApiDotNet6.Infra.Data.Context;

namespace MP.ApiDotNet6.Infra.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _dbContex;

        public PersonRepository(ApplicationDbContext dbContex)
        {
            _dbContex = dbContex;
        }

        public async Task<Person> CreateAsync(Person person)
        {
            _dbContex.Add(person);
            await _dbContex.SaveChangesAsync();
            return person;
        }

        public async Task DeleteAsync(Person person)
        {
            _dbContex.Remove(person);
            await _dbContex.SaveChangesAsync();
        }

        public async Task EditAsync(Person person)
        {
            _dbContex.Update(person);
            await _dbContex.SaveChangesAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _dbContex.People.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetIdByDocumentAsync(string document)
        {
            //Busca código ERP, se não encontrar envia 0
            return (await _dbContex.People.FirstOrDefaultAsync(x => x.Document == document))?.Id ?? 0;
        }

        public async Task<PagedBaseResponse<Person>> GetPagedAsync(PersonFilterDb request)
        {
            var people = _dbContex.People.AsQueryable();
            if(string.IsNullOrEmpty(request.Name))
                people = people.Where(x => x.Name.Contains(request.Name));

            return await PagedBaseResponseHelper
                .GetResponseAsync<PagedBaseResponse<Person>, Person>(people, request);
        }

        public async Task<ICollection<Person>> GetPeopleAsync()
        {
            return await _dbContex.People.ToListAsync();
        }
    }
}
