using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CIDRS.Shared.Installer.Interface
{
    /// <summary>
    /// The Interface that contains IInstaller
    /// </summary>
    public interface IInstaller
    {
        /// <summary>
        /// Oreder By Key
        /// </summary>
        public int OrderByKey { get; }
        /// <summary>
        /// The Method Install Services
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="services"></param>
        /// <param name="startupAssembly"></param>
        void InstallServices(IConfiguration configuration, IServiceCollection services, Assembly startupAssembly);
    }
}
