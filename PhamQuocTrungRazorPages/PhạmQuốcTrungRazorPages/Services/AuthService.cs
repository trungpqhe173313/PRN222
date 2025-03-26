using PhạmQuốcTrungRazorPages.Entities;

namespace PhạmQuốcTrungRazorPages.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        private readonly FUNewsDbContext _context;

        public AuthService(IConfiguration config, FUNewsDbContext context)
        {
            _config = config;
            _context = context;
        }

        public SystemAccount? Authenticate(string email, string password)
        {
            // Check admin credentials first
            var adminEmail = _config["AdminCredentials:Email"];
            var adminPassword = _config["AdminCredentials:Password"];

            if (email == adminEmail && password == adminPassword)
            {
                return new SystemAccount
                {
                    AccountEmail = email,
                    AccountRole = 0
                }; // 0 = Admin
            }

            // Check normal users
            return _context.SystemAccounts
                .FirstOrDefault(a => a.AccountEmail == email && a.AccountPassword == password);
        }
    }
}
