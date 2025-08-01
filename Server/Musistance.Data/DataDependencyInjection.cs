using Microsoft.Extensions.DependencyInjection;
using Musistance.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musistance.Data
{
    public static class DataDependencyInjection
    {
        /// <summary>
        /// Adds the data module to DI.
        /// </summary>
        /// <param name="services">The DI container.</param>
        /// <returns>The DI container.</returns>
        public static IServiceCollection AddData (this IServiceCollection services)
        {
            return services.AddScoped<MusistanceDbContext>();
        }
    }
}
