using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Shared.Installer.Interface
{
    /// <summary>
    /// The Interface that contains startup configuration 
    /// </summary>
    public interface IStartupConfiguration
    {
        /// <summary>
        /// The Method install Service for startup configure
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="app">app</param>
        void Configure(IConfiguration configuration, IApplicationBuilder app);

    }
}
