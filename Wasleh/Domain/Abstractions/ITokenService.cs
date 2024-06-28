using Wasleh.Domain.Entities;

namespace Wasleh.Domain.Abstractions;
public interface ITokenService
{
    Task<(string JwtToken, DateTime ExpireDate)> GenerateJwtTokenAsync(User user);
    Task<(string Message, bool IsSuccess)> VerfiyTokenAsync(string jwtToken);
}