using AutoMapper;
using SimpleAPI.Data;
using SimpleAPI.Models;
using SimpleAPI.Models.UserDTO;
using SimpleAPI.Services.Repository;

namespace SimpleAPI.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        readonly Mapper mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserResponse>();
            cfg.CreateMap<User, CreateUserResponse>();
        }));


        public UserService()
        {
            this._userRepository = new UserRepository(new SQLiteContext());
        }


        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }



        public async Task<IEnumerable<UserResponse>> GetUsersAsync()
        {
            return mapper.Map<IEnumerable<UserResponse>>(await _userRepository.GetUsersAsync());
        }

        public async Task<IEnumerable<User>> GetUsersWithAPIKeysAsync()
        {
            return await _userRepository.GetUsersAsync();
        }


        public async Task<UserResponse> GetUserAsync(int id)
        {
            User? user = await _userRepository.GetUserByIDAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            return mapper.Map<UserResponse>(user);
        }


        public async Task<UserResponse> UpdateUserAsync(int id, UpdateUserRequest updateUser)
        {
            User? user = await _userRepository.GetUserByIDAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }


            if (!string.IsNullOrEmpty(updateUser.Username))
            {
                user.Username = updateUser.Username;
            }

            if (!string.IsNullOrEmpty(updateUser.Email))
            {
                user.Email = updateUser.Email;
            }

            if (!string.IsNullOrEmpty(updateUser.Password))
            {
                user.Password = updateUser.Password;
            }

            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveAsync();


            return mapper.Map<UserResponse>(user);
        }

        public async Task<User> UpdateUserAsync(User user)
        {

            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveAsync();

            return user;
        }


        public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest createUser)
        {

            User user = new()
            {
                ApiKey = GenerateApiKey(),
                Email = createUser.Email,
                Username = createUser.Username,
                Password = createUser.Password
            };

            await _userRepository.InsertUserAsync(user);
            await _userRepository.SaveAsync();

            CreateUserResponse createUserResponse = mapper.Map<CreateUserResponse>(user);

            return createUserResponse;
        }

        public async Task DeleteUserAsync(int id)
        {

            await _userRepository.DeleteUserAsync(id);
            await _userRepository.SaveAsync();

        }


        public static string GenerateApiKey()
        {
            return Guid.NewGuid().ToString(); // Simple API key using GUID
        }


        public async Task<string> GenerateApiKeyAsync(int id)
        {
            
            User? user = await _userRepository.GetUserByIDAsync(id);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

            var apiKey = GenerateApiKey();

            user.ApiKey = apiKey;


            user = await UpdateUserAsync(user);


            return user.ApiKey;
        }


    }
}
