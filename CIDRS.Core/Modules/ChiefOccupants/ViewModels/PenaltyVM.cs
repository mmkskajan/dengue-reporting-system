using AutoMapper;
using CIDRS.Core.Mappings;
using CIDRS.Domain.Enums;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Core.Modules.ChiefOccupants.ViewModels
{
    public class PenaltyVM : IMapFrom<Penalty>
    {
        /// <summary>
        /// Auto generated and auto increment Entity Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identifier
        /// </summary>
        public string Identifier { get; set; }
        /// <summary>
        /// Created Date and Time
        /// </summary>
        public Nullable<DateTime> CreatedAt { get; set; }
        /// <summary>
        /// Updated Date and Time
        /// </summary>
        public Nullable<DateTime> UpdatedAt { get; set; }
        /// <summary>
        /// Deleted Date and Time
        /// </summary>
        public Nullable<DateTime> ArchivedAt { get; set; }

        public DateTime? ResolvedDate { get; set; }
        public DateTime DueDate { get; set; }
        public PenaltyType PenaltyType { get; set; }
        public PenaltyStatus PenaltyStatus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Penalty, PenaltyVM>();

        }
    }
}
