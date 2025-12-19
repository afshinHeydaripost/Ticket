using Helper;

namespace Ticket.Domain.Models;

public partial class User:BaseEntity
{

    public string FullName { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Role { get; set; }

    public virtual ICollection<Ticket> TicketAssignedToUsers { get; set; } = new List<Ticket>();

    public virtual ICollection<Ticket> TicketCreatedByUsers { get; set; } = new List<Ticket>();
}
