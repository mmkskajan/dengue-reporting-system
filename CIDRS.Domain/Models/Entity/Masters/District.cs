using CIDRS.Domain.Models.Entity.Base;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using CIDRS.Domain.Models.Entity.PublicHealthInspectors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CIDRS.Domain.Models.Entity.Masters
{
    public class District : BaseEntity
    {
        /// <summary>
        /// Country Name
        /// </summary>
        [Required]
        public string Name { get; set; }
        public List<MohArea> MohAreas { get; set; }
        public List<PublicHealthInspector> PublicHealthInspectors { get; set; }
        public List<ChiefOccupant> ChiefOccupants { get; set; }


        public District()
        {
            MohAreas = new List<MohArea>();
            PublicHealthInspectors = new List<PublicHealthInspector>();
            ChiefOccupants = new List<ChiefOccupant>();
        }
    }
}
