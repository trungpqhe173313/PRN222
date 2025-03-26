using PhạmQuốcTrungRazorPages.Entities;

namespace PhạmQuốcTrungRazorPages.Services.Interfaces
{
    public interface ISystemAccountService
    {
        IEnumerable<SystemAccount> GetAllAccounts();
        SystemAccount GetAccountByEmail(string email);
        void CreateAccount(SystemAccount account);
        void UpdateAccount(SystemAccount account);
        void DeleteAccount(short id);
    }
}
