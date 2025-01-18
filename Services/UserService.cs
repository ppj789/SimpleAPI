using SimpleAPI.Data;

namespace SimpleAPI.Services
{
    public class UserService
    {
        private readonly SQLiteContext _context;

        public UserService(SQLiteContext context)
        {
            _context = context;
        }




    }
}
