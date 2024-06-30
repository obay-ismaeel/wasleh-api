using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Wasleh.Domain.Abstractions;
using Wasleh.Domain.Entities;
using Wasleh.Dtos;
using Wasleh.Dtos.Incoming;
using Wasleh.Dtos.Outgoing;
using Wasleh.Presistence.Data;

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
    public async Task<IActionResult> Register(RequestRegisterDto request)
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
    public async Task<IActionResult> LoginEmail(RequestLoginDto request)
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
        var uni = await _unitOfWork.Universities.AddAsync(new University
        {
            Description = "The best in the east",
            Name ="Damascus University",
            LogoPath ="hello",
            Country ="Syria"
        });
        await _unitOfWork.CompleteAsync();

        var fac = await _unitOfWork.Faculties.AddAsync(new Faculty 
        {
            Name = "Information Technology",
            UniversityId = uni.Id,
        });
        await _unitOfWork.CompleteAsync();

        await _unitOfWork.Courses.AddAsync(new Course
        {
            Name = "Virtual Reality",
            FacultyId = fac.Id,
            Description = "Contains bullshit"
        });
        await _unitOfWork.CompleteAsync();

        return Ok(_unitOfWork.Users.GetAll());
    }
}
