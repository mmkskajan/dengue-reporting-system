using CIDRS.Domain.Enums;
using CIDRS.Domain.Models.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Domain.Models.Entity.ChiefOccupants
{
    public class Penalty : BaseEntity
    {
        public DateTime? ResolvedDate { get; set; }
        public DateTime DueDate { get; set; }
        public PenaltyType PenaltyType { get; set; }
        public PenaltyStatus PenaltyStatus { get; set; }
        public int ChiefOccupantId { get; set; }
        public ChiefOccupant ChiefOccupant { get; set; }
    }
}
