using CIDRS.Identity.Domain.Models.Entity;
using CIDRS.Identity.Infrastructure.Extensions.DataSeed;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CIDRS.Identity.Infrastructure
{
    /// <summary>
    /// Identity Data Context
    /// </summary>
    public class IdentityDataContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Constructor of IdentityDataContext
        /// </summary>
        /// <param name="options">DbContext Option</param>
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options)
        {
        }
        /// <summary>
        /// Model creating override
        /// </summary>
        /// <param name="modelBuilder">modelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        /// <summary>
        /// RefreshTokens
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; } 
    }
}
