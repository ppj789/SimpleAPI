using Microsoft.EntityFrameworkCore;
using SimpleAPI.Data;
using SimpleAPI.Models;
using SimpleAPI.Services.Repository;

namespace SimpleAPI.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            this._userRepository = new UserRepository(new SQLiteContext());
        }


        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }



        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }


        public async Task<User> GetUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIDAsync(id);

            return user;
        }


        public async Task<User> UpdateUserAsync(int id, User user)
        {
            if (id != user.Id)
            {
                throw new ArgumentException("User ID does not match.");
            }

            await _userRepository.UpdateUserAsync(user);

            try
            {
                await _userRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    throw new KeyNotFoundException("User not found.");
                }
                else
                {
                    throw;
                }
            }

            return user;
        }


        public async Task<User> CreateUserAsync(User user)
        {
            await _userRepository.InsertUserAsync(user);
            await _userRepository.SaveAsync();

            return user;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = _userRepository.GetUserByIDAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            await _userRepository.DeleteUserAsync(id);
            await _userRepository.SaveAsync();

        }

        private bool UserExists(int id)
        {
            return _userRepository.GetUserByIDAsync(id) != null;
        }


        public static string GenerateApiKey()
        {
            return Guid.NewGuid().ToString(); // Simple API key using GUID
        }

        public async Task<string> GenerateApiKeyAsync(int id)
        {
            var apiKey = GenerateApiKey();

            User user = await _userRepository.GetUserByIDAsync(id);

            if (user == null) 
                throw new KeyNotFoundException("User not found.");

            user.ApiKey = apiKey;


            await UpdateUserAsync(id, user);
            

            return apiKey;
        }


    }
}
