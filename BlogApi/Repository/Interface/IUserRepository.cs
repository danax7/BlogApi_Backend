using BlogApi.Entity;

namespace BlogApi.Repository.Interface;

public interface IUserRepository
{
    public Task CreateUser(UserEntity userEntity);
    // public Task<UserEntity?> GetUserByEmail(string email);
    // public Task<UserEntity?> GetUserByEmailAndPassword(string email, string password);
    // public Task<UserEntity?> GetUserById(Guid userId);
    // public Task UpdateUser(UserEntity userEntity);
}