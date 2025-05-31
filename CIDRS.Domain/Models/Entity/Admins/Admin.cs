using CIDRS.Domain.Models.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Domain.Models.Entity.Admins
{
    public class Admin : BaseEntity
    {
        public string IdentityUserId { get; set; }

    }
}
