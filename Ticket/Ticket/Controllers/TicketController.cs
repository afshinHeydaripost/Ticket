using Helper;
using Helper.VieModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Interfaces;


namespace Ticket.Controllers;

[ApiController]
[Route("tickets")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _service;

    public TicketsController(ITicketService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Employee")]
    [HttpPost]
    public async Task<IActionResult> create([FromBody] TicketViewMode item)
    {
        item.CreatedByUserId = User.GetLoginedUserId();
        item.Status = TicketStatus.Open.ToString();
        var res = await _service.CreateAsync(item);
        return Ok(res);
    }
    [Authorize]
    [HttpGet("My")]
    public async Task<IActionResult> my()
    {
        var list = await _service.GetListAsync(User.GetLoginedUserId());
        return Ok(list);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdateTicketRequest request)
    {
        request.Id = id;
        var res = await _service.UpdateAsync(request);
        return Ok(res);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("stats")]
    public async Task<IActionResult> Stats()
    {
        var list = await _service.GetStatsCountAsync();
        return Ok(list);
    }


    [Authorize]
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetItem(string id)
    {
        var list = await _service.GetItemAsync(User.GetLoginedUserId(), id);
        return Ok(list);
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var res = await _service.Delete(id);
        return Ok(res);
    }
}
