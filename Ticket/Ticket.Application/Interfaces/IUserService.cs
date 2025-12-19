
using Helper;
using Helper.VieModels;
using Ticket.Domain.Models;

namespace Ticket.Application.Interfaces;

public interface IUserService : IGeneralServices<User>
{
    Task<GeneralResponse<UserViewModel>> RegisterAsync(UserViewModel user);
    Task<GeneralResponse<UserViewModel>> LoginAsync(LoginRequestViewModel req);
}
