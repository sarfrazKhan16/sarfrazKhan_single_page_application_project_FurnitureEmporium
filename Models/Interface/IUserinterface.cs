using System.Threading.Tasks;
using FurnitureEmporium.Models.Entity;

namespace FurnitureEmporium.Models.Interface
{
    public interface IUserinterface
    {
        Task<bool> RegisterUserAsync(UserClass user);
        Task<bool> CheckIfEmailExistsAsync(string email);
        Task<bool> AuthenticateUserAsync(string email, string password);
        Task<string?> GetUserNameByEmailAsync(string email);
        Task<int> GetUserIdByEmailAsync(string email);


    }
}
