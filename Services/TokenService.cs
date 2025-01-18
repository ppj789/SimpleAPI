using SimpleAPI.Data;

namespace SimpleAPI.Services
{


    public class TokenService
    {


        private readonly SQLiteContext _context;

        public TokenService(SQLiteContext context)
        {
            _context = context;
        }




    }
}
