using Helper.VieModels;
using Ticket.Domain.Models;
internal static class CopyTo
{
    internal static User ToUser(this UserViewModel x)
    {
        return new User()
        {
            FullName = x.FullName,
            Role = x.Role,
            Email = x.Email,
            PasswordHash = x.Password,
        };
    }
    internal static Ticket.Domain.Models.Ticket ToTicket(this TicketViewMode x)
    {
        return new Ticket.Domain.Models.Ticket()
        {
            CreatedByUserId = x.CreatedByUserId,
            CreatedAt = DateTime.Now,
            Description = x.Description,
            Priority = x.Priority,
            Status = x.Status,
            Title = x.Title,
            UpdatedAt = null,

        };
    }

}
