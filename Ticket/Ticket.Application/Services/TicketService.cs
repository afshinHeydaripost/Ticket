using Helper;
using Helper.VieModels;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Ticket.Application.Interfaces;
using Ticket.Domain.Context;

namespace Ticket.Application.Services;
public class TicketService : GeneralServices<Ticket.Domain.Models.Ticket>, ITicketService
{


    public TicketService(TiketContext Context) : base(Context)
    { }

    public async Task<List<TicketViewMode>> GetListAsync(string userId)
    {
        var query = GetQuery();
        query = query.Where(x => x.CreatedByUserId == userId);
        return await query.Select(x => new TicketViewMode()
        {
            Description = x.Description,
            CreatedAt = Tools.ToDateTimeFa(x.CreatedAt),
            AssignedToUserFullName = (!string.IsNullOrEmpty(x.AssignedToUserId)) ? x.AssignedToUser.FullName : "",
            CreatedByUserFullName = x.CreatedByUser.FullName,
            Id = x.Id,
            Priority = x.Priority,
            Status = x.Status,
            Title = x.Title,
            UpdatedAt = Tools.ToDateTimeFa(x.UpdatedAt)
        }).ToListAsync();
    }
    public async Task<List<TicketViewMode>> GetAllAsync()
    {
        var query = GetQuery();
        return await query.Select(x => new TicketViewMode()
        {
            Description = x.Description,
            CreatedAt = Tools.ToDateTimeFa(x.CreatedAt),
            AssignedToUserFullName = (!string.IsNullOrEmpty(x.AssignedToUserId)) ? x.AssignedToUser.FullName : "",
            CreatedByUserFullName = x.CreatedByUser.FullName,
            Id = x.Id,
            Priority = x.Priority,
            Status = x.Status,
            Title = x.Title,
            UpdatedAt = Tools.ToDateTimeFa(x.UpdatedAt)
        }).ToListAsync();
    }

    public async Task<GeneralResponse> CreateAsync(TicketViewMode item)
    {
        try
        {
            if (string.IsNullOrEmpty(item.Title))
            {
                return GeneralResponse.Fail("عنوان را وارد کنید");
            }
            if (string.IsNullOrEmpty(item.Description))
            {
                return GeneralResponse.Fail("توضیحات را وارد کنید");
            }
            if (string.IsNullOrEmpty(item.Status))
            {
                return GeneralResponse.Fail("وضعیت را وارد کنید");
            }
            if (string.IsNullOrEmpty(item.Priority))
            {
                return GeneralResponse.Fail("اولویت را وارد کنید");
            }
            var obj = item.ToTicket();
            var res = await Add(obj);
            if (!res.isSuccess)
            {
                return GeneralResponse.Fail(res.Message, res.ErrorMessage);
            }
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }

    public async  Task<TicketViewMode> GetItemAsync(string userId, string id)
    {
        var query = GetQuery();
        query = query.Where(x => (x.Id == id) &&(x.CreatedByUserId==userId || x.AssignedToUserId==userId));
        return await query.Select(x => new TicketViewMode()
        {
            Description = x.Description,
            CreatedAt = Tools.ToDateTimeFa(x.CreatedAt),
            AssignedToUserFullName = (!string.IsNullOrEmpty(x.AssignedToUserId)) ? x.AssignedToUser.FullName : "",
            CreatedByUserFullName = x.CreatedByUser.FullName,
            Id = x.Id,
            Priority = x.Priority,
            Status = x.Status,
            Title = x.Title,
            UpdatedAt = Tools.ToDateTimeFa(x.UpdatedAt)
        }).FirstOrDefaultAsync();
    }

    public async Task<List<TicketStatusViewMode>> GetStatsCountAsync()
    {
        var query = GetQuery();
        return await  query.GroupBy(x => x.Status).Select(x => new TicketStatusViewMode() { 
            Status=x.Key,
            CountStatus=x.Count(),
        }).ToListAsync();
    }

    public async  Task<GeneralResponse> UpdateAsync(UpdateTicketRequest item)
    {
        try
        {
            var obj =await  GetById(item.Id);
            if (obj is null)
                return GeneralResponse.NotFound();
            obj.Status = item.Status.ToString();
            obj.AssignedToUserId = item.AssignedToUserId;
            var res = await Edit(obj);
            if (!res.isSuccess)
            {
                return GeneralResponse.Fail(res.Message, res.ErrorMessage);
            }
            return GeneralResponse.Success();
        }
        catch (Exception e)
        {
            return GeneralResponse.Fail(e);
        }
    }
}

