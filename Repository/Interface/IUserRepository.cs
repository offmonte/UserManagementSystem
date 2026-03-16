using UserManagementSystem.Models;

namespace UserManagementSystem.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User> Create(User user);

        Task<User?> GetById(Guid id);

        Task<IEnumerable<User>> GetAll();

        Task<bool> EmailExists(string email);
        
        Task Update(User user);

        Task SoftDelete(Guid id);
    }
}