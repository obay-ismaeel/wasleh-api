using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wasleh.Domain.Abstractions;
using Wasleh.Dtos;
using Wasleh.Dtos.Incoming;
using Wasleh.Dtos.Outgoing;

namespace Wasleh.Controllers;

public class AuthController : BaseController
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    public AuthController(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService, ITokenService tokenService) : base(unitOfWork, mapper)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterRequestDto request)
    {
        var result = await _userService.RegisterUserAsync(
            request.Email,
            request.Password
        );
        if (!result.IsSuccess)
            return BadRequest(new UserRegisterResponse
            {
                Message = result.Message,
                IsSuccess = false,
                Errors = result.Errors,
            });

        var (JwtToken, ExpireDate) = await _tokenService.GenerateJwtTokenAsync(result.User!);

        return Ok(new UserRegisterResponse
        {
            Message = result.Message,
            IsSuccess = true,
            JwtToken = JwtToken,
            ExpireDate = ExpireDate
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginEmail(UserLoginRequestDto request)
    {
        var result = await _userService.LoginUserUsingEmailAsync(
            request.Email,
            request.Password
        );
        if (!result.IsSuccess)
            return BadRequest(new UserLoginResponse
            {
                Message = result.Message,
                IsSuccess = false,
            });

        var (JwtToken, ExpireDate) = await _tokenService.GenerateJwtTokenAsync(result.User!);

        return Ok(new UserLoginResponse
        {
            Message = result.Message,
            IsSuccess = true,
            JwtToken = JwtToken,
            ExpireDate = ExpireDate
        });

    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(_unitOfWork.Users.GetAll());
    }
}
