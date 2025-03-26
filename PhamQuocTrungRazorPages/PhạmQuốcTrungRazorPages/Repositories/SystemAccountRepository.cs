using PhạmQuốcTrungRazorPages.Entities;
using PhạmQuốcTrungRazorPages.Repositories.Interfaces;

namespace PhạmQuốcTrungRazorPages.Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        private static SystemAccountRepository _instance;
        private static readonly object _lock = new object();

        private readonly FUNewsDbContext _context;

        // Private constructor để hạn chế khởi tạo từ bên ngoài
        private SystemAccountRepository(FUNewsDbContext context)
        {
            _context = context;
        }

        // Property Singleton
        public static SystemAccountRepository Instance(FUNewsDbContext context)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new SystemAccountRepository(context);
                }
                return _instance;
            }
        }

        // CRUD methods
        public IEnumerable<SystemAccount> GetAll()
        {
            return _context.SystemAccounts.ToList();
        }

        public SystemAccount GetByEmail(string email)
        {
            return _context.SystemAccounts.FirstOrDefault(a => a.AccountEmail == email);
        }

        public void Add(SystemAccount account)
        {
            _context.SystemAccounts.Add(account);
            _context.SaveChanges();
        }

        public void Update(SystemAccount account)
        {
            _context.SystemAccounts.Update(account);
            _context.SaveChanges();
        }

        public void Delete(short id)
        {
            var account = _context.SystemAccounts.Find(id);
            if (account != null)
            {
                _context.SystemAccounts.Remove(account);
                _context.SaveChanges();
            }
        }
    }
}
