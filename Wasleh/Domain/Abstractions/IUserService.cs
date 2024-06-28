using Wasleh.Domain.Entities;
using Wasleh.Dtos;

namespace Wasleh.Domain.Abstractions;

public interface IUserService
{
    Task<UserManagerResponse> RegisterUserAsync(string email, string userName, string password);
    Task<UserManagerResponse> LoginUserUsingUserNameAsync(string userName, string password);
    Task<UserManagerResponse> LoginUserUsingEmailAsync(string email, string password);
    Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);
    Task<UserManagerResponse> ForgetPasswordAsync(string email);
    Task<UserManagerResponse> ResetPasswordAsync(string email, string token, string newPassword);
}