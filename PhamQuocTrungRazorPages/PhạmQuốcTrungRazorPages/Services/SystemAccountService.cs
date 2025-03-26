using PhạmQuốcTrungRazorPages.Entities;
using PhạmQuốcTrungRazorPages.Repositories.Interfaces;
using PhạmQuốcTrungRazorPages.Services.Interfaces;

namespace PhạmQuốcTrungRazorPages.Services
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepository _repository;

        // Inject Repository
        public SystemAccountService(ISystemAccountRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<SystemAccount> GetAllAccounts()
        {
            return _repository.GetAll();
        }

        public SystemAccount GetAccountByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }

        public void CreateAccount(SystemAccount account)
        {
            // Business logic: validate, check email trùng, v.v.
            var existing = _repository.GetByEmail(account.AccountEmail);
            if (existing != null)
                throw new Exception("Email đã tồn tại!");

            _repository.Add(account);
        }

        public void UpdateAccount(SystemAccount account)
        {
            _repository.Update(account);
        }

        public void DeleteAccount(short id)
        {
            _repository.Delete(id);
        }
    }
}
