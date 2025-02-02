namespace SimpleAPI.Models.UserDTO
{
    public class CreateUserResponse : UserResponse
    {
        public required string APIKey { get; set; }
    }
}
