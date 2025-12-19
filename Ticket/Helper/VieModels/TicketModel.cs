using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.VieModels
{
    internal class TicketModel
    {
    }

    public  class TicketStatusViewMode
    {
        public string Status { get; set; }
        public int? CountStatus { get; set; }

    }

    public class UpdateTicketRequest : BaseEntity
    {
        public string Status { get; set; } 
        public string AssignedToUserId { get; set; } 
    }
    public  class TicketViewMode : BaseEntity
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }
        public int? CountStatus { get; set; }

        public string Priority { get; set; }

        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public string CreatedByUserId { get; set; }
        public string CreatedByUserFullName { get; set; }
        public string AssignedToUserFullName { get; set; }

    }
}
