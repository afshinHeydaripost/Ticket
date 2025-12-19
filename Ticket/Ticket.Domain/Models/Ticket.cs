using Helper;


namespace Ticket.Domain.Models;

public partial class Ticket : BaseEntity
{

    public string Title { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public string Priority { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string CreatedByUserId { get; set; }

    public string AssignedToUserId { get; set; }

    public virtual User AssignedToUser { get; set; }

    public virtual User CreatedByUser { get; set; }
}
