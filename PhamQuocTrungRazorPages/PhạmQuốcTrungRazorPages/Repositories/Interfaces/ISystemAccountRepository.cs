using PhạmQuốcTrungRazorPages.Entities;

namespace PhạmQuốcTrungRazorPages.Repositories.Interfaces
{
    public interface ISystemAccountRepository
    {
        IEnumerable<SystemAccount> GetAll();
        SystemAccount GetByEmail(string email);
        void Add(SystemAccount account);
        void Update(SystemAccount account);
        void Delete(short id);
    }
}
