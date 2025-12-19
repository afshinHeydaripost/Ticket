using Helper;
using Helper.VieModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Ticket.Application.Interfaces;
using Ticket.Domain.Context;
using Ticket.Domain.Models;


namespace Ticket.Application.Services;
public class UserService : GeneralServices<User>, IUserService
{
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher<User> _passwordHasher;
    public UserService(TiketContext Context, ITokenService tokenService, IPasswordHasher<User> passwordHasher) : base(Context)
    {
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<GeneralResponse<UserViewModel>> RegisterAsync(UserViewModel user)
    {
        try
        {
            if (string.IsNullOrEmpty(user.FullName))
            {
                return GeneralResponse<UserViewModel>.Fail("تام کاربر را وارد کنید");
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                return GeneralResponse<UserViewModel>.Fail("ایمیل را وارد کنید");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                return GeneralResponse<UserViewModel>.Fail("کلمه عبور را وارد کنید");
            }
            if (await _Context.Users.AnyAsync(u => u.Email.ToLower() == user.Email.ToLower()))
                return GeneralResponse<UserViewModel>.Fail(" ایمیل تکراری است");
            var objUser = user.ToUser();
            objUser.PasswordHash = _passwordHasher.HashPassword(objUser, user.Password);
            var res = await Add(objUser);
            if (!res.isSuccess)
            {
                return GeneralResponse<UserViewModel>.Fail(user, res.Message, res.ErrorMessage);
            }
            user.Password = "";
            return GeneralResponse<UserViewModel>.Success(user);
        }
        catch (Exception e)
        {
            return GeneralResponse<UserViewModel>.Fail(e);
        }
    }

    public async Task<GeneralResponse<UserViewModel>> LoginAsync(LoginRequestViewModel req)
    {
        try
        {
            var user = await _Context.Users
                        .FirstOrDefaultAsync(u => u.Email == req.Email);
            if (user == null)
                return GeneralResponse<UserViewModel>.NotFound(new UserViewModel());

            var res = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, req.Password);
            if (res == PasswordVerificationResult.Failed)
                return GeneralResponse<UserViewModel>.NotFound(new UserViewModel());


            var access = _tokenService.GenerateAccessToken(user);
            if (!access.isSuccess)
            {
                return GeneralResponse<UserViewModel>.Fail(new UserViewModel(), access.Message, access.ErrorMessage);
            }

            return GeneralResponse<UserViewModel>.Success(new UserViewModel()
            {
                Email = user.Email,
                FullName = user.FullName,
                Id = user.Id,
                Token = access.obj,

            });
        }
        catch (Exception e)
        {
            return GeneralResponse<UserViewModel>.Fail(e);
        }
    }
}

