using CIDRS.Domain.Models.Entity.Admins;
using CIDRS.Domain.Models.Entity.Applications;
using CIDRS.Domain.Models.Entity.Base;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using CIDRS.Domain.Models.Entity.Masters;
using CIDRS.Domain.Models.Entity.Masters.Pivot;
using CIDRS.Domain.Models.Entity.Polices;
using CIDRS.Domain.Models.Entity.PublicHealthInspectors;
using CIDRS.Domain.Models.Entity.WorkItems;
using CIDRS.Infrastructure.Extensions.DataSeed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Infrastructure
{
    /// <summary>
    /// Data Context of SORA Application
    /// </summary>
    public class ApplicationDataContext : DbContext
    {
        /// <summary>
        /// Constructor of ApplicationDataContext
        /// </summary>
        /// <param name="options">DbContext Option</param>
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        #region Masters
        public DbSet<District> Districts { get; set; }
        public DbSet<MohArea> MohAreas { get; set; }
        public DbSet<PoliceStation> PoliceStations { get; set; }
        public DbSet<MohAreaPoliceStation> MohAreaPoliceStations { get; set; }

        #endregion

        public DbSet<Admin> Admins { get; set; }
        public DbSet<PublicHealthInspector> PublicHealthInspectors { get; set; }
        public DbSet<ChiefOccupant> ChiefOccupants { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<Police> Polices { get; set; }

        #region Application
        public DbSet<ReportingApplication> ReportingApplications { get; set; }
        public DbSet<SurroundingSet> SurroundingSets { get; set; }
        #endregion

        #region WorkItem
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<WorkItemAction> WorkItemActions { get; set; }
        public DbSet<WorkItemRemark> WorkItemRemarks { get; set; }

        #endregion




    }
}
