
using Helper;
using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Context;



namespace Ticket.Application.Services;

public class GeneralServices<T> : IGeneralServices<T> where T : BaseEntity
{
    protected readonly TiketContext _Context;
    private readonly DbSet<T> entities;

    public GeneralServices(TiketContext Context)
    {
        _Context = Context;
        entities = _Context.Set<T>();
    }

    public async Task<GeneralResponse> Add(T item)
    {
        try
        {
            item.Id = Guid.NewGuid().ToString().Replace("-", "");
            entities.Add(item);
            await Save();
            return GeneralResponse.Success();
        }
        catch (Exception ex)
        {
            return GeneralResponse.Fail(ex);
        }
    }

    public async Task<GeneralResponse> Edit(T item)
    {
        try
        {
            _Context.DetachLocal(item, item.Id);
            await Save();
            return GeneralResponse.Success();
        }
        catch (Exception ex)
        {
            return GeneralResponse.Fail(ex);
        }
    }

    public async Task<GeneralResponse> Delete(string id)
    {
        var res = new GeneralResponse();
        try
        {
            var item = await GetById(id);
            if (item == null)
            {
                return GeneralResponse.NotFound();
            }
            entities.Remove(item);
            await Save();
            return GeneralResponse.SuccessDelete();
        }
        catch (Exception ex)
        {
            return GeneralResponse.Fail(ex);
        }
    }

    public async Task<GeneralResponse> Delete(T entity)
    {
        var res = new GeneralResponse();
        try
        {
            entities.Remove(entity);
            await Save();
            return GeneralResponse.Success();
        }
        catch (Exception ex)
        {
            return GeneralResponse.Fail(ex);
        }
    }

    public async Task<List<T>> GetAll()
    {
        return await entities.ToListAsync();
    }

    public async Task<T> GetById(string id)
    {
        try
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task Save()
    {
        await _Context.SaveChangesAsync();
    }

    public IQueryable<T> GetQuery()
    {
        return entities.AsQueryable();
    }
}

