namespace MP.ApiDotNet6.Infra.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();
    }
}
