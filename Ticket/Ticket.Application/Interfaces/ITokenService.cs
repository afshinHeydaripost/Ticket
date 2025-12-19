using Ticket.Domain.Models;
using Helper;

namespace Ticket.Application.Interfaces;
public interface ITokenService
{
    GeneralResponse<string> GenerateAccessToken(User user);

}

