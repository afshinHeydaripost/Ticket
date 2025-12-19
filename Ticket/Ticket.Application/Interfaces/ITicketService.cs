
using Helper;
using Helper.VieModels;

namespace Ticket.Application.Interfaces;

public interface ITicketService : IGeneralServices<Ticket.Domain.Models.Ticket>
{
    Task<GeneralResponse> CreateAsync(TicketViewMode item);
    Task<GeneralResponse> UpdateAsync(UpdateTicketRequest item);
    Task<TicketViewMode> GetItemAsync(string userId,string id);
    Task<List<TicketViewMode>> GetListAsync(string userId);
    Task<List<TicketViewMode>> GetAllAsync();
    Task<List<TicketStatusViewMode>> GetStatsCountAsync();
}
